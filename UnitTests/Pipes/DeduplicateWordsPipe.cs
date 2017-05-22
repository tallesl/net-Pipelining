namespace Pipelining.UnitTests
{
    using PipeliningLibrary;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DeduplicateWordsPipe : IPipe
    {
        public object Run(dynamic input, Action<string> progress)
        {
            IEnumerable<string> words = input;

            progress("Removing duplicate words...");
            return words.Distinct();
        }
    }
}
