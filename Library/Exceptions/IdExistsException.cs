namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Exception thrown when there is already a pipeline registered with the given ID in the group.
    /// </summary>
    [Serializable]
    public sealed class IdExistsException : PipeliningException
    {
        internal IdExistsException(string id)
            : base(string.Format("\"{0}\" already exists in group.", id)) { }
    }
}
