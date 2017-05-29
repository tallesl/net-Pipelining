namespace PipeliningLibrary.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class SimpleTests
    {
        private static readonly PipelineGroup _pipelines = new TextPipelines();

        private static readonly string _input = Tamagotchi.English;

        private static readonly object _expected = "the tamagotchi is a handheld digital pet created in japan by " +
            "akihiro yokoi of wiz and aki maita of bandai";

        private static readonly object[] _expectedEnumerable = new[]
        {
            // RemoveNonAlpha output
            "The Tamagotchi is a handheld digital pet created in Japan by Akihiro Yokoi of WiZ and Aki Maita of Bandai",

            // RemoveCasePipe output
            "the tamagotchi is a handheld digital pet created in japan by akihiro yokoi of wiz and aki maita of bandai",
        };

        private static readonly Action<PipelineResult> _assertResult = result =>
        {
            Assert.AreEqual("sanitize_input", result.Id);
            Assert.AreEqual(_expected, result.Output);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Exception());

            Assert.AreEqual(2, result.Pipes.Length);

            var removeNonAlpha = result.Pipes[0];
            Assert.AreEqual(typeof(RemoveNonAlphaPipe), removeNonAlpha.Pipe);
            Assert.IsNull(removeNonAlpha.Exception);

            var removeCase = result.Pipes[1];
            Assert.AreEqual(typeof(RemoveCasePipe), removeCase.Pipe);
            Assert.IsNull(removeCase.Exception);
        };

        [TestMethod]
        public void Run()
        {
            var actual = _pipelines["sanitize_input"].Run(_input);

            Assert.AreEqual(_expected, actual);
        }

        [TestMethod]
        public void RunAsync()
        {
            var task = _pipelines["sanitize_input"].RunAsync(_input);
            var actual = task.Result;

            Assert.AreEqual(_expected, actual);
        }

        [TestMethod]
        public void RunDetailed()
        {
            var result = _pipelines["sanitize_input"].RunDetailed(_input);

            _assertResult(result);
        }

        [TestMethod]
        public void RunDetailedAsync()
        {
            var task = _pipelines["sanitize_input"].RunDetailedAsync(_input);
            var result = task.Result;

            _assertResult(result);
        }

        [TestMethod]
        public void RunAsEnumerable()
        {
            var enumerable = _pipelines["sanitize_input"].RunAsEnumerable(_input);

            CollectionAssert.AreEqual(_expectedEnumerable, enumerable.ToArray());
        }
    }
}
