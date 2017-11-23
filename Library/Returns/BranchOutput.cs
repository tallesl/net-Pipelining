namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Output of a branch pipe.
    /// Contains an ID of the pipeline to branch to and an output object.
    /// A null ID is equivalent of a PipelineEnd output.
    /// </summary>
    public class BranchOutput
    {
        /// <summary>
        /// Constructor used when it's suppose to end the pipeline.
        /// Sets null as the output.
        /// </summary>
        public BranchOutput() : this((string)null, new PipelineEnd(null)) { }

        /// <summary>
        /// Constructor used when it's suppose to end the pipeline.
        /// </summary>
        /// <param name="output">Output object of the pipeline</param>
        public BranchOutput(object output) :
            this((string)null, output is PipelineEnd ? output : new PipelineEnd(output)) { }

        /// <summary>
        /// Constructor used to branch the pipeline execution.
        /// Sets null as the output.
        /// </summary>
        /// <param name="id">ID of the pipeline to branch to</param>
        public BranchOutput(string id) : this(id, null) { }

        /// <summary>
        /// Constructor used to branch the pipeline execution.
        /// </summary>
        /// <param name="id">ID of the pipeline to branch to</param>
        /// <param name="output">Output object of the pipeline</param>
        public BranchOutput(string id, object output)
        {
            var idGiven = id != null;
            var end = output is PipelineEnd;

            if (idGiven && end)
                throw new InvalidBranchingException(
                    "You can't branch to a pipeline and output a PipelineEnd at the same time.");

            if (!idGiven && !end)
                output = new PipelineEnd(output);

            Id = id;
            Output = output;
        }

        /// <summary>
        /// Constructor used to branch the pipeline execution.
        /// Sets null as the output.
        /// </summary>
        /// <param name="id">ID of the pipeline to branch to</param>
        public BranchOutput(Enum id) : this(id, null) { }

        /// <summary>
        /// Constructor used to branch the pipeline execution.
        /// </summary>
        /// <param name="id">ID of the pipeline to branch to</param>
        /// <param name="output">Output object of the pipeline</param>
        public BranchOutput(Enum id, object output) : this(id.ToString(), output) { }

        // ID of the pipeline to branch to. Null if output is PipelineEnd.
        internal string Id { get; private set; }

        // Output object. Can be null. Can be PipelineEnd (Id should be null in this case).
        internal object Output { get; private set; }
    }
}
