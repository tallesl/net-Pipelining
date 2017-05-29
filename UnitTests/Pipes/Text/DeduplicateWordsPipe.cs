namespace PipeliningLibrary.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DeduplicateWordsPipe : IPipe
    {
        public object Run(dynamic input)
        {
            IEnumerable<string> words = input;
            return words.Distinct();
        }
    }
}
