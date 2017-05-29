namespace PipeliningLibrary.TestApplication
{
    using System;

    public class CheckAnswerPipe : IBranchPipe
    {
        public BranchOutput Run(dynamic input)
        {
            // Type
            Tuple<int, string> tuple = input;
            var number = tuple.Item1;
            var guess = tuple.Item2;

            // Act
            int answer;
            var pipeline = int.TryParse(guess, out answer) ? (answer == number ? "success" : "failure") : "invalid";

            // Return
            return new BranchOutput(pipeline, number);
        }
    }
}