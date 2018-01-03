namespace PipeliningLibrary.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ImportTests
    {
        [TestMethod]
        public void Import()
        {
            var expected = "cowabunga";
            var pipelineGroup = new PipelineGroup();

            pipelineGroup.Import(new TextPipelines());

            var actual = pipelineGroup["sanitize_input"].Run("Cowabunga!");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ImportTypeT()
        {
            var expected = "cowabunga";
            var pipelineGroup = new PipelineGroup();

            pipelineGroup.Import<TextPipelines>();

            var actual = pipelineGroup["sanitize_input"].Run("Cowabunga!");

            Assert.AreEqual(expected, actual);
        }
    }
}
