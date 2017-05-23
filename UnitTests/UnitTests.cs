namespace Pipelining.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Pipelining.UnitTests.Pipes;
    using PipeliningLibrary;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class UnitTests
    {
        private PipelineGroup _pipelines;

        [TestInitialize]
        public void Initialize()
        {
            _pipelines = new TestsPipelines();
        }

        [TestMethod]
        public void SimplePipeline()
        {
            var input = @"The Tamagotchi (たまごっち) is a handheld digital pet, created in Japan by Akihiro Yokoi of
                WiZ and Aki Maita of Bandai.";

            var expected = "the tamagotchi is a handheld digital pet created in japan by akihiro yokoi of wiz and " +
                "aki maita of bandai";

            var result = _pipelines.Run("sanitize_input", input).Result;
            var actual = (string)result.Output;

            Assert.IsTrue(result.Success);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CompositePipeline()
        {
            var input = @"The Tamagotchi (たまごっち) is a handheld digital pet, created in Japan by Akihiro Yokoi of
                WiZ and Aki Maita of Bandai.";

            var expected = new[] { "aki", "akihiro", "bandai", "created", "digital", "handheld", "japan", "maita",
                "pet", "tamagotchi", "wiz", "yokoi" };

            var result = _pipelines.Run("extract_keywords", input).Result;
            var actual = ((IEnumerable<string>)result.Output).ToArray();

            Assert.IsTrue(result.Success);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RunningAsEnumerable()
        {
            var input = @"The Tamagotchi (たまごっち) is a handheld digital pet, created in Japan by Akihiro Yokoi of
                WiZ and Aki Maita of Bandai.";

            var expected =  new object[]
            {
                    // RemoveNonAlpha output
                    "The Tamagotchi is a handheld digital pet created in Japan by Akihiro Yokoi of WiZ and Aki Maita " +
                        "of Bandai",

                    // RemoveCasePipe output
                    "the tamagotchi is a handheld digital pet created in japan by akihiro yokoi of wiz and aki maita " +
                    "of bandai",

                    // SplitIntoWordsPipe output
                    new[] { "the", "tamagotchi", "is", "a", "handheld", "digital", "pet", "created", "in", "japan",
                        "by", "akihiro", "yokoi", "of", "wiz", "and", "aki", "maita", "of", "bandai", },

                    // DeduplicateWordsPipe output
                    new[] { "the", "tamagotchi", "is", "a", "handheld", "digital", "pet", "created", "in", "japan",
                        "by", "akihiro", "yokoi", "of", "wiz", "and", "aki", "maita", "bandai", },

                    // RemoveStopWordsPipe output
                    new[] { "tamagotchi", "handheld", "digital", "pet", "created", "japan", "akihiro", "yokoi", "wiz",
                        "aki", "maita", "bandai", },

                    // SortAlphabeticallyPipe output
                    new[] { "aki", "akihiro", "bandai", "created", "digital", "handheld", "japan", "maita", "pet",
                        "tamagotchi", "wiz", "yokoi", }
            };

            var actual = _pipelines.GetEnumerable("extract_keywords", input).ToArray();

            Action<object, object> assertCollection = (e, a) =>
                CollectionAssert.AreEqual(((IEnumerable<string>)e).ToArray(), ((IEnumerable<string>)a).ToArray());

            Assert.AreEqual(expected.Length, actual.Length);

            assertCollection(expected[2], actual[2]);
            assertCollection(expected[3], actual[3]);
            assertCollection(expected[4], actual[4]);
            assertCollection(expected[5], actual[5]);
        }

        [TestMethod]
        public void ProgressMessages()
        {
            var expected = new Queue<string>(
                new []
                {
                    // RemoveNonAlpha message
                    "Removing non-alphabetic characters...",

                    // RemoveCasePipe message
                    "Replacing upper case characters to lower case...",

                    // SplitIntoWordsPipe message
                    "Splitting text into collection of words...",

                    // DeduplicateWordsPipe message
                    "Removing duplicate words...",

                    // RemoveStopWordsPipe message
                    "Removing english stop words...",

                    // SortAlphabeticallyPipe message
                    "Sorting words alphabetically...",
                }
            );

            Action<PipelineEvent> progress = e =>
            {
                if (e is PipeMessage)
                {
                    var message = (PipeMessage)e;
                    Assert.AreEqual(expected.Dequeue(), message.Message);
                }
            };

            var result = _pipelines.Run("extract_keywords", "Some text.", progress).Result;

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void SuccessResult()
        {
            var now = DateTime.UtcNow;
            Func<DateTime, bool> checkDate = d => (d - now) < TimeSpan.FromSeconds(5);

            var result = _pipelines.Run("extract_keywords", "Some text.").Result;

            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Pipes.Count(), 6);

            Assert.AreEqual(typeof(RemoveNonAlphaPipe), result.Pipes[0].Pipe);
            Assert.IsTrue(checkDate(result.Pipes[0].Started));
            Assert.IsTrue(checkDate(result.Pipes[0].Ended));
            Assert.IsNull(result.Pipes[0].Exception);

            Assert.AreEqual(typeof(RemoveCasePipe), result.Pipes[1].Pipe);
            Assert.IsTrue(checkDate(result.Pipes[1].Started));
            Assert.IsTrue(checkDate(result.Pipes[1].Ended));
            Assert.IsNull(result.Pipes[1].Exception);

            Assert.AreEqual(typeof(SplitIntoWordsPipe), result.Pipes[2].Pipe);
            Assert.IsTrue(checkDate(result.Pipes[2].Started));
            Assert.IsTrue(checkDate(result.Pipes[2].Ended));
            Assert.IsNull(result.Pipes[2].Exception);

            Assert.AreEqual(typeof(DeduplicateWordsPipe), result.Pipes[3].Pipe);
            Assert.IsTrue(checkDate(result.Pipes[3].Started));
            Assert.IsTrue(checkDate(result.Pipes[3].Ended));
            Assert.IsNull(result.Pipes[3].Exception);

            Assert.AreEqual(typeof(RemoveStopWordsPipe), result.Pipes[4].Pipe);
            Assert.IsTrue(checkDate(result.Pipes[4].Started));
            Assert.IsTrue(checkDate(result.Pipes[4].Ended));
            Assert.IsNull(result.Pipes[4].Exception);

            Assert.AreEqual(typeof(SortAlphabeticallyPipe), result.Pipes[5].Pipe);
            Assert.IsTrue(checkDate(result.Pipes[5].Started));
            Assert.IsTrue(checkDate(result.Pipes[5].Ended));
            Assert.IsNull(result.Pipes[5].Exception);
        }

        [TestMethod]
        public void FailureResult()
        {
            var now = DateTime.UtcNow;
            Func<DateTime, bool> checkDate = d => (d - now) < TimeSpan.FromSeconds(5);

            var result = _pipelines.Run("extract_keywords_exception", "Some text.").Result;

            Assert.IsFalse(result.Success);
            Assert.AreEqual(3, result.Pipes.Count());

            Assert.AreEqual(typeof(RemoveNonAlphaPipe), result.Pipes[0].Pipe);
            Assert.IsTrue(checkDate(result.Pipes[0].Started));
            Assert.IsTrue(checkDate(result.Pipes[0].Ended));
            Assert.IsNull(result.Pipes[0].Exception);

            Assert.AreEqual(typeof(RemoveCasePipe), result.Pipes[1].Pipe);
            Assert.IsTrue(checkDate(result.Pipes[1].Started));
            Assert.IsTrue(checkDate(result.Pipes[1].Ended));
            Assert.IsNull(result.Pipes[1].Exception);

            Assert.AreEqual(typeof(ExceptionPipe), result.Pipes[2].Pipe);
            Assert.IsTrue(checkDate(result.Pipes[2].Started));
            Assert.IsTrue(checkDate(result.Pipes[2].Ended));
            Assert.IsNotNull(result.Pipes[2].Exception);

            Assert.AreEqual("Exception thrown on purpose.", result.Pipes[2].Exception.Message);
            Assert.AreEqual(typeof(InvalidOperationException), result.Pipes[2].Exception.GetType());
        }
    }
}
