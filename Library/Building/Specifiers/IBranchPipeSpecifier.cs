namespace PipeliningLibrary
{
    // Represents a IBranchPipe in the pipeline chain.
    // Similar to its base interface (IPipeSpecifier) but with a method to make branch restrictions.
    internal interface IBranchPipeSpecifier : IPipeSpecifier
    {
        // Adds the given pipeline ID as a branch restriction to this specifier.
        void AddRestriction(string id);
    }
}
