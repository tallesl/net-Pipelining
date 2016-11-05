﻿namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;
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
            Pipes = new List<PipeResult>();
        }

        internal PipelineResult(string id, object output, IList<PipeResult> results)
        {
            Id = id;
            Output = output;
            Success = results.All(r => r.Exception == null);
            ElapsedTime = results.Any() ? results.Last(r => r.Ended != null).Ended.Value -
                    results.First().Started.Value : TimeSpan.Zero;
            Pipes = results;
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
        public IList<PipeResult> Pipes { get; set; }

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

        /// <summary>
        /// Returns a serialize-friendly result.
        /// </summary>
        /// <returns>A serialize-friendly result</returns>
        public FriendlyPipelineResult Friendly()
        {
            object output;

            if (Output == null)
            {
                output = null;
            }
            else if (Output.GetType().IsSerializable)
            {
                output = Output;
            }
            else
            {
                output = "The output object is not seriazable (System.Type.IsSerializable).";
            }

            return new FriendlyPipelineResult
            {
                Id = Id,
                Output = output,
                Success = Success,
                ElapsedTime = ElapsedTime,
                Pipes = Pipes.Select((p, i) => p.Friendly(i)).ToList(),
            };
        }
    }
}
