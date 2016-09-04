namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Exception thrown when there's already a pipeline registered with the given identifier.
    /// </summary>
    [Serializable]
    public sealed class PipelineAlreadyRegisteredException : Exception
    {
        internal PipelineAlreadyRegisteredException(string id)
            : base(string.Format("\"{0}\" already registered.", id)) { }
    }
}
