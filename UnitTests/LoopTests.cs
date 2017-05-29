namespace PipeliningLibrary.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class LoopTests
    {
        private static readonly PipelineGroup _pipelines = new SortPipelines();

        private static readonly int[] _input = new[] { 2, 1, 3, 4, 7, 9, 8, 6, 5, };

        private static readonly object[] _expected = new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

        private static readonly object[][] _expectedEnumerable = new object[][]
        {
            new object[] { 1, 2, 3, 4, 7, 8, 6, 5, 9, },
            new object[] { 1, 2, 3, 4, 7, 6, 5, 8, 9, },
            new object[] { 1, 2, 3, 4, 6, 5, 7, 8, 9, },
            new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, },
            new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, },
        };

        private static readonly Func<object, object[]> _toArray = output => ((IEnumerable<object>)output).ToArray();

        private static readonly Action<PipelineResult> _assertResult = result =>
        {
            Assert.AreEqual("bubble_sort", result.Id);
            CollectionAssert.AreEqual(_expected, _toArray(result.Output));
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Exception());

            Assert.AreEqual(5, result.Pipes.Length);

            foreach (var pipeResult in result.Pipes)
            {
                Assert.AreEqual(typeof(BubbleSortPipe), pipeResult.Pipe);
                Assert.IsNull(pipeResult.Exception);
            }
        };

        [TestMethod]
        public void Run()
        {
            var actual = _pipelines["bubble_sort"].Run(_input);

            CollectionAssert.AreEqual(_expected, _toArray(actual));
        }

        [TestMethod]
        public void RunAsync()
        {
            var task = _pipelines["bubble_sort"].RunAsync(_input);
            var actual = task.Result;

            CollectionAssert.AreEqual(_expected, _toArray(actual));
        }

        [TestMethod]
        public void RunDetailed()
        {
            var result = _pipelines["bubble_sort"].RunDetailed(_input);

            _assertResult(result);
        }

        [TestMethod]
        public void RunDetailedAsync()
        {
            var task = _pipelines["bubble_sort"].RunDetailedAsync(_input);
            var result = task.Result;

            _assertResult(result);
        }

        [TestMethod]
        public void RunAsEnumerable()
        {
            var enumerable = _pipelines["bubble_sort"].RunAsEnumerable(_input);
            var actual = enumerable.ToArray();

            CollectionAssert.AreEqual(_toArray(_expectedEnumerable[0]), _toArray(actual[0]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerable[1]), _toArray(actual[1]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerable[2]), _toArray(actual[2]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerable[3]), _toArray(actual[3]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerable[4]), _toArray(actual[4]));
        }
    }
}
