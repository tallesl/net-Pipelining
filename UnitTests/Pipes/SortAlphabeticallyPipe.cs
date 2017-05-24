namespace PipeliningLibrary.UnitTests.Pipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SortAlphabeticallyPipe : IPipe
    {
        public object Run(dynamic input)
        {
            IEnumerable<string> text = input;

            var sorted = text.ToList();
            sorted.Sort();
            return sorted;
        }
    }
}
