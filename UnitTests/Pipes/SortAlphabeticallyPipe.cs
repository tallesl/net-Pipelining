namespace Pipelining.UnitTests
{
    using PipeliningLibrary;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SortAlphabeticallyPipe : IPipe
    {
        public object Run(dynamic input, Action<string> progress)
        {
            IEnumerable<string> text = input;
            var sorted = text.ToList();

            sorted.Sort();

            return sorted;
        }
    }
}
