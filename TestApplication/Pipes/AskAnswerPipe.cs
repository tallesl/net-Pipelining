namespace PipeliningLibrary.TestApplication
{
    using System;

    public class AskAnswerPipe : IPipe
    {
        public object Run(dynamic input)
        {
            // Type
            int number = input;

            // Act
            Console.Write("Guess it: ");
            var guess = Console.ReadLine();

            // Return
            return new Tuple<int, string>(number, guess);
        }
    }
}