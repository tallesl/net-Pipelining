namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Exception thrown by a running pipe.
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
