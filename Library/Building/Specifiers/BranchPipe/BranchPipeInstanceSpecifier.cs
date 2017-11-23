namespace PipeliningLibrary
{
    using System.Collections.Generic;

    // A specifier that encapsulates an instance of IBranchType.
    internal class BranchPipeInstanceSpecifier : IBranchPipeSpecifier
    {
        // Encapsulated instance.
        private readonly IBranchPipe _instance;

        // Branch restrictions (pipeline IDs) of this branch.
        private readonly IList<string> _restrictions;

        // Ctor accepting the instance to be encapsulated.
        public BranchPipeInstanceSpecifier(IBranchPipe instance)
        {
            _instance = instance;
            _restrictions = new List<string>();
        }

        // Resolves this specifier returning the encapsulated instance.
        public IEnumerable<IBasePipe> Resolve()
        {
            yield return new BranchPipeWrapper(_instance, _restrictions);
        }

        // Adds a branch restriction.
        void IBranchPipeSpecifier.AddRestriction(string id) => _restrictions.Add(id);
    }
}
