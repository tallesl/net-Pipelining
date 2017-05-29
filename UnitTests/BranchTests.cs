namespace PipeliningLibrary.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class BranchTests
    {
        private static readonly PipelineGroup _pipelines = new TextPipelines();

        private static readonly object _englishInput = new
        {
            Text = Tamagotchi.English,
            Language = "English",
        };

        private static readonly object _spanishInput = new
        {
            Text = Tamagotchi.Spanish,
            Language = "Spanish",
        };

        private static readonly object _portugueseInput = new
        {
            Text = Tamagotchi.Portuguese,
            Language = "Portuguese",
        };

        private static readonly string[] _expectedEnglish = new[]
        {
            "aki", "akihiro", "bandai", "created", "digital", "handheld", "japan", "maita", "pet", "tamagotchi", "wiz",
            "yokoi"
        };

        private static readonly string[] _expectedSpanish = new[]
        {
            "aki", "bandai", "comercializada", "creada", "maita", "mascota", "tamagotchi", "virtual",
        };

        private static readonly string[] _expectedPortuguese = new[]
        {
            "animal", "brinquedo", "cria", "estimacao", "tamagotchi", "virtual",
        };

        private static readonly object[] _expectedEnumerableEnglish = new object[]
        {
            // PickLanguage output
            "The Tamagotchi (たまごっち) is a handheld digital pet, created in Japan by Akihiro Yokoi of WiZ and Aki " +
                "Maita of Bandai.",

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

        private static readonly object[] _expectedEnumerableSpanish = new object[]
        {
            // PickLanguage output
            "Un Tamagotchi (たまごっち) es una mascota virtual creada en 1996 por Aki Maita y comercializada por " +
            "Bandai.",

            // RemoveNonAlpha output
            "Un Tamagotchi es una mascota virtual creada en por Aki Maita y comercializada por Bandai",

            // RemoveCasePipe output
            "un tamagotchi es una mascota virtual creada en por aki maita y comercializada por bandai",

            // SplitIntoWordsPipe output
            new[]
            {
                "un", "tamagotchi", "es", "una", "mascota", "virtual", "creada", "en", "por", "aki", "maita", "y",
                "comercializada", "por", "bandai",
            },

            // DeduplicateWordsPipe output
            new[]
            {
                "un", "tamagotchi", "es", "una", "mascota", "virtual", "creada", "en", "por", "aki", "maita", "y",
                "comercializada", "bandai",
            },

            // RemoveStopWordsPipe output
            new[]
            {
                "tamagotchi", "mascota", "virtual", "creada", "aki", "maita", "comercializada", "bandai",
            },

            // SortAlphabeticallyPipe output
            new[]
            {
                "aki", "bandai", "comercializada", "creada", "maita", "mascota", "tamagotchi", "virtual",
            }
        };

        private static readonly object[] _expectedEnumerablePortuguese = new object[]
        {
            // PickLanguage output
            "O tamagotchi (たまごっち) é um brinquedo em que se cria um animal de estimação virtual.",

            // RemoveNonAlpha output
            "O tamagotchi e um brinquedo em que se cria um animal de estimacao virtual",

            // RemoveCasePipe output
            "o tamagotchi e um brinquedo em que se cria um animal de estimacao virtual",

            // SplitIntoWordsPipe output
            new[]
            {
                "o", "tamagotchi", "e", "um", "brinquedo", "em", "que", "se", "cria", "um", "animal", "de", "estimacao",
                "virtual",
            },

            // DeduplicateWordsPipe output
            new[]
            {
                "o", "tamagotchi", "e", "um", "brinquedo", "em", "que", "se", "cria", "animal", "de", "estimacao",
                "virtual",
            },

            // RemoveStopWordsPipe output
            new[]
            {
                "tamagotchi", "brinquedo", "cria", "animal", "estimacao", "virtual",
            },

            // SortAlphabeticallyPipe output
            new[]
            {
                "animal", "brinquedo", "cria", "estimacao", "tamagotchi", "virtual",
            }
        };

        private static readonly Func<object, string[]> _toArray = output => ((IEnumerable<string>)output).ToArray();

        private static readonly Action<PipelineResult, object[]> _assertResult = (result, expected) =>
        {
            Assert.AreEqual("extract_keywords", result.Id);
            CollectionAssert.AreEqual(expected, _toArray(result.Output));
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Exception());

            Assert.AreEqual(7, result.Pipes.Length);

            var pickLanguage = result.Pipes[0];
            Assert.AreEqual(typeof(PickLanguagePipe), pickLanguage.Pipe);
            Assert.IsNull(pickLanguage.Exception);

            var removeNonAlpha = result.Pipes[1];
            Assert.AreEqual(typeof(RemoveNonAlphaPipe), removeNonAlpha.Pipe);
            Assert.IsNull(removeNonAlpha.Exception);

            var removeCase = result.Pipes[2];
            Assert.AreEqual(typeof(RemoveCasePipe), removeCase.Pipe);
            Assert.IsNull(removeCase.Exception);

            var splitIntoWords = result.Pipes[3];
            Assert.AreEqual(typeof(SplitIntoWordsPipe), splitIntoWords.Pipe);
            Assert.IsNull(splitIntoWords.Exception);

            var deduplicateWords = result.Pipes[4];
            Assert.AreEqual(typeof(DeduplicateWordsPipe), deduplicateWords.Pipe);
            Assert.IsNull(deduplicateWords.Exception);

            var removeStopWords = result.Pipes[5];
            Assert.AreEqual(typeof(RemoveStopWordsPipe), removeStopWords.Pipe);
            Assert.IsNull(removeStopWords.Exception);

            var sortAlphabetically = result.Pipes[6];
            Assert.AreEqual(typeof(SortAlphabeticallyPipe), sortAlphabetically.Pipe);
            Assert.IsNull(sortAlphabetically.Exception);
        };

        [TestMethod]
        public void RunEnglish()
        {
            var actual = _pipelines["extract_keywords"].Run(_englishInput);

            CollectionAssert.AreEqual(_expectedEnglish, _toArray(actual));
        }

        [TestMethod]
        public void RunSpanish()
        {
            var actual = _pipelines["extract_keywords"].Run(_spanishInput);

            CollectionAssert.AreEqual(_expectedSpanish, _toArray(actual));
        }

        [TestMethod]
        public void RunPortuguese()
        {
            var actual = _pipelines["extract_keywords"].Run(_portugueseInput);

            CollectionAssert.AreEqual(_expectedPortuguese, _toArray(actual));
        }

        [TestMethod]
        public void RunAsyncEnglish()
        {
            var task = _pipelines["extract_keywords"].RunAsync(_englishInput);
            var actual = task.Result;

            CollectionAssert.AreEqual(_expectedEnglish, _toArray(actual));
        }

        [TestMethod]
        public void RunAsyncSpanish()
        {
            var task = _pipelines["extract_keywords"].RunAsync(_spanishInput);
            var actual = task.Result;

            CollectionAssert.AreEqual(_expectedSpanish, _toArray(actual));
        }

        [TestMethod]
        public void RunAsyncPortuguese()
        {
            var task = _pipelines["extract_keywords"].RunAsync(_portugueseInput);
            var actual = task.Result;

            CollectionAssert.AreEqual(_expectedPortuguese, _toArray(actual));
        }

        [TestMethod]
        public void RunDetailedEnglish()
        {
            var result = _pipelines["extract_keywords"].RunDetailed(_englishInput);

            _assertResult(result, _expectedEnglish);
        }

        [TestMethod]
        public void RunDetailedSpanish()
        {
            var result = _pipelines["extract_keywords"].RunDetailed(_spanishInput);

            _assertResult(result, _expectedSpanish);
        }

        [TestMethod]
        public void RunDetailedPortuguese()
        {
            var result = _pipelines["extract_keywords"].RunDetailed(_portugueseInput);

            _assertResult(result, _expectedPortuguese);
        }

        [TestMethod]
        public void RunDetailedAsyncEnglish()
        {
            var task = _pipelines["extract_keywords"].RunDetailedAsync(_englishInput);
            var result = task.Result;

            _assertResult(result, _expectedEnglish);
        }

        [TestMethod]
        public void RunDetailedAsyncSpanish()
        {
            var task = _pipelines["extract_keywords"].RunDetailedAsync(_spanishInput);
            var result = task.Result;

            _assertResult(result, _expectedSpanish);
        }

        [TestMethod]
        public void RunDetailedAsyncPortuguese()
        {
            var task = _pipelines["extract_keywords"].RunDetailedAsync(_portugueseInput);
            var result = task.Result;

            _assertResult(result, _expectedPortuguese);
        }

        [TestMethod]
        public void RunAsEnumerableEnglish()
        {
            var enumerable = _pipelines["extract_keywords"].RunAsEnumerable(_englishInput);
            var actual = enumerable.ToArray();

            Assert.AreEqual(_expectedEnumerableEnglish[0], actual[0]);
            Assert.AreEqual(_expectedEnumerableEnglish[1], actual[1]);
            Assert.AreEqual(_expectedEnumerableEnglish[2], actual[2]);

            CollectionAssert.AreEqual(_toArray(_expectedEnumerableEnglish[3]), _toArray(actual[3]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerableEnglish[4]), _toArray(actual[4]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerableEnglish[5]), _toArray(actual[5]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerableEnglish[6]), _toArray(actual[6]));
        }

        [TestMethod]
        public void RunAsEnumerableSpanish()
        {
            var enumerable = _pipelines["extract_keywords"].RunAsEnumerable(_spanishInput);
            var actual = enumerable.ToArray();

            Assert.AreEqual(_expectedEnumerableSpanish[0], actual[0]);
            Assert.AreEqual(_expectedEnumerableSpanish[1], actual[1]);
            Assert.AreEqual(_expectedEnumerableSpanish[2], actual[2]);

            CollectionAssert.AreEqual(_toArray(_expectedEnumerableSpanish[3]), _toArray(actual[3]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerableSpanish[4]), _toArray(actual[4]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerableSpanish[5]), _toArray(actual[5]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerableSpanish[6]), _toArray(actual[6]));
        }

        [TestMethod]
        public void RunAsEnumerablePortuguese()
        {
            var enumerable = _pipelines["extract_keywords"].RunAsEnumerable(_portugueseInput);
            var actual = enumerable.ToArray();

            Assert.AreEqual(_expectedEnumerablePortuguese[0], actual[0]);
            Assert.AreEqual(_expectedEnumerablePortuguese[1], actual[1]);
            Assert.AreEqual(_expectedEnumerablePortuguese[2], actual[2]);

            CollectionAssert.AreEqual(_toArray(_expectedEnumerablePortuguese[3]), _toArray(actual[3]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerablePortuguese[4]), _toArray(actual[4]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerablePortuguese[5]), _toArray(actual[5]));
            CollectionAssert.AreEqual(_toArray(_expectedEnumerablePortuguese[6]), _toArray(actual[6]));
        }
    }
}
