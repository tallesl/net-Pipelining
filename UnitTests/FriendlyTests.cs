namespace PipeliningLibrary.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using PipeliningLibrary.Friendly;
    using System;
    using System.Dynamic;

    [TestClass]
    public class FriendlyTests
    {
        private FriendlyPipelineResult Result(object output)
        {
            return new FriendlyPipelineResult(
                new PipelineResult
                {
                    Id = "some_id",
                    Output = output,
                    Success = true,
                    ElapsedTime = TimeSpan.FromSeconds(1),
                    Pipes = new[]
                    {
                        new PipeResult
                        {
                            Pipe = typeof(BubbleSortPipe),
                            Started = new DateTime(2017, 06, 05, 15, 16, 17),
                            Ended = new DateTime(2017, 06, 05, 15, 16, 18),
                            Exception = null,
                        },
                    }
                }
            );
        }

        private string Json(FriendlyPipelineResult result)
        {
            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        private string NormalizeLineEndings(string s)
        {
            return s.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }

        [TestMethod]
        public void Empty()
        {
            var actual = NormalizeLineEndings(Json(new FriendlyPipelineResult(new PipelineResult())));
            var expected = NormalizeLineEndings(FriendlyTestsResources.Empty);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serializable()
        {
            var actual = NormalizeLineEndings(Json(Result(new[] { 1, 2, 3, })));
            var expected = NormalizeLineEndings(FriendlyTestsResources.Serializable);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotSerializable()
        {
            var actual = NormalizeLineEndings(Json(Result(this)));
            var expected = NormalizeLineEndings(FriendlyTestsResources.NotSerializable);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotSerializableExpando()
        {
            var expando = new ExpandoObject();

            ((dynamic)expando).Serializable = 123;
            ((dynamic)expando).NotSerializable = this;

            var actual = NormalizeLineEndings(Json(Result(expando)));
            var expected = NormalizeLineEndings(FriendlyTestsResources.NotSerializableExpando);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NullProperty()
        {
            var expando = new ExpandoObject();

            ((dynamic)expando).NullProperty = null;

            var actual = NormalizeLineEndings(Json(Result(expando)));
            var expected = NormalizeLineEndings(FriendlyTestsResources.NullProperty);

            Assert.AreEqual(expected, actual);
        }
    }
}
