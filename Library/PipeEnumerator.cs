namespace PipeliningLibrary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class PipeEnumerator : IEnumerable<object>, IEnumerator<object>
    {
        private Action<string> _progress;

        private IEnumerator<IPipe> _toRun;

        private Queue<PipeSpecifier> _toResolve;

        internal PipeEnumerator(object input, Action<string> progress, IEnumerable<PipeSpecifier> specifiers)
        {
            Current = input;

            _progress = progress ?? (s => { });
            _toResolve = new Queue<PipeSpecifier>(specifiers);

            Resolve();
        }

        public object Current { get; private set; }

        public bool MoveNext()
        {
            if (_toRun.MoveNext())
            {
                RunPipe(_toRun.Current);
                return true;
            }
            else
            {
                if (Resolve())
                    return MoveNext();
                else
                    return false;
            }
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        private bool Resolve()
        {
            if (_toResolve.Any())
            {
                _toRun = _toResolve.Dequeue().Resolve().GetEnumerator();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RunPipe(IPipe pipe)
        {
            Current = pipe.Run(Current, _progress);
        }

        public void Dispose() { }

        public IEnumerator<object> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
