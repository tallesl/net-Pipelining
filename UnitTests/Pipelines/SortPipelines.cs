namespace PipeliningLibrary.UnitTests
{
    public class SortPipelines : PipelineGroup
    {
        public SortPipelines()
        {
            Pipeline("bubble_sort")
                .Pipe<BubbleSortPipe>()
                .Pipe("bubble_sort");
        }
    }
}
