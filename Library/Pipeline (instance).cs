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

        internal readonly IList<IPipe> _pipes;

        internal Pipeline(string id)
        {
            _id = id;
            _pipes = new List<IPipe>();
        }

        /// <summary>
        /// Sets the next pipe in the pipeline chain.
        /// </summary>
        /// <param name="pipe">The IPipe to set</typeparam>
        /// <returns>This pipeline instance</returns>
        public Pipeline Pipe(IPipe pipe)
        {
            _pipes.Add(pipe);
            return this;
        }

        /// <summary>
        /// Sets the next pipe in the pipeline chain.
        /// </summary>
        /// <typeparam name="T">The IPipe to set</typeparam>
        /// <returns>This pipeline instance</returns>
        public Pipeline Pipe<T>() where T : IPipe, new()
        {
            _pipes.Add(new T());
            return this;
        }

        /// <summary>
        /// Gets the pipes from the given pipeline and sets on this one.
        /// </summary>
        /// <param name="id">Id of the pipeline to get the pipes from</param>
        /// <returns>This pipeline instance</returns>
        public Pipeline Pipe(string id)
        {
            var pipeline = Get(id);

            foreach (var pipe in pipeline._pipes)
                _pipes.Add(pipe);

            return this;
        }

        internal Task<PipelineResult> Run(object input, Action<PipelineEvent> progress, TaskScheduler scheduler)
        {
            return Task.Factory.StartNew<PipelineResult>(() =>
            {
                object output = null;
                var results = new List<PipeResult>();

                for (var i = 0; i < _pipes.Count; ++i)
                {
                    var pipe = _pipes[i];

                    if (results.Any(r => r.Exception != null))
                    {
                        #region not executed result

                        results.Add(
                            new PipeResult
                            {
                                Pipe = pipe.GetType(),
                            }
                        );

                        #endregion
                    }
                    else
                    {
                        #region notifying start

                        progress(
                            new PipeStarted
                            {
                                Pipe = pipe.GetType(),
                                Current = i,
                                Total = _pipes.Count,
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
                                        Total = _pipes.Count,
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
                                    Total = _pipes.Count,
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
                                    Total = _pipes.Count,
                                }
                            );

                            #endregion
                        }
                    }
                }

                return new PipelineResult(_id, output, results);
            }, CancellationToken.None, TaskCreationOptions.None, scheduler);
        }
    }
}
