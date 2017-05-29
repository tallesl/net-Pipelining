namespace PipeliningLibrary
{
    using System.Collections.Generic;

    // Represents a pipe in the pipeline chain.
    // This is the most generic form, it can by a type (IPipe or IBranchPipe), an instance (IPipe or IBranchPipe) or
    // a whole pipeline reference (by it's ID).
    internal interface IPipeSpecifier
    {
        // Resolves and returns the actual pipes instance of this specifier.
        // A whole pipeline reference of branching can yield many pipes (hence the IEnumerable).
        IEnumerable<IBasePipe> Resolve();
    }
}
