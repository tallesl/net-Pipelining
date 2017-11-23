namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Builds pipelines.
    /// </summary>
    public class PipelineBuilder
    {
        // The pipeline been constructed.
        private readonly Pipeline _pipeline;

        // Ctor accepting the pipeline been constructed.
        internal PipelineBuilder(Pipeline pipeline)
        {
            _pipeline = pipeline;
        }

        /// <summary>
        /// Sets the next pipe in the pipeline chain.
        /// </summary>
        /// <typeparam name="T">The IPipe to set</typeparam>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public PipelineBuilder Pipe<T>() where T : IPipe, new()
        {
            var specifier = new PipeTypeSpecifier(typeof(T));
            _pipeline.AddPipe(specifier);
            return this;
        }

        /// <summary>
        /// Sets the next pipe in the pipeline chain.
        /// </summary>
        /// <param name="pipe">The IPipe to set</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public PipelineBuilder Pipe(IPipe pipe)
        {
            var specifier = new PipeInstanceSpecifier(pipe);
            _pipeline.AddPipe(specifier);
            return this;
        }

        /// <summary>
        /// Gets the pipes from the given pipeline and sets in this pipeline chain.
        /// </summary>
        /// <param name="id">ID of the pipeline to get the pipes from</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public PipelineBuilder Pipe(string id)
        {
            var specifier = new PipelineReferenceSpecifier(id, _pipeline.Group);
            _pipeline.AddPipe(specifier);
            return this;
        }

        /// <summary>
        /// Constructs a new pipeline with the given ID and sets its pipes in this pipeline chain.
        /// </summary>
        /// <param name="id">ID of the pipeline been constructed in this call</param>
        /// <param name="pipelineBuilder">An action that builds the new pipeline</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public PipelineBuilder Pipeline(string id, Action<PipelineBuilder> pipelineBuilder)
        {
            pipelineBuilder(_pipeline.Group.Pipeline(id));
            return Pipe(id);
        }

        /// <summary>
        /// Gets the pipes from the given pipeline and sets in this pipeline chain.
        /// </summary>
        /// <param name="id">ID of the pipeline to get the pipes from</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public PipelineBuilder Pipe(Enum id) => Pipe(id.ToString());

        /// <summary>
        /// Constructs a new pipeline with the given ID and sets its pipes in this pipeline chain.
        /// </summary>
        /// <param name="id">ID of the pipeline been constructed in this call</param>
        /// <param name="pipelineBuilder">An action that builds the new pipeline</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public PipelineBuilder Pipeline(Enum id, Action<PipelineBuilder> pipelineBuilder) =>
            Pipeline(id, pipelineBuilder);

        /// <summary>
        /// Constructs a branch path in this pipeline.
        /// </summary>
        /// <typeparam name="T">The IBranchPipe that branchs at this point</typeparam>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public BranchBuilder BranchPipe<T>() where T : IBranchPipe, new()
        {
            var specializer = new BranchPipeTypeSpecifier(typeof(T));
            _pipeline.AddPipe(specializer);
            return new BranchBuilder(_pipeline);
        }

        /// <summary>
        /// Constructs a branch path in this pipeline.
        /// </summary>
        /// <param name="pipe">The IBranchPipe that branchs at this point</param>
        /// <returns>This builder instance, so you can use it in a fluent fashion</returns>
        public BranchBuilder BranchPipe(IBranchPipe pipe)
        {
            var specializer = new BranchPipeInstanceSpecifier(pipe);
            _pipeline.AddPipe(specializer);
            return new BranchBuilder(_pipeline);
        }
    }
}
