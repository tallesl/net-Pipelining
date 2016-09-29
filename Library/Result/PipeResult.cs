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

        /// <summary>
        /// Returns a serialize-friendly result.
        /// </summary>
        /// <param name="position">Position of the pipe in the pipeline</param>
        /// <returns>A serialize-friendly result</returns>
        public FriendlyPipeResult Friendly(int position)
        {
            return new FriendlyPipeResult
            {
                Position = position,
                Pipe = Pipe.ToString(),
                Started = Started,
                Ended = Ended,
                Exception = Exception == null ? null : Exception.ToString(),
            };
        }
    }
}
