namespace PipeliningLibrary
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using XpandoLibrary;

    public sealed partial class Pipeline
    {
        private static bool _forceExpando = false;

        private static TaskScheduler _scheduler = TaskScheduler.Default;

        private static readonly ConcurrentDictionary<string, Pipeline> Pipelines =
            new ConcurrentDictionary<string, Pipeline>();

        /// <summary>
        /// Forces the input object to be an ExpandoObject.
        /// </summary>
        public static void ForceExpando()
        {
            _forceExpando = true;
        }

        /// <summary>
        /// Sets a TaskScheduler to be used.
        /// </summary>
        /// <param name="scheduler">TaskScheduler to use</param>
        public static void WithScheduler(TaskScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        /// <summary>
        /// Runs the pipeline of the given identifier.
        /// </summary>
        /// <param name="id">Pipeline identifier</param>
        /// <param name="input">Input for the pipeline</param>
        /// <param name="progress">Optional action to be called with pipeline events</param>
        /// <param name="scheduler">TaskScheduler to use for this run</param>
        /// <returns>A Task of the running pipeline with gives a PipelineResult</returns>
        public static Task<PipelineResult> Run(string id, object input = null,
            Action<PipelineEvent> progress = null, TaskScheduler scheduler = null)
        {
            var pipeline = Get(id);

            if (_forceExpando)
                input = (input ?? new object()).ToExpando();

            progress = progress ?? (p => { });

            scheduler = scheduler ?? _scheduler;

            return pipeline.Run(input, progress, scheduler);
        }

        /// <summary>
        /// Register a new pipeline.
        /// </summary>
        /// <param name="id">Pipeline identifier</param>
        /// <returns>The registered pipeline instance</returns>
        public static Pipeline Register(string id)
        {
            var pipeline = new Pipeline(id);

            if (!Pipelines.TryAdd(id, pipeline))
                throw new PipelineAlreadyRegisteredException(id);

            return pipeline;
        }

        internal static Pipeline Get(string id)
        {
            Pipeline pipeline;

            if (!Pipelines.TryGetValue(id, out pipeline))
                throw new PipelineNotFoundException(id);

            return pipeline;
        }
    }
}
