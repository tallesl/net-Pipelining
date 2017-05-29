namespace PipeliningLibrary.TestApplication
{
    using System;

    public class FailurePipe : IPipe
    {
        public object Run(dynamic input)
        {
            // Type
            int number = input;

            // Act
            Console.WriteLine("Nope. It was {0}.", number);

            // Return
            return input;
        }
    }
}