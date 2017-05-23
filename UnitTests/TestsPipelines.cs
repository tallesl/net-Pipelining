namespace Pipelining.UnitTests
{
    using Pipelining.UnitTests.Pipes;
    using PipeliningLibrary;

    public class TestsPipelines : PipelineGroup
    {
        public TestsPipelines()
        {
            Register("sanitize_input")
                .Pipe<RemoveNonAlphaPipe>()
                .Pipe<RemoveCasePipe>();

            Register("extract_keywords")
                .Pipe("sanitize_input")
                .Pipe<SplitIntoWordsPipe>()
                .Pipe<DeduplicateWordsPipe>()
                .Pipe<RemoveStopWordsPipe>()
                .Pipe<SortAlphabeticallyPipe>();

            Register("extract_keywords_exception")
                .Pipe("sanitize_input")
                .Pipe<ExceptionPipe>()
                .Pipe<SplitIntoWordsPipe>()
                .Pipe<DeduplicateWordsPipe>()
                .Pipe<RemoveStopWordsPipe>()
                .Pipe<SortAlphabeticallyPipe>();
        }
    }
}
