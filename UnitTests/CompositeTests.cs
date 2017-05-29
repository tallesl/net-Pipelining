namespace PipeliningLibrary.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class CompositeTests
    {
        private static readonly PipelineGroup _pipelines = new TextPipelines();

        private static readonly string _input = Tamagotchi.English;

        private static readonly string[] _expected = new[]
        {
            "aki", "akihiro", "bandai", "created", "digital", "handheld", "japan", "maita", "pet", "tamagotchi", "wiz",
            "yokoi"
        };

        private static readonly object[] _expectedEnumerable = new object[]
        {                    
            // RemoveNonAlpha output
            "The Tamagotchi is a handheld digital pet created in Japan by Akihiro Yokoi of WiZ and Aki Maita of Bandai",

            // RemoveCasePipe output
            "the tamagotchi is a handheld digital pet created in japan by akihiro yokoi of wiz and aki maita of bandai",

            // SplitIntoWordsPipe output
            new[]
            {
                "the", "tamagotchi", "is", "a", "handheld", "digital", "pet", "created", "in", "japan", "by", "akihiro",
                "yokoi", "of", "wiz", "and", "aki", "maita", "of", "bandai",
            },

            // DeduplicateWordsPipe output
            new[]
            {
                "the", "tamagotchi", "is", "a", "handheld", "digital", "pet", "created", "in", "japan", "by", "akihiro",
                "yokoi", "of", "wiz", "and", "aki", "maita", "bandai",
            },

            // RemoveStopWordsPipe output
            new[]
            {
                "tamagotchi", "handheld", "digital", "pet", "created", "japan", "akihiro", "yokoi", "wiz", "aki",
                "maita", "bandai",
            },

            // SortAlphabeticallyPipe output
            new[]
            {
                "aki", "akihiro", "bandai", "created", "digital", "handheld", "japan", "maita", "pet", "tamagotchi",
                "wiz", "yokoi",
            }
        };

        private static readonly Func<object, string[]> _toArray = output => ((IEnumerable<string>)output).ToArray();

        private static readonly Action<PipelineResult> _assertResult = result =>
        {
            Assert.AreEqual("extract_keywords_en", result.Id);
            CollectionAssert.AreEqual(_expected, _toArray(result.Output));
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Exception());

            Assert.AreEqual(6, result.Pipes.Length);

            var removeNonAlpha = result.Pipes[0];
            Assert.AreEqual(typeof(RemoveNonAlphaPipe), removeNonAlpha.Pipe);
            Assert.IsNull(removeNonAlpha.Exception);

            var removeCase = result.Pipes[1];
            Assert.AreEqual(typeof(RemoveCasePipe), removeCase.Pipe);
            Assert.IsNull(removeCase.Exception);

            var splitIntoWords = result.Pipes[2];
            Assert.AreEqual(typeof(SplitIntoWordsPipe), splitIntoWords.Pipe);
            Assert.IsNull(splitIntoWords.Exception);

            var deduplicateWords = result.Pipes[3];
            Assert.AreEqual(typeof(DeduplicateWordsPipe), deduplicateWords.Pipe);
            Assert.IsNull(deduplicateWords.Exception);

            var removeStopWords = result.Pipes[4];
            Assert.AreEqual(typeof(RemoveStopWordsPipe), removeStopWords.Pipe);
            Assert.IsNull(removeStopWords.Exception);

            var sortAlphabetically = result.Pipes[5];
            Assert.AreEqual(typeof(SortAlphabeticallyPipe), sortAlphabetically.Pipe);
            Assert.IsNull(sortAlphabetically.Exception);
        };

        [TestMethod]
        public void Run()
        {
            var actual = _pipelines["extract_keywords_en"].Run(_input);

            CollectionAssert.AreEqual(_expected, _toArray(actual));
        }

        [TestMethod]
        public void RunAsync()
        {
            var task = _pipelines["extract_keywords_en"].RunAsync(_input);
            var actual = task.Result;

            CollectionAssert.AreEqual(_expected, _toArray(actual));
        }

        [TestMethod]
        public void RunDetailed()
        {
            var result = _pipelines["extract_keywords_en"].RunDetailed(_input);

            _assertResult(result);
        }

        [TestMethod]
        public void RunDetailedAsync()
        {
            var task = _pipelines["extract_keywords_en"].RunDetailedAsync(_input);
            var result = task.Result;

            _assertResult(result);
        }

        [TestMethod]
        public void RunAsEnumerable()
        {
            var enumerable = _pipelines["extract_keywords_en"].RunAsEnumerable(_input);
            var actual = enumerable.ToArray();

            Assert.AreEqual(_expectedEnumerable[0], actual[0]);
            Assert.AreEqual(_expectedEnumerable[1], actual[1]);

            CollectionAssert.AreEqual(_toArray(_expectedEnumerable[2]), _toArray(actual[2]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerable[3]), _toArray(actual[3]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerable[4]), _toArray(actual[4]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerable[5]), _toArray(actual[5]));
        }
    }
}
