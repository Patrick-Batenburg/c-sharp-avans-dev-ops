using AvansDevOps;
using Moq;
using Xunit;

namespace AvansDevOpsTests
{
    public class BacklogComponentStateTests
    {
        [Fact]
        public void Should_StartDoingBacklogItem()
        {
            //arrange
            int score = 10;
            int priority = 2;
            string description = "A backlog component";
            User user = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg"
            };

            Mock<BacklogComponent> backlogComponent = new Mock<BacklogComponent>(new BacklogComponentStateToDo()) { CallBase = true };
            Mock<BacklogItem> backlogItem = new Mock<BacklogItem>(new BacklogComponentStateToDo()) { CallBase = true };
            Mock<BacklogTask> backlogTask = new Mock<BacklogTask>(new BacklogComponentStateToDo()) { CallBase = true };

            //act
            backlogComponent.Object.Priority = priority;
            backlogComponent.Object.Score = score;
            backlogComponent.Object.AssignedTo = user;
            backlogComponent.Object.Description = description;
            backlogComponent.Object.Start();
            backlogItem.Object.Start();
            backlogTask.Object.Start();

            //assert
            Assert.True(backlogTask.Object.CanFinish);
            Assert.True(backlogItem.Object.CanFinish);
            Assert.Equal(score, backlogComponent.Object.Score);
            Assert.Equal(priority, backlogComponent.Object.Priority);
            Assert.Equal(user, backlogComponent.Object.AssignedTo);
            Assert.Equal(description, backlogComponent.Object.Description);
            Assert.IsType<BacklogComponentStateDoing>(backlogComponent.Object.State);
            Assert.IsType<BacklogComponentStateDoing>(backlogItem.Object.State);
            Assert.IsType<BacklogComponentStateDoing>(backlogTask.Object.State);
            Assert.True(backlogTask.Object.State.CanFinish);
        }

        [Fact]
        public void Should_DoNothingWhenOtherStatesCalled()
        {
            //arrange
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>(new BacklogComponentStateToDo()) { CallBase = true };
            Mock<BacklogItem> backlogItem2 = new Mock<BacklogItem>(new BacklogComponentStateDone()) { CallBase = true };
            backlogItem2.SetupGet(x => x.CanFinish).Returns(true);

            //act
            backlogItem1.Object.Finish();
            backlogItem1.Object.Cancel();
            backlogItem2.Object.Start();

            //assert
            Assert.True(backlogItem2.Object.CanFinish);
            Assert.True(backlogItem1.Object.CanFinish);
            Assert.IsType<BacklogComponentStateToDo>(backlogItem1.Object.State);
            Assert.IsType<BacklogComponentStateDone>(backlogItem2.Object.State);
            Assert.False(backlogItem2.Object.State.CanFinish);
        }

        [Fact]
        public void ShouldNot_FinishBacklogItem()
        {
            //arrange
            Mock<BacklogComponent> backlogComponent = new Mock<BacklogComponent>() { CallBase = true };
            Mock<BacklogItem> backlogItem = new Mock<BacklogItem>() { CallBase = true };
            backlogComponent.Object.State = new BacklogComponentStateDoing();
            backlogItem.Object.State = new BacklogComponentStateDoing();

            //act
            backlogComponent.Object.Finish();
            backlogItem.Object.Finish();

            //assert
            Assert.IsType<BacklogComponentStateDoing>(backlogComponent.Object.State);
            Assert.IsType<BacklogComponentStateDoing>(backlogItem.Object.State);
        }

        [Fact]
        public void Should_FinishBacklogItem()
        {
            //arrange
            Mock<BacklogComponent> backlogComponent = new Mock<BacklogComponent>() { CallBase = true };
            Mock<BacklogItem> backlogItem = new Mock<BacklogItem>() { CallBase = true };
            backlogComponent.Object.State = new BacklogComponentStateDoing();
            backlogItem.Object.State = new BacklogComponentStateDoing();
            backlogComponent.SetupGet(x => x.Finished).Returns(true);
            backlogItem.SetupGet(x => x.Finished).Returns(true);

            //act
            backlogComponent.Object.Finish();
            backlogItem.Object.Finish();

            //assert
            Assert.IsType<BacklogComponentStateDone>(backlogComponent.Object.State);
            Assert.IsType<BacklogComponentStateDone>(backlogItem.Object.State);
            Assert.True(backlogComponent.Object.State.Finished);
            Assert.True(backlogItem.Object.State.Finished);
        }

        [Fact]
        public void Should_CancelBacklogItem()
        {
            //arrange
            Mock<BacklogComponent> backlogComponent = new Mock<BacklogComponent>() { CallBase = true };
            Mock<BacklogItem> backlogItem = new Mock<BacklogItem>() { CallBase = true };
            backlogComponent.Object.State = new BacklogComponentStateDoing();
            backlogItem.Object.State = new BacklogComponentStateDoing();

            //act
            backlogComponent.Object.Cancel();
            backlogItem.Object.Cancel();

            //assert
            Assert.IsType<BacklogComponentStateToDo>(backlogComponent.Object.State);
            Assert.IsType<BacklogComponentStateToDo>(backlogItem.Object.State);
        }

        [Fact]
        public void Should_CancelFinishBacklogItem()
        {
            //arrange
            Mock<BacklogComponent> backlogComponent = new Mock<BacklogComponent>() { CallBase = true };
            Mock<BacklogItem> backlogItem = new Mock<BacklogItem>() { CallBase = true };
            backlogComponent.Object.State = new BacklogComponentStateDone();
            backlogItem.Object.State = new BacklogComponentStateDone();
            backlogComponent.SetupGet(x => x.Finished).Returns(true);
            backlogItem.SetupGet(x => x.Finished).Returns(true);

            //act
            backlogComponent.Object.Cancel();
            backlogItem.Object.Cancel();

            //assert
            Assert.IsType<BacklogComponentStateToDo>(backlogComponent.Object.State);
            Assert.IsType<BacklogComponentStateToDo>(backlogItem.Object.State);
        }
    }
}
