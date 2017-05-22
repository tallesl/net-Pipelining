namespace Pipelining.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PipeliningLibrary;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class UnitTests
    {
        static UnitTests()
        {
            Pipeline.Register("extract_keywords")
                .Pipe("sanitize_input")
                .Pipe<SplitIntoWordsPipe>()
                .Pipe<DeduplicateWordsPipe>()
                .Pipe<RemoveStopWordsPipe>()
                .Pipe<SortAlphabeticallyPipe>();

            Pipeline.Register("sanitize_input")
                .Pipe<RemoveNonAlpha>()
                .Pipe<RemoveCasePipe>();
        }

        [TestMethod]
        public void SimplePipeline()
        {
            var input = @"The Tamagotchi (たまごっち) is a handheld digital pet, created in Japan by Akihiro Yokoi of
                WiZ and Aki Maita of Bandai.";

            var expected = "the tamagotchi is a handheld digital pet created in japan by akihiro yokoi of wiz and " +
                "aki maita of bandai";

            var result = Pipeline.Run("sanitize_input", input).Result;
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

            var result = Pipeline.Run("extract_keywords", input).Result;
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

            var actual = Pipeline.GetEnumerable("extract_keywords", input).ToArray();

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

            var result = Pipeline.Run("extract_keywords", "Some text.", progress).Result;

            Assert.IsTrue(result.Success);
        }
    }
}
