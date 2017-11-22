namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;

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
                yield break;
            }

            var instance = (IBranchPipe)Activator.CreateInstance(_type);
            yield return new BranchPipeWrapper(instance, _restrictions);
        }

        // Adds a branch restriction.
        public void AddRestriction(string id) => _restrictions.Add(id);
    }
}
