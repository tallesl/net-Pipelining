namespace PipeliningLibrary
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    // Represents an ongoing pipeline run.
    internal class PipelineRun
    {
        // Current output object.
        private object _output;

        // Ctor accepting the input object and the pipeline been run.
        internal PipelineRun(object input, Pipeline pipeline)
        {
            Output = input;
            Pipeline = pipeline;
            PendingPipes = pipeline.Pipes.GetEnumerator();
        }

        // Output object accessor.
        // When a PipelineEnd is set, it automatically unwraps the output object and empties the pending pipes to run.
        internal object Output
        {
            get
            {
                return _output;
            }

            set
            {
                if (value is PipelineEnd)
                {
                    _output = ((PipelineEnd)value).Output;
                    ClearPending();
                }
                else
                {
                    _output = value;
                }
            }
        }

        // Pipeline of this run.
        internal Pipeline Pipeline { get; set; }

        // Pending to run pipes of this run.
        internal IEnumerator<IBasePipe> PendingPipes { get; set; }

        // Empties the pending to run pipes of this run.
        internal void ClearPending() => PendingPipes = Enumerable.Empty<IBasePipe>().GetEnumerator();

        // Runs all pending pipes.
        internal void RunAll()
        {
            while (PendingPipes.MoveNext())
                RunOne();
        }

        // Runs a single pending pipe.
        internal virtual void RunOne()
        {
            // current pipe been run
            var currentPipe = PendingPipes.Current;

            // if it's of type IPipe
            if (currentPipe is IPipe)
            {
                // casting to its proper type
                var pipe = (IPipe)currentPipe;

                // runs the pipe making the current pipeline output its input
                // also sets the output of this pipeline run as the output got of this pipe run
                Output = pipe.Run(Output);
            }

            // if it's of type BranchPipeWrapper
            else if (currentPipe is BranchPipeWrapper)
            {
                // casting to its proper type
                var branchPipe = (BranchPipeWrapper)currentPipe;

                // casting to IBranchPipe so it can access Run
                // runs the pipe making the curren pipeline output its input
                var branchResult = ((IBranchPipe)branchPipe).Run(Output);

                // sets the output of this pipeline run as the output got of this pipe run
                Output = branchResult.Output;

                // if there's a pipeline ID in the result it means we have to branch it to another pipeline
                if (branchResult.Id != null)
                {
                    // if there's not restriction to branch to the pipeline we got
                    if (branchPipe.CanBranch(branchResult.Id))
                    {
                        // we get the pipeline to branch to
                        var pipeline = Pipeline.Group.Get(branchResult.Id);

                        // and set the pending pipes to run to be the branched pipeline pipes
                        PendingPipes = pipeline.Pipes.GetEnumerator();
                    }
                    else
                    {
                        // if we can't branch to the pipeline given
                        // an exception is thrown saying exactly that
                        throw new InvalidBranchingException(
                            string.Format("\"{0}\" is not part of the allowed branches.", branchResult.Id));
                    }
                }
            }

            // if it's any other type (all IBranchPipe should be BranchPipeWrapper)
            else
            {
                Trace.UnexpectedType(currentPipe.GetType());
            }
        }
    }
}
