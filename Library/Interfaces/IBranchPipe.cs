namespace PipeliningLibrary
{
    using System;

    /// <summary>
    /// Pipe that can be branched into a different pipeline.
    /// </summary>
    public interface IBranchPipe : IBasePipe
    {
        /// <summary>
        /// Runs this branch pipe.
        /// </summary>
        /// <param name="input">Pipe's input</param>
        /// <returns>An object with the output object and the pipeline to branch to</returns>
        BranchOutput Run(dynamic input);
    }
}
