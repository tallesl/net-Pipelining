namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// A pipeline event.
    /// </summary>
    [Serializable]
    public abstract class PipelineEvent
    {
        /// <summary>
        /// Type of the pipe.
        /// </summary>
        public Type Pipe { get; set; }

        /// <summary>
        /// Index of the pipe in the pipeline (starts with 1).
        /// </summary>
        public int Current { get; set; }
    }
}
