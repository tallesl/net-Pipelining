namespace PipeliningLibrary.TestApplication
{
    using System;

    public class SuccessPipe : IPipe
    {
        public object Run(dynamic input)
        {
            // no need to Type

            // Act
            Console.WriteLine("Yes! That was it!");

            // Return
            return input;
        }
    }
}
