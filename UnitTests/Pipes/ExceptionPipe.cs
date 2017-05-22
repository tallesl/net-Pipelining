namespace Pipelining.UnitTests.Pipes
{
    using PipeliningLibrary;
    using System;

    public class ExceptionPipe : IPipe
    {
        public object Run(dynamic input, Action<string> progress)
        {
            throw new InvalidOperationException("Exception thrown on purpose.");
        }
    }
}
