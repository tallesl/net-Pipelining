namespace PipeliningLibrary.TestApplication
{
    using PipeliningLibrary;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class FilterOpenIssues : IPipe
    {
        public object Run(dynamic input, Action<string> progress)
        {
            // repositories data
            IEnumerable<dynamic> repositories = input.Repositories;

            // notifying that the filtering is taking place
            progress("Filtering repositories...");

            // total of repositories received
            var total = repositories.Count();

            // filtered repositories
            var filtered = repositories.Where(r => r.open_issues > 0).ToList();

            // notifying what we filtered
            progress(string.Format("Picked {0} from {1} repositories.", filtered.Count, total));

            // setting the "Repositories" property
            input.Repositories = filtered;

            // here, take it
            return input;
        }
    }
}
