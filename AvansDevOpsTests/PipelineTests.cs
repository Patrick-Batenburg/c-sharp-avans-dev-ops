using AvansDevOps;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AvansDevOpsTests
{
    public class PipelineTests
    {
        [Fact]
        public void Should_AddPipelineTask()
        {
            //arrange
            string title = "New pipeline";
            Mock<Pipeline> pipeline = new Mock<Pipeline>() { CallBase = true };
            Mock<IPipelineTask> task = new Mock<IPipelineTask>() { CallBase = true };
            Mock<Repository> repository = new Mock<Repository>() { CallBase = true };
            Mock<RepositoryBranch> repositoryBranch = new Mock<RepositoryBranch>() { CallBase = true };

            //act
            pipeline.Object.Name = title;
            pipeline.Object.Repository = repository.Object;
            pipeline.Object.Branch = repositoryBranch.Object;
            pipeline.Object.Add(task.Object);

            //assert
            pipeline.Verify(x => x.Add(It.IsAny<IPipelineTask>()), Times.Exactly(1));
            Assert.Single(pipeline.Object.Tasks);
            Assert.Equal(title, pipeline.Object.Name);
            Assert.Equal(repository.Object, pipeline.Object.Repository);
            Assert.Equal(repositoryBranch.Object, pipeline.Object.Branch);
            Assert.Single(pipeline.Object.Tasks);
        }

        [Fact]
        public void Should_HaveEmptyPipelineTasks()
        {
            //arrange
            string title = "New pipeline";
            Mock<Pipeline> pipeline = new Mock<Pipeline>() { CallBase = true };
            pipeline.SetupGet(x => x.Name).Returns(title);

            //assert
            pipeline.Verify(x => x.Add(It.IsAny<IPipelineTask>()), Times.Exactly(0));
            Assert.Equal(title, pipeline.Object.Name);
            Assert.Empty(pipeline.Object.Tasks);
        }

        [Fact]
        public void Should_AddBeforePipelineTask()
        {
            //arrange
            Mock<Pipeline> pipeline = new Mock<Pipeline>() { CallBase = true };
            Mock<IPipelineTask> task1 = new Mock<IPipelineTask>() { CallBase = true };
            Mock<IPipelineTask> task2 = new Mock<IPipelineTask>() { CallBase = true };
            Mock<IPipelineTask> task3 = new Mock<IPipelineTask>() { CallBase = true };

            //act
            pipeline.Object.Add(task1.Object);
            pipeline.Object.AddBefore(task2.Object, task1.Object);

            //assert
            pipeline.Verify(x => x.Add(It.IsAny<IPipelineTask>()), Times.Exactly(1));
            pipeline.Verify(x => x.AddBefore(It.IsAny<IPipelineTask>(), It.IsAny<IPipelineTask>()), Times.Exactly(1));
            Assert.Equal(task2.Object, pipeline.Object.Tasks[0]);
            Assert.Equal(task1.Object, pipeline.Object.Tasks[1]);
            Assert.Equal(2, pipeline.Object.Tasks.Count);
        }

        [Fact]
        public void Should_AddAfterPipelineTask()
        {
            //arrange
            Mock<Pipeline> pipeline = new Mock<Pipeline>() { CallBase = true };
            Mock<IPipelineTask> task1 = new Mock<IPipelineTask>() { CallBase = true };
            Mock<IPipelineTask> task2 = new Mock<IPipelineTask>() { CallBase = true };
            Mock<IPipelineTask> task3 = new Mock<IPipelineTask>() { CallBase = true };

            //act
            pipeline.Object.Add(task1.Object);
            pipeline.Object.Add(task2.Object);
            pipeline.Object.AddAfter(task3.Object, task1.Object);

            //assert
            pipeline.Verify(x => x.Add(It.IsAny<IPipelineTask>()), Times.Exactly(2));
            pipeline.Verify(x => x.AddAfter(It.IsAny<IPipelineTask>(), It.IsAny<IPipelineTask>()), Times.Exactly(1));
            Assert.Equal(task1.Object, pipeline.Object.Tasks[0]);
            Assert.Equal(task3.Object, pipeline.Object.Tasks[1]);
            Assert.Equal(task2.Object, pipeline.Object.Tasks[2]);
            Assert.Equal(3, pipeline.Object.Tasks.Count);
        }

        [Fact]
        public void Should_RemovePipelineTask()
        {
            //arrange
            Mock<Pipeline> pipeline = new Mock<Pipeline>() { CallBase = true };
            Mock<IPipelineTask> task1 = new Mock<IPipelineTask>() { CallBase = true };
            Mock<IPipelineTask> task2 = new Mock<IPipelineTask>() { CallBase = true };

            //act
            pipeline.Object.Add(task1.Object);
            pipeline.Object.Add(task2.Object);

            pipeline.Object.Remove(task1.Object);

            //assert
            pipeline.Verify(x => x.Add(It.IsAny<IPipelineTask>()), Times.Exactly(2));
            pipeline.Verify(x => x.Remove(It.IsAny<IPipelineTask>()), Times.Exactly(1));
            Assert.Equal(task2.Object, pipeline.Object.Tasks[0]);
            Assert.Single(pipeline.Object.Tasks);
        }
    }
}
