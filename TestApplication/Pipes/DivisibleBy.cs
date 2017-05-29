namespace PipeliningLibrary.TestApplication
{
    using System;

    public class DivisibleBy : IPipe
    {
        private readonly int _number;

        public DivisibleBy(int number)
        {
            _number = number;
        }

        public object Run(dynamic input)
        {
            // Type
            int number = input;

            // Act
            if ((number % _number) == 0)
                Console.WriteLine("The number is divisible by {0}.", _number);

            // Return
            return input;
        }
    }
}
