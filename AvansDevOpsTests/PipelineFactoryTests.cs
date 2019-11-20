using AvansDevOps;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AvansDevOpsTests
{
    public class PipelineFactoryTests
    {
        [Fact]
        public void Should_CreateCsharpPipelineFromFactory()
        {
            //arrange
            string buildPipelineName = ".NET Core Build Pipeline";
            string releasePipelineName = ".NET Core Release Pipeline";
            Mock<CsharpPipelineFactory> factory = new Mock<CsharpPipelineFactory>() { CallBase = true };

            //act
            Pipeline buildPipeline = factory.Object.CreateBuildPipeline();
            Pipeline releasePipeline = factory.Object.CreateReleasePipeline();

            //assert
            Assert.Equal(buildPipelineName, buildPipeline.Name);
            Assert.Equal(releasePipelineName, releasePipeline.Name);
            Assert.Equal(3, buildPipeline.Tasks.Count);
            Assert.Equal(5, releasePipeline.Tasks.Count);
            factory.Verify(x => x.CreateBuildPipeline(), Times.Exactly(1));
            factory.Verify(x => x.CreateReleasePipeline(), Times.Exactly(1));
        }

        [Fact]
        public void Should_CreateJavaPipelineFromFactory()
        {
            //arrange
            string buildPipelineName = "Java Build Pipeline with Maven";
            string releasePipelineName = "Java Release Pipeline with Maven";
            Mock<JavaPipelineFactory> factory = new Mock<JavaPipelineFactory>() { CallBase = true };

            //act
            Pipeline buildPipeline = factory.Object.CreateBuildPipeline();
            Pipeline releasePipeline = factory.Object.CreateReleasePipeline();

            //assert
            Assert.Equal(buildPipelineName, buildPipeline.Name);
            Assert.Equal(releasePipelineName, releasePipeline.Name);
            Assert.Equal(2, buildPipeline.Tasks.Count);
            Assert.Equal(3, releasePipeline.Tasks.Count);
            factory.Verify(x => x.CreateBuildPipeline(), Times.Exactly(1));
            factory.Verify(x => x.CreateReleasePipeline(), Times.Exactly(1));
        }
    }
}
