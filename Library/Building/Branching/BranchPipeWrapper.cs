namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // A wrapper of IBranchPipe needed to make branch restriction work.
    internal class BranchPipeWrapper : IBranchPipe
    {
        // The wrapped IBranchPipe.
        private readonly IBranchPipe _pipe;

        // IDs of the branches that are allow to branch to.
        private readonly string[] _restrictions;

        // Ctor accepting the IBranchPipe been wrapped and the list of pipeline IDs that are restricts its
        // branching.
        // If an empty restriction is passed it means that it can branch to any pipeline.
        internal BranchPipeWrapper(IBranchPipe pipe, IEnumerable<string> restrictions)
        {
            _pipe = pipe;
            _restrictions = (restrictions ?? new string[0]).ToArray();
        }

        // Type of the wrapped IBranchPipe.
        internal Type Type
        {
            get
            {
                return _pipe.GetType();
            }
        }

        // True if branching to the given pipeline ID is allowed.
        internal bool CanBranch(string id)
        {
            return _restrictions.Any() ? _restrictions.Contains(id) : true;
        }

        // Just runs and returns the wrapped IBranchPipe.
        BranchOutput IBranchPipe.Run(object input)
        {
            return _pipe.Run(input);
        }
    }
}
