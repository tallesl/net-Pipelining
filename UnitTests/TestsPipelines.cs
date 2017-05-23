namespace Pipelining.UnitTests
{
    using Pipelining.UnitTests.Pipes;
    using PipeliningLibrary;

    public class TestsPipelines : PipelineGroup
    {
        public TestsPipelines()
        {
            Pipeline("empty");

            Pipeline("sanitize_input")
                .Pipe<RemoveNonAlphaPipe>()
                .Pipe<RemoveCasePipe>();

            Pipeline("extract_keywords")
                .Pipe("sanitize_input")
                .Pipe<SplitIntoWordsPipe>()
                .Pipe<DeduplicateWordsPipe>()
                .Pipe<RemoveStopWordsPipe>()
                .Pipe<SortAlphabeticallyPipe>();

            Pipeline("extract_keywords_exception")
                .Pipe("sanitize_input")
                .Pipe<ExceptionPipe>()
                .Pipe<SplitIntoWordsPipe>()
                .Pipe<DeduplicateWordsPipe>()
                .Pipe<RemoveStopWordsPipe>()
                .Pipe<SortAlphabeticallyPipe>();
        }
    }
}
