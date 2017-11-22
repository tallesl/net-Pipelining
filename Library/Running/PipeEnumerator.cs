namespace PipeliningLibrary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    // A pipeline execution in enumerable/enumerator iterator form.
    internal class PipeEnumerator : IEnumerable<object>, IEnumerator<object>
    {
        // Object representing the current run of this iterator.
        private readonly PipelineRun _current;

        // Ctor accepting the pipeline to run and its input object.
        internal PipeEnumerator(Pipeline pipeline, object input)
        {
            _current = new PipelineRun(input, pipeline);
        }

        #region IDisposable

        void IDisposable.Dispose() { }

        #endregion

        #region IEnumerator

        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        bool IEnumerator.MoveNext()
        {
            if (_current.PendingPipes.MoveNext())
            {
                _current.RunOne();
                return true;
            }
            else
            {
                return false;
            }
        }

        object IEnumerator.Current
        {
            get { return _current.Output; }
        }

        IEnumerator IEnumerable.GetEnumerator() => this;

        #endregion

        #region IEnumerator<object>

        object IEnumerator<object>.Current
        {
            get { return _current.Output; }
        }

        #endregion

        #region IEnumerable<object>

        IEnumerator<object> IEnumerable<object>.GetEnumerator() => this;

        #endregion
    }
}
