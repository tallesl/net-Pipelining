﻿namespace PipeliningLibrary
{
    using System;
    using System.Linq;

    /// <summary>
    /// Result from a pipeline run.
    /// </summary>
    [Serializable]
    public class PipelineResult
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public PipelineResult()
        {
            Pipes = new PipeResult[0];
        }

        internal PipelineResult(PipelineDetailedRun current)
        {
            Id = current.Pipeline.Id;
            Output = current.Output;
            Success = current.Results.All(r => r.Exception == null);

            ElapsedTime = current.Results.Any() ?
                current.Results.Last().Ended - current.Results.First().Started : TimeSpan.Zero;

            Pipes = current.Results.ToArray();
        }

        /// <summary>
        /// Pipeline ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Output of the run.
        /// </summary>
        public object Output { get; set; }

        /// <summary>
        /// Flag indicating success.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Elapsed time of the run.
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Individual pipe results.
        /// </summary>
        public PipeResult[] Pipes { get; set; }

        /// <summary>
        /// Returns the exception of this result, if any.
        /// If the success flag is true should return null.
        /// </summary>
        /// <returns>The exception of this result, if any</returns>
        public Exception Exception()
        {
            var result = Pipes.SingleOrDefault(p => p.Exception != null);
            return result == null ? null : result.Exception;
        }
    }
}
