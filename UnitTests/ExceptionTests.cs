namespace PipeliningLibrary.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class ExceptionTests
    {
        private static readonly PipelineGroup _pipelines = new TextPipelines();

        private static readonly string _input = Tamagotchi.English;

        private static readonly object _expected = "the tamagotchi is a handheld digital pet created in japan by " +
            "akihiro yokoi of wiz and aki maita of bandai";

        private static readonly Action<PipelineResult> _assertResult = result =>
        {
            Assert.AreEqual("extract_keywords_ex", result.Id);
            Assert.AreEqual(_expected, result.Output);
            Assert.IsFalse(result.Success);
            Assert.IsNotNull(result.Exception());

            Assert.AreEqual(3, result.Pipes.Length);

            var removeNonAlpha = result.Pipes[0];
            Assert.AreEqual(typeof(RemoveNonAlphaPipe), removeNonAlpha.Pipe);
            Assert.IsNull(removeNonAlpha.Exception);

            var removeCase = result.Pipes[1];
            Assert.AreEqual(typeof(RemoveCasePipe), removeCase.Pipe);
            Assert.IsNull(removeCase.Exception);

            var exceptionPipe = result.Pipes[2];
            Assert.AreEqual(typeof(ExceptionPipe), exceptionPipe.Pipe);
            Assert.IsNotNull(exceptionPipe.Exception);
            Assert.AreEqual(typeof(InvalidOperationException), exceptionPipe.Exception.GetType());
            Assert.AreEqual("Exception thrown on purpose.", exceptionPipe.Exception.Message);
        };

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Run()
        {
            _pipelines["extract_keywords_ex"].Run(_input);
        }

        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void RunAsync()
        {
            var task = _pipelines["extract_keywords_ex"].RunAsync(_input);
            var actual = task.Result;
        }

        [TestMethod]
        public void RunDetailed()
        {
            var result = _pipelines["extract_keywords_ex"].RunDetailed(_input);

            _assertResult(result);
        }

        [TestMethod]
        public void RunDetailedAsync()
        {
            var task = _pipelines["extract_keywords_ex"].RunDetailedAsync(_input);
            var result = task.Result;

            _assertResult(result);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void RunAsEnumerable()
        {
            var enumerable = _pipelines["extract_keywords_ex"].RunAsEnumerable(_input);
            var actual = enumerable.ToArray();
        }

        [TestMethod, ExpectedException(typeof(InvalidBranchingException))]
        public void BranchRestrictedException()
        {
            _pipelines["extract_keywords"].Run(
                new
                {
                    Text = "Some text.",
                    Language = "Exception",
                }
            );
        }

        [TestMethod, ExpectedException(typeof(IdExistsException))]
        public void IdExistsException()
        {
            _pipelines.Pipeline("extract_keywords");
        }

        [TestMethod, ExpectedException(typeof(IdNotFoundException))]
        public void IdNotFoundException()
        {
            _pipelines["non_existent"].Run();
        }
    }
}
