namespace PipeliningLibrary.UnitTests.Pipes
{
    using System;

    public class SplitIntoWordsPipe : IPipe
    {
        public object Run(dynamic input)
        {
            string text = input;
            return text.Split(' ');
        }
    }
}
