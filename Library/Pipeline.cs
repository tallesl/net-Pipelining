namespace PipeliningLibrary
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Builds and runs pipelines out of pipes.
    /// </summary>
    public sealed partial class Pipeline
    {
        // Pipes of this pipeline.
        private readonly IList<IPipeSpecifier> _specifiers;

        // Ctor accepting this pipeline ID and group.
        internal Pipeline(string id, PipelineGroup group)
        {
            Id = id;
            Group = group;
            _specifiers = new List<IPipeSpecifier>();
        }

        // ID of this pipeline.
        internal string Id { get; private set; }

        // Group of this pipeline.
        internal PipelineGroup Group { get; private set; }

        // Gets the pipes of this pipeline.
        internal IEnumerable<IBasePipe> Pipes
        {
            get
            {
                return _specifiers.SelectMany(s => s.Resolve());
            }
        }

        // Add a branch restriction to the last pipe.
        // The last pipe must be a branch pipe, else raises an error.
        internal void AddBranchRestriction(string id)
        {
            var last = _specifiers.Last();

            if (!(last is IBranchPipeSpecifier))
                Trace.UnexpectedType(last.GetType());

            ((IBranchPipeSpecifier)last).AddRestriction(id);
        }

        // "Pipes" a pipe to this pipeline.
        internal void AddPipe(IPipeSpecifier specifier)
        {
            _specifiers.Add(specifier);
        }
    }
}
