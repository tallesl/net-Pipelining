namespace PipeliningLibrary.TestApplication
{
    using System;

    public class TryAgainPipe : IBranchPipe
    {
        public BranchOutput Run(dynamic input)
        {
            // no need to Type

            // Act
            Console.WriteLine("Do you want to play again? [y/n]");
            var read = Console.ReadLine();
            var yes = !string.IsNullOrEmpty(read) && char.ToLowerInvariant(read[0]) == 'y';

            // Return
            return yes ? new BranchOutput("guess_the_number") : new BranchOutput();
        }
    }
}