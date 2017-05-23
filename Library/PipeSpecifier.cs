namespace PipeliningLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    internal class PipeSpecifier
    {
        private object _specifier;

        internal PipeSpecifier(Type type)
        {
            _specifier = type;
        }

        internal PipeSpecifier(IPipe pipe)
        {
            _specifier = pipe;
        }

        internal PipeSpecifier(PipelineGroup pipelines, string id)
        {
            _specifier = new Tuple<PipelineGroup, string>(pipelines, id);
        }

        internal IEnumerable<IPipe> Resolve()
        {
            var type = _specifier.GetType();

            if (_specifier is Type)
            {
                type = (Type)_specifier;

                if (typeof(IPipe).IsAssignableFrom(type))
                    return new[] { (IPipe)Activator.CreateInstance(type) };
            }
            else if (_specifier is IPipe)
            {
                return new[] { (IPipe)_specifier };
            }
            else if (_specifier is Tuple<PipelineGroup, string>)
            {
                var tuple = (Tuple<PipelineGroup, string>)_specifier;
                return tuple.Item1.Get(tuple.Item2).Pipes.SelectMany(p => p.Resolve());
            }

            Debug.Assert(true, "Unexpected type: " + _specifier.GetType());
            return Enumerable.Empty<IPipe>();
        }
    }
}
