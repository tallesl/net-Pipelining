namespace PipeliningLibrary
{
    /// <summary>
    /// Ends a pipeline execution prematurely.
    /// </summary>
    public class PipelineEnd
    {
        internal object Output;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="output">Output object of the pipeline</param>
        public PipelineEnd(object output)
        {
            Output = output;
        }
    }
}
