namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Exception thrown when an invalid attempt to branch is made.
    /// </summary>
    [Serializable]
    public sealed class InvalidBranchingException : PipeliningException
    {
        internal InvalidBranchingException(string message) : base(message) { }
    }
}
