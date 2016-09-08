namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Base class for pipeline events.
    /// </summary>
    [Serializable]
    public abstract class PipelineEvent
    {
        /// <summary>
        /// Type of the pipe.
        /// </summary>
        public Type Pipe { get; set; }

        /// <summary>
        /// Position of the pipe in the pipeline.
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// Total of pipes in the pipeline.
        /// </summary>
        public int Total { get; set; }
    }
}
