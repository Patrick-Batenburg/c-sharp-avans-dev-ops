using AvansDevOps;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AvansDevOpsTests
{
    public class SprintTests
    {
        [Fact]
        public void Should_CreateSprint()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>(new SprintStateCreated()) { CallBase = true };
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>() { CallBase = true };

            //act
            sprint.Object.Add(backlogItem1.Object);

            //assert
            Assert.IsType<SprintStateCreated>(sprint.Object.CurrentState);
            Assert.Null(sprint.Object.PreviousState);
            Assert.Single(sprint.Object.BacklogComponents);
            Assert.True(sprint.Object.Editable);
            Assert.Equal(backlogItem1.Object, sprint.Object.BacklogComponents[0]);
            Assert.Equal(sprint.Object.Ended, sprint.Object.Created);
            Assert.Equal(1, sprint.Object.BacklogItemsToDo);
        }

        [Fact]
        public void Should_NotifyObservers()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>() { CallBase = true };
            Mock<NotificationHandler> notificationHandler = new Mock<NotificationHandler>() { CallBase = true };
            Mock<EmailNotificationHandler> emailNotificationHandler = new Mock<EmailNotificationHandler>() { CallBase = true };
            Mock<SlackNotificationHandler> slackNotificationHandler = new Mock<SlackNotificationHandler>() { CallBase = true };
            Mock<IObserver> observer1 = new Mock<IObserver>() { CallBase = true };
            Mock<SprintEmailObserver> observer2 = new Mock<SprintEmailObserver>(emailNotificationHandler.Object) { CallBase = true };
            Mock<SprintSlackObserver> observer3 = new Mock<SprintSlackObserver>(slackNotificationHandler.Object) { CallBase = true };
            Mock<SprintEmailObserver> observer4 = new Mock<SprintEmailObserver>(notificationHandler.Object) { CallBase = true };
            Mock<SprintSlackObserver> observer5 = new Mock<SprintSlackObserver>(notificationHandler.Object) { CallBase = true };
            sprint.SetupGet(x => x.CurrentState.Type).Returns(SprintStateType.Finished);

            //act
            sprint.Object.Attach(observer1.Object);
            sprint.Object.Attach(observer2.Object);
            sprint.Object.Attach(observer3.Object);
            sprint.Object.Attach(observer4.Object);
            sprint.Object.Attach(observer5.Object);
            sprint.Object.NextSprint();
            sprint.Object.NextSprint();
            sprint.Object.NextSprint();
            sprint.Object.Notify();

            //assert
            sprint.Verify(x => x.Notify(), Times.Exactly(1));
            sprint.Verify(x => x.Attach(It.IsAny<IObserver>()), Times.Exactly(5));
            sprint.Verify(x => x.Attach(It.IsAny<SprintEmailObserver>()), Times.Exactly(2));
            sprint.Verify(x => x.Attach(It.IsAny<SprintSlackObserver>()), Times.Exactly(2));
            observer1.Verify(x => x.Update(It.IsAny<ISubject>()), Times.Exactly(1));
            observer2.Verify(x => x.Update(It.IsAny<ISubject>()), Times.Exactly(1));
            observer3.Verify(x => x.Update(It.IsAny<ISubject>()), Times.Exactly(1));
            observer4.Verify(x => x.Update(It.IsAny<ISubject>()), Times.Exactly(1));
            notificationHandler.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Exactly(2));
            emailNotificationHandler.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Exactly(1));
            slackNotificationHandler.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Exactly(1));
            Assert.Equal(SprintType.Closing, sprint.Object.Type);
        }


        [Fact]
        public void Should_StartRelease()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>() { CallBase = true };
            Mock<PipelineBuild> pipelineBuild = new Mock<PipelineBuild>() { CallBase = true };
            sprint.SetupGet(x => x.CurrentState.Type).Returns(SprintStateType.Finished);

            //act
            pipelineBuild.Object.Tasks.Add(new PipelineDotNetRestoreTask());
            sprint.Object.DevelopmentPipeline = pipelineBuild.Object;
            sprint.Object.NextSprint();
            sprint.Object.NextSprint();
            sprint.Object.NextSprint();
            sprint.Object.StartRelease();

            //assert
            Assert.Equal(SprintType.Closing, sprint.Object.Type);
            Assert.Equal(pipelineBuild.Object, sprint.Object.DevelopmentPipeline);
            Assert.Equal(0, pipelineBuild.Object.ExitCode);
            Assert.Equal("Success!", pipelineBuild.Object.Logs);
            sprint.Verify(x => x.StartRelease(), Times.Exactly(1));
            pipelineBuild.Verify(x => x.Run(), Times.Exactly(1));
        }

        [Fact]
        public void Should_NotifyReleaseFailed()
        {
            //arrange
            string error = "Unexpected error occured";
            Mock<Sprint> sprint = new Mock<Sprint>() { CallBase = true };
            Mock<PipelineBuild> pipelineBuild = new Mock<PipelineBuild>() { CallBase = true };
            Mock<PipelineDotNetRestoreTask> pipelineTask = new Mock<PipelineDotNetRestoreTask>() { CallBase = true };
            sprint.SetupGet(x => x.CurrentState.Type).Returns(SprintStateType.Finished);
            pipelineTask.Setup(x => x.Execute()).Returns(-1);
            pipelineTask.SetupGet(x => x.Logs).Returns(error);
            pipelineBuild.SetupGet(x => x.Tasks).Returns(new List<IPipelineTask>() { pipelineTask .Object });

            //act
            sprint.Object.DevelopmentPipeline = pipelineBuild.Object;
            sprint.Object.NextSprint();
            sprint.Object.NextSprint();
            sprint.Object.NextSprint();
            sprint.Object.StartRelease();

            //assert
            Assert.Equal(SprintType.Closing, sprint.Object.Type);
            Assert.Equal(pipelineBuild.Object, sprint.Object.DevelopmentPipeline);
            Assert.Equal(-1, pipelineBuild.Object.ExitCode);
            Assert.Equal(error, pipelineBuild.Object.Logs);
            sprint.Verify(x => x.StartRelease(), Times.Exactly(1));
            pipelineBuild.Verify(x => x.Run(), Times.Exactly(1));
        }


        [Fact]
        public void Should_AttachObservers()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>(new SprintStateCreated()) { CallBase = true };
            Mock<IObserver> observer1 = new Mock<IObserver>() { CallBase = true };
            Mock<IObserver> observer2 = new Mock<IObserver>() { CallBase = true };

            //act
            sprint.Object.Attach(observer1.Object);
            sprint.Object.Attach(observer2.Object);

            //assert
            Assert.IsType<SprintStateCreated>(sprint.Object.CurrentState);
            Assert.Null(sprint.Object.PreviousState);
            sprint.Verify(x => x.Attach(It.IsAny<IObserver>()), Times.Exactly(2));
        }

        [Fact]
        public void Should_DetachObservers()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>(new SprintStateCreated()) { CallBase = true };
            Mock<IObserver> observer1 = new Mock<IObserver>() { CallBase = true };
            Mock<IObserver> observer2 = new Mock<IObserver>() { CallBase = true };

            //act
            sprint.Object.Attach(observer1.Object);
            sprint.Object.Attach(observer2.Object);
            sprint.Object.Detach(observer1.Object);
            sprint.Object.Detach(observer2.Object);

            //assert
            Assert.IsType<SprintStateCreated>(sprint.Object.CurrentState);
            Assert.Null(sprint.Object.PreviousState);
            sprint.Verify(x => x.Attach(It.IsAny<IObserver>()), Times.Exactly(2));
            sprint.Verify(x => x.Detach(It.IsAny<IObserver>()), Times.Exactly(2));
        }
    }
}
