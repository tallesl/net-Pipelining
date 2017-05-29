namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Event raised when a pipe throws an exception while running.
    /// </summary>
    [Serializable]
    public class PipeException : PipelineEvent
    {
        /// <summary>
        /// Exception thrown.
        /// </summary>
        public Exception Exception { get; set; }
    }
}
