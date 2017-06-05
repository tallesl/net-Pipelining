namespace PipeliningLibrary.Friendly
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using XpandoLibrary;

    /// <summary>
    /// A serialize-friendly version of PipelineResult.
    /// </summary>
    [Serializable]
    public class FriendlyPipelineResult
    {
        private const string _message = "The object is not seriazable (System.Type.IsSerializable).";

        /// <summary>
        /// Constructs a serialize-friendly PipelineResult.
        /// A string with a proper message is used to replace non-serializable values.
        /// </summary>
        /// <param name="pipelineResult">PipelineResult to make serialize-friendly.</param>
        public FriendlyPipelineResult(PipelineResult pipelineResult) : this(pipelineResult, _message) { }

        /// <summary>
        /// Constructs a serialize-friendly PipelineResult.
        /// A string with a proper message is used to replace non-serializable values.
        /// </summary>
        /// <param name="pipelineResult">PipelineResult to make serialize-friendly.</param>
        /// <param name="replacement">Replacement to use when a non-serializable value is found</param>
        public FriendlyPipelineResult(PipelineResult pipelineResult, object replacement)
        {
            Id = pipelineResult.Id;
            Success = pipelineResult.Success;
            ElapsedTime = pipelineResult.ElapsedTime;
            Pipes = pipelineResult.Pipes.Select((p, i) => new FriendlyPipeResult(p, i)).ToArray();

            // if it's null
            if (pipelineResult.Output == null)

                // is also null on the friendly result
                Output = null;

            // if it's an ExpandoObject
            else if (pipelineResult.Output is ExpandoObject)

                // we navigate the whole property tree replacing with the "not serializable" message when needed
                Output = CopyWithSerializableOnly((ExpandoObject)pipelineResult.Output, replacement);

            // if it's serializable
            else if (pipelineResult.Output.GetType().IsSerializable)

                // then we use it
                Output = pipelineResult.Output;

            // if it's not null, not an ExpandoObject and not serializable
            else

                // we use the replacement instead
                Output = replacement;
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
        public FriendlyPipeResult[] Pipes { get; set; }

        private static ExpandoObject CopyWithSerializableOnly(ExpandoObject expando, object replacement)
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

                    // if the current value is not an ExpandoObject
                    if (innerExpando == null)
                    {
                        // if it's not serializable
                        if (!kvp.Value.GetType().IsSerializable)

                            // we use the replacement
                            dict[kvp.Key] = replacement;
                    }
                    else
                    {
                        // if it's an ExpandoObject and is not empty
                        if (!innerExpando.Empty())

                            // recursion!
                            replace(innerExpando);
                    }
                });
            };

            var copy = expando.DeepCopy();
            replace(copy);

            return copy;
        }
    }
}
