namespace PipeliningLibrary.TestApplication
{
    using PipeliningLibrary;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GetIssuesDetails : IPipe
    {
        public object Run(dynamic input)
        {
            // GitHub user
            string user = input.User;

            // repositories data
            IEnumerable<dynamic> repositories = input.Repositories;

            // found issues by repository
            var issues = new Dictionary<string, IEnumerable<dynamic>>();

            // iterating through each repository
            foreach (var repository in repositories)
            {
                // repository name
                string name = repository.name;

                // notifying the current navigation
                Console.WriteLine("Getting \"{0}\" issues...", name);

                // getting repository issues
                issues.Add(name, Http.Get("https://api.github.com/repos/{0}/{1}/issues", user, name));
            }

            // notifying how many issues we found
            Console.WriteLine("Found {0} total issues for {1} repositories.",
                issues.Select(kvp => kvp.Value.Count()).Sum(), issues.Keys.Count);

            // setting the "Issues" property
            input.Issues = issues;

            // here, take it
            return input;
        }
    }
}
