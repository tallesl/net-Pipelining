namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Message sent by a running pipe.
    /// </summary>
    [Serializable]
    public class PipeMessage : PipelineEvent
    {
        /// <summary>
        /// Message sent.
        /// </summary>
        public string Message { get; set; }
    }
}
