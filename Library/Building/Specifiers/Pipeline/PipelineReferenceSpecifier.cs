namespace PipeliningLibrary
{
    using System.Collections.Generic;

    // A specifier of all the pipes of a pipeline.
    internal class PipelineReferenceSpecifier : IPipeSpecifier
    {
        // ID of the encapsulated pipeline.
        private readonly string _id;

        // Group of the encapsulated pipeline.
        private readonly PipelineGroup _group;

        // Ctor accepting the ID and the group of the pipeline to be encapsulated.
        internal PipelineReferenceSpecifier(string id, PipelineGroup group)
        {
            _id = id;
            _group = group;
        }

        // Resolves this specifier returning the pipes of the encapsulated pipeline.
        IEnumerable<IBasePipe> IPipeSpecifier.Resolve() => _group.Get(_id).Pipes;
    }
}
