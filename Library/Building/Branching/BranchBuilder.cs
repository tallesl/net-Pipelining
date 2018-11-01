namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Builds branch paths in pipelines.
    /// </summary>
    public class BranchBuilder : PipelineBuilder
    {
        // The pipeline been constructed.
        private readonly Pipeline _pipeline;

        // Ctor accepting the pipeline been constructed.
        internal BranchBuilder(Pipeline pipeline) : base(pipeline)
        {
            _pipeline = pipeline;
        }

        /// <summary>
        /// Sets that this pipeline can branch to specified pipeline.
        /// </summary>
        /// <param name="id">ID of the pipeline that it can branch to</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public BranchBuilder BranchTo(string id)
        {
            _pipeline.AddBranchRestriction(id);
            return this;
        }

        /// <summary>
        /// Sets that this pipeline can branch to specified pipeline.
        /// </summary>
        /// <param name="id">ID of the pipeline been constructed by this call</param>
        /// <param name="pipelineBuilder">An action that builds a whole new pipeline</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public BranchBuilder BranchTo(string id, Action<PipelineBuilder> pipelineBuilder)
        {
            _pipeline.AddBranchRestriction(id);
            pipelineBuilder(_pipeline.Group.Pipeline(id));
            return this;
        }

        /// <summary>
        /// Sets that this pipeline can branch to specified pipeline.
        /// </summary>
        /// <param name="id">ID of the pipeline that it can branch to</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public BranchBuilder BranchTo(Enum id) => BranchTo(id.ToString());


        /// <summary>
        /// Sets that this pipeline can branch to specified pipeline.
        /// </summary>
        /// <param name="id">ID of the pipeline been constructed by this call</param>
        /// <param name="pipelineBuilder">An action that builds a whole new pipeline</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public BranchBuilder BranchTo(Enum id, Action<PipelineBuilder> pipelineBuilder) =>
            BranchTo(id.ToString(), pipelineBuilder);
    }
}
