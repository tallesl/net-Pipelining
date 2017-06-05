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

        [TestMethod]
        public void Empty()
        {
            var result = new FriendlyPipelineResult(new PipelineResult());
            var actual = Json(result);
            var expected = FriendlyTestsResources.Empty;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serializable()
        {
            var result = Result(new[] { 1, 2, 3, });
            var actual = Json(result);
            var expected = FriendlyTestsResources.Serializable;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotSerializable()
        {
            var result = Result(this);
            var actual = Json(result);
            var expected = FriendlyTestsResources.NotSerializable;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotSerializableExpando()
        {
            var expando = new ExpandoObject();

            ((dynamic)expando).Serializable = 123;
            ((dynamic)expando).NotSerializable = this;

            var result = Result(expando);
            var actual = Json(result);
            var expected = FriendlyTestsResources.NotSerializableExpando;

            Assert.AreEqual(expected, actual);
        }
    }
}
