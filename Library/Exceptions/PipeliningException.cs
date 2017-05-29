namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Exception specific to the pipelining library.
    /// </summary>
    public class PipeliningException : Exception
    {
        internal PipeliningException(string message) : base(message) { } 
    }
}
