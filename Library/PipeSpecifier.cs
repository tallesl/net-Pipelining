namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    internal class PipeSpecifier
    {
        private object _specifier;

        internal PipeSpecifier(string id)
        {
            _specifier = id;
        }

        internal PipeSpecifier(Type type)
        {
            _specifier = type;
        }

        internal PipeSpecifier(IPipe pipe)
        {
            _specifier = pipe;
        }

        internal IEnumerable<IPipe> Resolve()
        {
            var type = _specifier.GetType();

            if (_specifier is string)
            {
                return Pipeline.Get((string)_specifier).Pipes.SelectMany(p => p.Resolve());
            }
            else if (_specifier is Type)
            {
                type = (Type)_specifier;

                if (typeof(IPipe).IsAssignableFrom(type))
                    return new[] { (IPipe)Activator.CreateInstance(type) };
            }
            else if (_specifier is IPipe)
            {
                return new[] { (IPipe)_specifier };
            }

            Debug.Assert(true, "Unexpected type: " + _specifier.GetType());
            return Enumerable.Empty<IPipe>();
        }
    }
}
