namespace PipeliningLibrary.UnitTests.Pipes
{
    using System;

    public class RemoveCasePipe : IPipe
    {
        public object Run(dynamic input)
        {
            string text = input;
            return text.ToLowerInvariant();
        }
    }
}
