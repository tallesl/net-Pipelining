namespace PipeliningLibrary.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class EmptyTests
    {
        private static readonly PipelineGroup _pipelines = new EmptyPipeline();

        private static readonly Action<PipelineResult> _assertResult = result =>
        {
            Assert.AreEqual("empty", result.Id);
            Assert.IsNull(result.Output);
            Assert.IsTrue(result.Success);
            Assert.IsFalse(result.Pipes.Any());
        };

        [TestMethod]
        public void Run()
        {
            var output = _pipelines["empty"].Run();

            Assert.IsNull(output);
        }

        [TestMethod]
        public void RunAsync()
        {
            var task = _pipelines["empty"].RunAsync();
            var output = task.Result;

            Assert.IsNull(output);
        }

        [TestMethod]
        public void RunDetailed()
        {
            var result = _pipelines["empty"].RunDetailed();

            _assertResult(result);
        }

        [TestMethod]
        public void RunDetailedAsync()
        {
            var task = _pipelines["empty"].RunDetailedAsync();
            var result = task.Result;

            _assertResult(result);
        }

        [TestMethod]
        public void RunAsEnumerable()
        {
            var enumerable = _pipelines["empty"].RunAsEnumerable();

            Assert.IsFalse(enumerable.Any());
        }
    }
}
