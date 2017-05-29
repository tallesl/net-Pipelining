namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A pipeline runner.
    /// </summary>
    public class PipelineRunner
    {
        // The pipeline been ran.
        private readonly Pipeline _pipeline;

        // Ctor accepting the pipeline to run.
        internal PipelineRunner(Pipeline pipeline)
        {
            _pipeline = pipeline;
        }

        /// <summary>
        /// Runs this pipeline.
        /// </summary>
        /// <param name="input">Input for this run (optional)</param>
        /// <returns>The output of this run</returns>
        public object Run(object input = null)
        {
            var run = new PipelineRun(input, _pipeline);
            run.RunAll();
            return run.Output;
        }

        /// <summary>
        /// Runs this pipeline as a task giving the output as result.
        /// </summary>
        /// <param name="input">Input for this run (optional)</param>
        /// <param name="taskFactory">Task factory to use to create this task (optional)</param>
        /// <returns>A task that gives you the result of the run</returns>
        public Task<object> RunAsync(object input = null, TaskFactory taskFactory = null)
        {
            return (taskFactory ?? Task.Factory).StartNew<object>(() => Run(input));
        }

        /// <summary>
        /// Runs this pipeline giving detailed information as result.
        /// </summary>
        /// <param name="input">Input for this run (optional)</param>
        /// <param name="progress">An action to notify about pipe events (optional)</param>
        /// <returns>A detailed result of the run</returns>
        public PipelineResult RunDetailed(object input = null, Action<PipelineEvent> progress = null)
        {
            var run = new PipelineDetailedRun(input, _pipeline, progress);
            run.RunAll();
            return new PipelineResult(run);
        }

        /// <summary>
        /// Runs this pipeline as a task giving detailed information as result.
        /// </summary>
        /// <param name="input">Input for this run (optional)</param>
        /// <param name="progress">An action to notify about pipe events (optional)</param>
        /// <param name="taskFactory">Task factory to use to create this task (optional)</param>
        /// <returns>A task that gives you a detailed result of the run</returns>
        public Task<PipelineResult> RunDetailedAsync(
            object input = null, Action<PipelineEvent> progress = null, TaskFactory taskFactory = null)
        {
            return (taskFactory ?? Task.Factory).StartNew<PipelineResult>(() => RunDetailed(input, progress));
        }

        /// <summary>
        /// Runs this pipeline as an enumerable.
        /// Each iteration runs a pipe and yields a output.
        /// </summary>
        /// <param name="input">Input for this run (optional)</param>
        /// <returns>An enumerable with pipe outputs</returns>
        public IEnumerable<object> RunAsEnumerable(object input = null)
        {
            return new PipeEnumerator(_pipeline, input);
        }
    }
}
