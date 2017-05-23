namespace PipeliningLibrary.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Pipelines;
    using System;
    using System.Collections;
    using System.Linq;

    [TestClass]
    public class SortTests
    {
        private PipelineGroup _pipelines;

        [TestInitialize]
        public void Initialize()
        {
            _pipelines = new SortPipelines();
        }

        [TestMethod]
        public void LoopTask()
        {
            var input = new[] { 2, 1, 3, 4, 7, 9, 8, 6, 5, };
            var expected = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };

            var result = _pipelines.Run("bubble_sort", input).Result;
            var output = (object[])result.Output;
            var actual = output.Cast<int>().ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoopEnumerable()
        {
            var input = new[] { 2, 1, 3, 4, 7, 9, 8, 6, 5, };
            var expected = new int[][]
            {
                new[] { 1, 2, 3, 4, 7, 8, 6, 5, 9, },
                new[] { 1, 2, 3, 4, 7, 6, 5, 8, 9, },
                new[] { 1, 2, 3, 4, 6, 5, 7, 8, 9, },
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, },
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, },
            };

            var actual = _pipelines.GetEnumerable("bubble_sort", input).ToArray();

            Action<object, object> assertCollection = (e, a) =>
                CollectionAssert.AreEqual((ICollection)e, (ICollection)a);

            Assert.AreEqual(expected.Length, actual.Length);

            assertCollection(expected[0], actual[0]);
            assertCollection(expected[1], actual[1]);
            assertCollection(expected[2], actual[2]);
            assertCollection(expected[3], actual[3]);
            assertCollection(expected[4], actual[4]);
        }
    }
}
