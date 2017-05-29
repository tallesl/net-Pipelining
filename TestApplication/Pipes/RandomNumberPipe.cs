namespace PipeliningLibrary.TestApplication
{
    using System;

    public class RandomNumberPipe : IPipe
    {
        private readonly int _min;

        private readonly int _max;

        public RandomNumberPipe(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public object Run(dynamic input)
        {
            // no need to Type

            // Act
            Console.WriteLine("I thought of a number in the range from {0} to {1}.", _min, _max);

            // Return
            return new Random().Next(_min, _max + 1);
        }
    }

}