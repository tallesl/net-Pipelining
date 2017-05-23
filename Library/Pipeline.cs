namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Builds and runs pipelines out of pipes.
    /// </summary>
    public sealed partial class Pipeline
    {
        private readonly string _id;

        private readonly PipelineGroup _pipelines;

        internal readonly IList<PipeSpecifier> Pipes;

        internal Pipeline(PipelineGroup pipelines, string id)
        {
            _id = id;
            _pipelines = pipelines;
            Pipes = new List<PipeSpecifier>();
        }

        /// <summary>
        /// Gets the pipes from the given pipeline and sets on this one.
        /// </summary>
        /// <param name="id">Id of the pipeline to get the pipes from</param>
        /// <returns>This pipeline instance</returns>
        public Pipeline Pipe(string id)
        {
            Pipes.Add(new PipeSpecifier(_pipelines, id));
            return this;
        }

        /// <summary>
        /// Sets the next pipe in the pipeline chain.
        /// </summary>
        /// <typeparam name="T">The IPipe to set</typeparam>
        /// <returns>This pipeline instance</returns>
        public Pipeline Pipe<T>() where T : IPipe, new()
        {
            Pipes.Add(new PipeSpecifier(typeof(T)));
            return this;
        }

        /// <summary>
        /// Sets the next pipe in the pipeline chain.
        /// </summary>
        /// <param name="pipe">The IPipe to set</typeparam>
        /// <returns>This pipeline instance</returns>
        public Pipeline Pipe(IPipe pipe)
        {
            Pipes.Add(new PipeSpecifier(pipe));
            return this;
        }

        internal Task<PipelineResult> Run(object input, Action<PipelineEvent> progress, TaskScheduler scheduler)
        {
            return Task.Factory.StartNew<PipelineResult>(() =>
            {
                object output = null;
                var results = new List<PipeResult>();

                var i = 0;
                var pipes = Pipes.SelectMany(p => p.Resolve());

                foreach (var pipe in pipes)
                {
                    #region notifying start

                    progress(
                        new PipeStarted
                        {
                            Pipe = pipe.GetType(),
                            Current = i,
                        }
                    );

                    #endregion

                    var start = DateTime.UtcNow;

                    try
                    {
                        #region running the pipe

                        output = pipe.Run(
                            input,
                            (message) => progress(
                                new PipeMessage
                                {
                                    Pipe = pipe.GetType(),
                                    Message = message,
                                    Current = i,
                                }
                            )
                        );

                        #endregion

                        #region success result

                        results.Add(
                            new PipeResult
                            {
                                Pipe = pipe.GetType(),
                                Started = start,
                                Ended = DateTime.UtcNow,
                            }
                        );

                        input = output;

                        #endregion

                        #region notifying end

                        progress(
                            new PipeEnded
                            {
                                Pipe = pipe.GetType(),
                                Current = i,
                            }
                        );

                        #endregion
                    }
                    catch (Exception e)
                    {
                        #region error result

                        results.Add(
                            new PipeResult
                            {
                                Pipe = pipe.GetType(),
                                Started = start,
                                Ended = DateTime.UtcNow,
                                Exception = e,
                            }
                        );

                        #endregion

                        #region notifying exception

                        progress(
                            new PipeException
                            {
                                Exception = e,
                                Pipe = pipe.GetType(),
                                Current = i,
                            }
                        );

                        #endregion

                        break;
                    }

                    ++i;
                }

                return new PipelineResult(_id, output, results.ToArray());
            }, CancellationToken.None, TaskCreationOptions.None, scheduler);
        }
    }
}
