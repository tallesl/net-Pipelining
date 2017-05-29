namespace PipeliningLibrary.TestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pipelines = new GuessTheNumberPipelines();
            pipelines["guess_a_number"].Run();
        }
    }
}