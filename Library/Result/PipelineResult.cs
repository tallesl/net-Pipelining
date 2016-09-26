namespace PipeliningLibrary
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
    }
}
