namespace PipeliningLibrary.TestApplication
{
    using PipeliningLibrary;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GetUserRepositories : IPipe
    {
        public object Run(dynamic input)
        {
            // GitHub user
            string user = input.User;

            // number of repositories to retrieve in each API call
            int perPage = input.PerPage;

            // found repositories
            var repositories = new List<dynamic>();

            // current navigation
            var i = 1;

            // looping
            for (;;)
            {
                // notifying the current navigation
                Console.WriteLine("Navigating to page {0}...", i);

                // getting the current page data
                var current = Http.Get(
                    "https://api.github.com/users/{0}/repos?per_page={1}&page={2}", user, perPage, i);

                // to the next page
                ++i;

                // if anything was returned we add to our results and continue
                if (current.Any()) repositories.AddRange(current);

                // else we break
                else break;
            }

            // notifying about how many repositories we found
            Console.WriteLine("Found {0} repositories.", repositories.Count);

            // setting the "Repositories" property
            input.Repositories = repositories;

            // here, take it
            return input;
        }
    }
}
