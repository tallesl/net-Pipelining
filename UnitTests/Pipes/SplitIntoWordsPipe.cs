namespace Pipelining.UnitTests
{
    using PipeliningLibrary;
    using System;

    public class SplitIntoWordsPipe : IPipe
    {
        public object Run(dynamic input, Action<string> progress)
        {
            string text = input;

            return text.Split(' ');
        }
    }
}
