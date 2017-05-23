namespace PipeliningLibrary.UnitTests.Pipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SortAlphabeticallyPipe : IPipe
    {
        public object Run(dynamic input, Action<string> progress)
        {
            IEnumerable<string> text = input;

            progress("Sorting words alphabetically...");

            var sorted = text.ToList();
            sorted.Sort();
            return sorted;
        }
    }
}
