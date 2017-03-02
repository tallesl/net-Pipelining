﻿namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using XpandoLibrary;

    /// <summary>
    /// Result from a pipeline run.
    /// </summary>
    [Serializable]
    public class PipelineResult
    {
        private const string _notSerializableMessage = "The object is not seriazable (System.Type.IsSerializable).";

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

            // if it's null
            if (Output == null)

                // is also null on the friendly result
                output = null;

            // if it's an ExpandoObject
            else if (Output is ExpandoObject)

                // we navigate the whole property tree replacing with the "not serializable" message when needed
                output = CopyWithSerializableOnly((ExpandoObject)Output);

            // if it's serializable
            else if (Output.GetType().IsSerializable)

                // then we use it
                output = Output;

            // if it's not null, not an ExpandoObject and not serializable
            else

                // we use the "not serializable" message instead
                output = _notSerializableMessage;

            return new FriendlyPipelineResult
            {
                Id = Id,
                Output = output,
                Success = Success,
                ElapsedTime = ElapsedTime,
                Pipes = Pipes.Select((p, i) => p.Friendly(i)).ToList(),
            };
        }

        private static ExpandoObject CopyWithSerializableOnly(ExpandoObject expando)
        {
            Action<ExpandoObject> replace = null;
            replace = e =>
            {
                // using it as a dictionary
                var dict = (IDictionary<string, object>)e;

                // for each key value pair
                dict.ToList().ForEach(kvp =>
                {
                    var innerExpando = kvp.Value as ExpandoObject;

                    // if the current value is an ExpandoObject
                    if (innerExpando != null)

                        // recursion!
                        replace(e);

                    // if it's not an Expando and it's not serializable
                    else if (kvp.Value != null && !kvp.Value.GetType().IsSerializable)

                        // we use the "not serializable" message instead
                        dict[kvp.Key] = _notSerializableMessage;
                });
            };

            var copy = expando.DeepCopy();
            replace(copy);

            return copy;
        }
    }
}
