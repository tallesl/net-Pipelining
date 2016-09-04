namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Result from an IPipe run.
    /// </summary>
    [Serializable]
    public class PipeResult
    {
        /// <summary>
        /// Type of the pipe.
        /// </summary>
        public Type Pipe { get; set; }

        /// <summary>
        /// When it started.
        /// </summary>
        public DateTime? Started { get; set; }

        /// <summary>
        /// When it ended.
        /// </summary>
        public DateTime? Ended { get; set; }

        /// <summary>
        /// Exception caught while running it.
        /// </summary>
        public Exception Exception { get; set; }
    }
}
