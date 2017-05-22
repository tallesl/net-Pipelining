namespace Pipelining.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PipeliningLibrary;
    using System;
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
                .Pipe<RemoveCasePipe>()
                .Pipe<RemoveNonAlpha>();
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
        public void ProgressMessages()
        {
            var expected = new Queue<string>(
                new []
                {
                    "Replacing upper case characters to lower case...",
                    "Removing non-alphabetic characters...",
                    "Splitting text into collection of words...",
                    "Removing duplicate words...",
                    "Removing english stop words...",
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
