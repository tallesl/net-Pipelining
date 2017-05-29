namespace PipeliningLibrary
{
    /// <summary>
    /// Ends a pipeline execution prematurely.
    /// </summary>
    public class PipelineEnd
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="output">Output object of the pipeline</param>
        public PipelineEnd(object output)
        {
            Output = output;
        }

        // Output object of the pipeline that just ended.
        internal object Output { get; private set; }
    }
}
