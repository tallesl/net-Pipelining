namespace PipeliningLibrary
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PipelineGroup
    {
        private readonly ConcurrentDictionary<string, Pipeline> _pipelines =
            new ConcurrentDictionary<string, Pipeline>();

        /// <summary>
        /// Returns an IEnumerable which each iteration runs the next pipe in the pipeline.
        /// The "Current" property of the enumerable is the output of the last pipe run.
        /// </summary>
        /// <param name="id">Pipeline identifier</param>
        /// <param name="input">Input for the pipeline</param>
        /// <returns>An IEnumerable which each iteration runs the next pipe in the pipeline.</returns>
        public IEnumerable<object> GetEnumerable(string id, object input = null)
        {
            return new PipeEnumerator(input, Get(id).Pipes);
        }

        /// <summary>
        /// Register a new pipeline.
        /// </summary>
        /// <param name="id">Pipeline identifier</param>
        /// <returns>The registered pipeline instance</returns>
        public Pipeline Pipeline(string id)
        {
            var pipeline = new Pipeline(this, id);

            if (!_pipelines.TryAdd(id, pipeline))
                throw new PipelineAlreadyRegisteredException(id);

            return pipeline;
        }

        /// <summary>
        /// Runs the pipeline of the given identifier.
        /// </summary>
        /// <param name="id">Pipeline identifier</param>
        /// <param name="input">Input for the pipeline</param>
        /// <param name="progress">Optional action to be called with pipeline events</param>
        /// <param name="scheduler">TaskScheduler to use for this run</param>
        /// <returns>A Task of the running pipeline with gives a PipelineResult</returns>
        public Task<PipelineResult> Run(string id, object input = null,
            Action<PipelineEvent> progress = null, TaskScheduler scheduler = null)
        {
            return Get(id).Run(input, progress ?? (p => { }), scheduler ?? TaskScheduler.Default);
        }

        internal Pipeline Get(string id)
        {
            Pipeline pipeline;

            if (!_pipelines.TryGetValue(id, out pipeline))
                throw new PipelineNotFoundException(id);

            return pipeline;
        }
    }
}
