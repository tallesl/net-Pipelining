namespace Pipelining.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PipeliningLibrary;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class UnitTests
    {
        static UnitTests()
        {
            Pipeline.Register("sanitize_input")
                .Pipe<RemoveCasePipe>()
                .Pipe<RemoveNonAlpha>();

            Pipeline.Register("extract_keywords")
                .Pipe("sanitize_input")
                .Pipe<SplitIntoWordsPipe>()
                .Pipe<DeduplicateWordsPipe>()
                .Pipe<RemoveStopWordsPipe>()
                .Pipe<SortAlphabeticallyPipe>();
        }

        [TestMethod]
        public void SanitizeInput()
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
        public void ExtractKeywords()
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
    }
}
