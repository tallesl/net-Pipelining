namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Pipe that can be chained into a pipeline.
    /// </summary>
    public interface IPipe : IBasePipe
    {
        /// <summary>
        /// Runs this pipe.
        /// </summary>
        /// <param name="input">Input of this run</param>
        /// <returns>Output of this run</returns>
        object Run(dynamic input);
    }
}
