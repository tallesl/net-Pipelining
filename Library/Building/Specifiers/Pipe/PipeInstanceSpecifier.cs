namespace PipeliningLibrary
{
    using System.Collections.Generic;

    // A specifier that encapsulates an instance of IPipe.
    internal class PipeInstanceSpecifier : IPipeSpecifier
    {
        // Encapsulated instance.
        private readonly IPipe _instance;

        // Ctor acceptings the instance to be encapsulated.
        public PipeInstanceSpecifier(IPipe instance)
        {
            _instance = instance;
        }

        // Resolves this specifier returning the encapsulated instance.
        public IEnumerable<IBasePipe> Resolve()
        {
            yield return _instance;
        }
    }
}
