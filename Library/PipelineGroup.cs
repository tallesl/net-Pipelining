namespace PipeliningLibrary
{
    using System.Collections.Concurrent;

    /// <summary>
    /// A group of pipelines that can reference each other.
    /// </summary>
    public class PipelineGroup
    {
        // The key is the pipeline ID, the value is the pipeline instance.
        private readonly ConcurrentDictionary<string, Pipeline> _pipelines =
            new ConcurrentDictionary<string, Pipeline>();

        /// <summary>
        /// Register a new pipeline.
        /// </summary>
        /// <param name="id">Pipeline ID</param>
        /// <returns>The registered pipeline instance</returns>
        /// <exception cref="IdExistsException">
        /// If there is already a pipeline with the given id in this group
        /// </exception>
        public PipelineBuilder Pipeline(string id)
        {
            var pipeline = new Pipeline(id, this);

            if (!_pipelines.TryAdd(id, pipeline))
                throw new IdExistsException(id);

            return new PipelineBuilder(pipeline);
        }

        /// <summary>
        /// Gets an object to run the pipeline of the given id.
        /// </summary>
        /// <param name="id">Pipeline ID</param>
        /// <returns>An object to run the pipeline of the given id</returns>
        /// <exception cref="IdNotFoundException">
        /// If there is no pipeline with the given id in this group
        /// </exception>
        public PipelineRunner this[string id]
        {
            get
            {
                Pipeline pipeline;

                if (_pipelines.TryGetValue(id, out pipeline))
                    return new PipelineRunner(pipeline);
                else
                    throw new IdNotFoundException(id);
            }
        }

        // Return the pipeline of the given ID
        internal Pipeline Get(string id)
        {
            return _pipelines[id];
        }
    }
}
