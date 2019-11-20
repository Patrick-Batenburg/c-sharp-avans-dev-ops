using AvansDevOps;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AvansDevOpsTests
{
    public class PipelineTaskTests
    {
        [Fact]
        public void Should_ExecuteDotNetRestoreTask()
        {
            //arrange
            string name = ".NET Restore";
            string log = "Success!";
            Mock<PipelineDotNetRestoreTask> task = new Mock<PipelineDotNetRestoreTask>() { CallBase = true };

            //act
            task.Object.Execute();

            //assert
            Assert.Equal(log, task.Object.Logs);
            Assert.Equal(name, task.Object.Name);
            task.Verify(x => x.Execute(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExecuteDotNetBuildTask()
        {
            //arrange
            string name = ".NET Build";
            string log = "Success!";
            Mock<PipelineDotNetBuildTask> task = new Mock<PipelineDotNetBuildTask>() { CallBase = true };

            //act
            task.Object.Execute();

            //assert
            Assert.Equal(log, task.Object.Logs);
            Assert.Equal(name, task.Object.Name);
            task.Verify(x => x.Execute(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExecuteDotNetTestTask()
        {
            //arrange
            string name = ".NET Test";
            string log = "Success!";
            Mock<PipelineDotNetTestTask> task = new Mock<PipelineDotNetTestTask>() { CallBase = true };

            //act
            task.Object.Execute();

            //assert
            Assert.Equal(log, task.Object.Logs);
            Assert.Equal(name, task.Object.Name);
            task.Verify(x => x.Execute(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExecuteDotNetPublishTask()
        {
            //arrange
            string name = ".NET Publish";
            string log = "Success!";
            Mock<PipelineDotNetPublishTask> task = new Mock<PipelineDotNetPublishTask>() { CallBase = true };

            //act
            task.Object.Execute();

            //assert
            Assert.Equal(log, task.Object.Logs);
            Assert.Equal(name, task.Object.Name);
            task.Verify(x => x.Execute(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExecuteMavenPackageTask()
        {
            //arrange
            string name = "Maven Package";
            string log = "Success!";
            Mock<PipelineMavenPackageTask> task = new Mock<PipelineMavenPackageTask>() { CallBase = true };

            //act
            task.Object.Execute();

            //assert
            Assert.Equal(log, task.Object.Logs);
            Assert.Equal(name, task.Object.Name);
            task.Verify(x => x.Execute(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExecuteMavenTestTask()
        {
            //arrange
            string name = "Maven Test";
            string log = "Success!";
            Mock<PipelineMavenTestTask> task = new Mock<PipelineMavenTestTask>() { CallBase = true };

            //act
            task.Object.Execute();

            //assert
            Assert.Equal(log, task.Object.Logs);
            Assert.Equal(name, task.Object.Name);
            task.Verify(x => x.Execute(), Times.Exactly(1));
        }

        [Fact]
        public void Should_ExecutePublishArtifactTask()
        {
            //arrange
            string name = "Publish Artifact: drop";
            string log = "Success!";
            Mock<PipelinePublishArtifactTask> task = new Mock<PipelinePublishArtifactTask>() { CallBase = true };

            //act
            task.Object.Execute();

            //assert
            Assert.Equal(log, task.Object.Logs);
            Assert.Equal(name, task.Object.Name);
            task.Verify(x => x.Execute(), Times.Exactly(1));
        }
    }
}
