namespace PipeliningLibrary
{
    using System;

    // Helpful trace class.
    internal static class Trace
    {
        // The only trace usage of the library: unexpected type.
        internal static void UnexpectedType(Type type)
        {
            System.Diagnostics.Trace.Assert(true, "Unexpected type \"{0}\".", type.Name);
        }
    }
}
