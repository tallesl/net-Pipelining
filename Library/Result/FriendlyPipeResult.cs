namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// A serialize-friendly version of PipeResult.
    /// </summary>
    [Serializable]
    public class FriendlyPipeResult
    {
        /// <summary>
        /// The position of the pipe in the pipeline (1 is the first).
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Type of the pipe.
        /// </summary>
        public string Pipe { get; set; }

        /// <summary>
        /// When it started.
        /// </summary>
        public DateTime Started { get; set; }

        /// <summary>
        /// When it ended.
        /// </summary>
        public DateTime Ended { get; set; }

        /// <summary>
        /// Exception caught while running it.
        /// </summary>
        public string Exception { get; set; }
    }
}
