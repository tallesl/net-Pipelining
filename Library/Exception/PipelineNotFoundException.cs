namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Exception thrown when there's no pipeline registered with the given identifier.
    /// </summary>
    [Serializable]
    public sealed class PipelineNotFoundException : Exception
    {
        internal PipelineNotFoundException(string id)
            : base(string.Format("\"{0}\" not found.", id)) { }
    }
}
