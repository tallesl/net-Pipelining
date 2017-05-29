namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // A specifier that encapsulates a type (to be instantiated) of a IBranchType.
    internal class BranchPipeTypeSpecifier : IBranchPipeSpecifier
    {
        // Encapsulated type (to be instantiated).
        private readonly Type _type;

        // Branch restrictions (pipeline IDs) of this branch.
        private readonly IList<string> _restrictions;

        // Ctor accepting the type to be encapsulated.
        public BranchPipeTypeSpecifier(Type type)
        {
            _type = type;
            _restrictions = new List<string>();
        }

        // Resolves this specifier returning an instance of the encapsulated type.
        public IEnumerable<IBasePipe> Resolve()
        {
            if (!typeof(IBranchPipe).IsAssignableFrom(_type))
            {
                Trace.UnexpectedType(_type);
                return Enumerable.Empty<IBasePipe>();
            }

            var instance = (IBranchPipe)Activator.CreateInstance(_type);
            var restrictedBranchPipe = new BranchPipeWrapper(instance, _restrictions);

            return new[] { (IBasePipe)restrictedBranchPipe };
        }

        // Adds a branch restriction.
        public void AddRestriction(string id)
        {
            _restrictions.Add(id);
        }
    }
}
