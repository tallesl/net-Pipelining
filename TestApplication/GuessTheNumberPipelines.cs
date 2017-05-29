namespace PipeliningLibrary.TestApplication
{
    using PipeliningLibrary;

    public class GuessTheNumberPipelines : PipelineGroup
    {
        public GuessTheNumberPipelines()
        {
            Pipeline("guess_a_number")
                .Pipe(new RandomNumberPipe(1 , 10))

                .Pipeline("divisible", p => p
                    .Pipe(new DivisibleBy(2))
                    .Pipe(new DivisibleBy(3))
                    .Pipe(new DivisibleBy(4))
                    .Pipe(new DivisibleBy(5)))

                .Pipeline("ask", p => p
                    .Pipe<AskAnswerPipe>()
                    .BranchPipe<CheckAnswerPipe>()

                        .BranchTo("success", _p => _p
                            .Pipe<SuccessPipe>()
                            .Pipeline("try_again", __p => __p
                                .BranchPipe<TryAgainPipe>()
                                .BranchTo("guess_a_number")))

                        .BranchTo("failure", _p => _p
                            .Pipe<FailurePipe>()
                            .Pipe("try_again"))

                        .BranchTo("invalid", _p => _p
                            .Pipe<InvalidAnswerPipe>()
                            .Pipe("ask")));
        }
    }
}