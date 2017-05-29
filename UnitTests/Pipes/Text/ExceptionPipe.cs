namespace PipeliningLibrary.UnitTests
{
    using System;

    public class ExceptionPipe : IPipe
    {
        public object Run(dynamic input)
        {
            throw new InvalidOperationException("Exception thrown on purpose.");
        }
    }
}
