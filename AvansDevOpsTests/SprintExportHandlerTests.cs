using AvansDevOps;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AvansDevOpsTests
{
    public class SprintExportHandlerTests
    {
        [Fact]
        public void Should_ExportSprintToPdf()
        {
            //arrange
            SprintStateActive sprintState = new SprintStateActive();
            IExportHandler exportHandler = new PdfExportHandler();
            Mock<Sprint> sprint = new Mock<Sprint>(sprintState, exportHandler) { CallBase = true };

            //act
            sprint.Object.GenerateReport();

            //assert
            Assert.IsType<SprintStateActive>(sprint.Object.CurrentState);
            Assert.IsType<PdfExportHandler>(sprint.Object.ExportHandler);
            sprint.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToPdfWithHeadersAndFooters()
        {
            //arrange
            SprintStateActive sprintState = new SprintStateActive();
            IExportHandler exportHandler = new PdfExportHandler()
            {
                HasHeader = true,
                HasFooter = true
            };
            Mock<Sprint> sprint = new Mock<Sprint>(sprintState, exportHandler) { CallBase = true };

            //act
            sprint.Object.GenerateReport();

            //assert
            Assert.IsType<SprintStateActive>(sprint.Object.CurrentState);
            Assert.IsType<PdfExportHandler>(sprint.Object.ExportHandler);
            sprint.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToDocxWithHeadersAndFooters()
        {
            //arrange
            SprintStateActive sprintState = new SprintStateActive();
            IExportHandler exportHandler = new DocxExportHandler()
            {
                HasHeader = true,
                HasFooter = true
            };
            Mock<Sprint> sprint = new Mock<Sprint>(sprintState, exportHandler) { CallBase = true };

            //act
            sprint.Object.GenerateReport();

            //assert
            Assert.IsType<SprintStateActive>(sprint.Object.CurrentState);
            Assert.IsType<DocxExportHandler>(sprint.Object.ExportHandler);
            sprint.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToDocx()
        {
            //arrange
            SprintStateActive sprintState = new SprintStateActive();
            IExportHandler exportHandler = new DocxExportHandler();
            Mock<Sprint> sprint = new Mock<Sprint>(sprintState, exportHandler) { CallBase = true };

            //act
            sprint.Object.GenerateReport();

            //assert
            Assert.IsType<SprintStateActive>(sprint.Object.CurrentState);
            Assert.IsType<DocxExportHandler>(sprint.Object.ExportHandler);
            sprint.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToPngWithHeadersAndFooters()
        {
            //arrange
            SprintStateActive sprintState = new SprintStateActive();
            IExportHandler exportHandler = new PngExportHandler()
            {
                HasHeader = true,
                HasFooter = true
            };
            Mock<Sprint> sprint = new Mock<Sprint>(sprintState, exportHandler) { CallBase = true };

            //act
            sprint.Object.GenerateReport();

            //assert
            Assert.IsType<SprintStateActive>(sprint.Object.CurrentState);
            Assert.IsType<PngExportHandler>(sprint.Object.ExportHandler);
            sprint.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToPng()
        {
            //arrange
            SprintStateActive sprintState = new SprintStateActive();
            IExportHandler exportHandler = new PngExportHandler();
            Mock<Sprint> sprint = new Mock<Sprint>(sprintState, exportHandler) { CallBase = true };

            //act
            sprint.Object.GenerateReport();

            //assert
            Assert.IsType<SprintStateActive>(sprint.Object.CurrentState);
            Assert.IsType<PngExportHandler>(sprint.Object.ExportHandler);
            sprint.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }
    }
}
