namespace PipeliningLibrary.UnitTests.Pipelines
{
    using Pipes;

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
