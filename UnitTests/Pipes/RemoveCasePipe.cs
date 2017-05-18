namespace Pipelining.UnitTests
{
    using PipeliningLibrary;
    using System;

    public class RemoveCasePipe : IPipe
    {
        public object Run(dynamic input, Action<string> progress)
        {
            string text = input;

            return text.ToLowerInvariant();
        }
    }
}
