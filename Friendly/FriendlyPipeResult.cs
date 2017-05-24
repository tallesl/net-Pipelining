namespace PipeliningLibrary.Friendly
{
    using System;

    /// <summary>
    /// A serialize-friendly version of PipeResult.
    /// </summary>
    [Serializable]
    public class FriendlyPipeResult
    {
        /// <summary>
        /// Constructs a serialize-friendly PipeResult.
        /// </summary>
        /// <param name="pipeResult">PipeResult to make serialize-friendly</param>
        /// <param name="position">Position of the pipe in the pipeline</param>
        public FriendlyPipeResult(PipeResult pipeResult, int position)
        {
            Position = position;
            Pipe = pipeResult.Pipe.ToString();
            Started = pipeResult.Started;
            Ended = pipeResult.Ended;
            Exception = pipeResult.Exception == null ? null : pipeResult.Exception.ToString();
        }

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

        /// <summary>
        /// The position of the pipe in the pipeline (1 is the first).
        /// </summary>
        public int Position { get; set; }
    }
}
