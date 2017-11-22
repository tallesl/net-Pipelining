namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;

    // Represents an ongoing pipeline detailed run.
    internal class PipelineDetailedRun : PipelineRun
    {
        // Ctor accepting the input object, the pipeline been run and an action provided by the library user to notify
        // any progress.
        internal PipelineDetailedRun(object input, Pipeline pipeline, Action<PipelineEvent> progress)
            : base(input, pipeline)
        {
            Progress = progress;
            Results = new List<PipeResult>();
        }

        // An action provided by the library user to notify any progress (pipe started, error'ed, ended, ...).
        internal Action<PipelineEvent> Progress { get; set; }

        // Results of the pipes the already executed pipes.
        internal IList<PipeResult> Results { get; set; }

        // Runs a single pending pipe.
        internal override void RunOne()
        {
            var pipe = PendingPipes.Current;
            var start = DateTime.UtcNow;
            var type = pipe is BranchPipeWrapper ? ((BranchPipeWrapper)pipe).Type : pipe.GetType();
            Func<int> index = () => Results.Count + 1;

            try
            {
                #region notifying start

                Progress?.Invoke(
                    new PipeStarted
                    {
                        Pipe = type,
                        Current = index(),
                    }
                );

                #endregion

                #region running the pipe

                base.RunOne();

                #endregion

                #region notifying end

                Progress?.Invoke(
                    new PipeEnded
                    {
                        Pipe = type,
                        Current = index(),
                    }
                );

                #endregion

                #region success result

                Results.Add(
                    new PipeResult
                    {
                        Pipe = type,
                        Started = start,
                        Ended = DateTime.UtcNow,
                    }
                );

                #endregion
            }
            catch (Exception e)
            {
                #region error result

                Results.Add(
                    new PipeResult
                    {
                        Pipe = type,
                        Started = start,
                        Ended = DateTime.UtcNow,
                        Exception = e,
                    }
                );

                #endregion

                #region notifying exception

                Progress?.Invoke(
                    new PipeException
                    {
                        Exception = e,
                        Pipe = type,
                        Current = index(),
                    }
                );

                #endregion

                ClearPending();
            }
        }
    }
}
