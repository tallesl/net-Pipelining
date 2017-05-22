namespace Pipelining.UnitTests.Pipes
{
    using PipeliningLibrary;
    using System;

    public class RemoveCasePipe : IPipe
    {
        public object Run(dynamic input, Action<string> progress)
        {
            string text = input;

            progress("Replacing upper case characters to lower case...");
            return text.ToLowerInvariant();
        }
    }
}
