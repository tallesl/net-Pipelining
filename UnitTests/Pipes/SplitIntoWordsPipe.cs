namespace PipeliningLibrary.UnitTests.Pipes
{
    using System;

    public class SplitIntoWordsPipe : IPipe
    {
        public object Run(dynamic input, Action<string> progress)
        {
            string text = input;

            progress("Splitting text into collection of words...");
            return text.Split(' ');
        }
    }
}
