namespace PipeliningLibrary.TestApplication
{
    using System;

    public class InvalidAnswerPipe : IPipe
    {
        public object Run(dynamic input)
        {
            // no need to Type

            // Act
            Console.WriteLine("Type an integer!");

            // Return
            return input;
        }
    }
}