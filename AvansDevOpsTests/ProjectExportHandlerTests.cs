using AvansDevOps;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AvansDevOpsTests
{
    public class ProjectExportHandlerTests
    {
        [Fact]
        public void Should_ExportSprintToPdf()
        {
            //arrange
            IExportHandler exportHandler = new PdfExportHandler();
            Mock<Project> project = new Mock<Project>(exportHandler) { CallBase = true };

            //act
            project.Object.GenerateReport();

            //assert
            Assert.IsType<PdfExportHandler>(project.Object.ExportHandler);
            project.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToPdfWithHeadersAndFooters()
        {
            //arrange
            IExportHandler exportHandler = new PdfExportHandler()
            {
                HasHeader = true,
                HasFooter = true
            };
            Mock<Project> project = new Mock<Project>(exportHandler) { CallBase = true };

            //act
            project.Object.GenerateReport();

            //assert
            Assert.IsType<PdfExportHandler>(project.Object.ExportHandler);
            project.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToDocxWithHeadersAndFooters()
        {
            //arrange
            IExportHandler exportHandler = new DocxExportHandler()
            {
                HasHeader = true,
                HasFooter = true
            };
            Mock<Project> project = new Mock<Project>(exportHandler) { CallBase = true };

            //act
            project.Object.GenerateReport();

            //assert
            Assert.IsType<DocxExportHandler>(project.Object.ExportHandler);
            project.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToDocx()
        {
            //arrange
            IExportHandler exportHandler = new DocxExportHandler();
            Mock<Project> project = new Mock<Project>(exportHandler) { CallBase = true };

            //act
            project.Object.GenerateReport();

            //assert
            Assert.IsType<DocxExportHandler>(project.Object.ExportHandler);
            project.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToPngWithHeadersAndFooters()
        {
            //arrange
            IExportHandler exportHandler = new PngExportHandler()
            {
                HasHeader = true,
                HasFooter = true
            };
            Mock<Project> project = new Mock<Project>(exportHandler) { CallBase = true };

            //act
            project.Object.GenerateReport();

            //assert
            Assert.IsType<PngExportHandler>(project.Object.ExportHandler);
            project.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExportSprintToPng()
        {
            //arrange
            IExportHandler exportHandler = new PngExportHandler();
            Mock<Project> project = new Mock<Project>(exportHandler) { CallBase = true };

            //act
            project.Object.GenerateReport();

            //assert
            Assert.IsType<PngExportHandler>(project.Object.ExportHandler);
            project.Verify(x => x.GenerateReport(), Times.Exactly(1));
        }
    }
}
