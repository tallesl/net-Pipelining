namespace PipeliningLibrary.UnitTests
{
    public class TextPipelines : PipelineGroup
    {
        public TextPipelines()
        {
            Pipeline("sanitize_input")
                .Pipe<RemoveNonAlphaPipe>()
                .Pipe<RemoveCasePipe>();

            Pipeline("extract_keywords")
                .BranchPipe<PickLanguagePipe>()
                    .BranchTo(
                        "extract_keywords_en",
                        p => p.Pipe("sanitize_input")
                            .Pipe<SplitIntoWordsPipe>()
                            .Pipe<DeduplicateWordsPipe>()
                            .Pipe(new RemoveStopWordsPipe(StopWords.English))
                            .Pipe<SortAlphabeticallyPipe>()
                    )
                    .BranchTo(
                        "extract_keywords_es",
                        p => p.Pipe("sanitize_input")
                            .Pipe<SplitIntoWordsPipe>()
                            .Pipe<DeduplicateWordsPipe>()
                            .Pipe(new RemoveStopWordsPipe(StopWords.Spanish))
                            .Pipe<SortAlphabeticallyPipe>()
                    )
                    .BranchTo(
                        "extract_keywords_pt",
                        p => p.Pipe("sanitize_input")
                            .Pipe<SplitIntoWordsPipe>()
                            .Pipe<DeduplicateWordsPipe>()
                            .Pipe(new RemoveStopWordsPipe(StopWords.Portuguese))
                            .Pipe<SortAlphabeticallyPipe>()
                    );

            Pipeline("extract_keywords_ex")
                .Pipe("sanitize_input")
                .Pipe<ExceptionPipe>()
                .Pipe<SplitIntoWordsPipe>()
                .Pipe<DeduplicateWordsPipe>()
                .Pipe(new RemoveStopWordsPipe(StopWords.English))
                .Pipe<SortAlphabeticallyPipe>();
        }
    }
}
