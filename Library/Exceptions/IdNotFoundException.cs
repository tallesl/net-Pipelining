namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Exception thrown when there is no pipeline registered with the given ID in the group.
    /// </summary>
    [Serializable]
    public sealed class IdNotFoundException : PipeliningException
    {
        internal IdNotFoundException(string id)
            : base(string.Format("\"{0}\" not found.", id)) { }
    }
}
