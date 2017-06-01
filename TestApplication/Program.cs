namespace PipeliningLibrary.TestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Constructing the pipeline group.
            // All the pipeline registering magic goes there.
            var pipelines = new GuessTheNumberPipelines();

            // Here we are both picking the pipeline by its ID and running it.
            // It returns the output of the pipeline, but we're ignoring it this time.
            pipelines["guess_the_number"].Run();

            // You can also run a pipeline asynchronously (RunAsync), with a detailed result (RunDetailed), both
            // asynchronously and with a detailed result (RunDetailedAsync) and also as a enumerable that yields the
            // output of each pipe run (RunAsEnumerable).
        }
    }
}