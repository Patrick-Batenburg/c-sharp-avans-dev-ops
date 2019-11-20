using AvansDevOps;
using Moq;
using Xunit;

namespace AvansDevOpsTests
{
    public class BacklogItemTests
    {
        [Fact]
        public void Should_AddBacklogItems()
        {
            //arrange
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem2 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogComponent> backlogComponent = new Mock<BacklogComponent>() { CallBase = true };

            //act
            backlogItem1.Object.Add(backlogItem2.Object);
            backlogItem1.Object.Add(backlogComponent.Object);

            //assert
            backlogItem1.Verify(x => x.Add(It.IsAny<BacklogItem>()), Times.Exactly(1));
            backlogItem1.Verify(x => x.Add(It.IsAny<BacklogComponent>()), Times.Exactly(2));
            Assert.Equal(backlogItem2.Object, backlogItem1.Object.BacklogItems[0]);
            Assert.Equal(backlogComponent.Object, backlogItem1.Object.BacklogItems[1]);
            Assert.Equal(2, backlogItem1.Object.BacklogItems.Count);
            Assert.Equal(BacklogItemType.Task, backlogItem1.Object.Type);
        }

        [Fact]
        public void Should_FinishBacklogItems()
        {
            //arrange
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem2 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem3 = new Mock<BacklogItem>() { CallBase = true };
            backlogItem1.SetupProperty(x => x.State, new BacklogComponentStateDoing());
            backlogItem2.SetupProperty(x => x.State, new BacklogComponentStateDoing());
            backlogItem3.SetupProperty(x => x.State, new BacklogComponentStateDoing());
            backlogItem1.SetupGet(x => x.Finished).Returns(true);
            backlogItem2.SetupGet(x => x.Finished).Returns(true);
            backlogItem3.SetupGet(x => x.Finished).Returns(true);

            //act
            backlogItem1.Object.Add(backlogItem2.Object);
            backlogItem1.Object.Add(backlogItem3.Object);
            backlogItem1.Object.Finish();

            //assert
            backlogItem1.Verify(x => x.Add(It.IsAny<BacklogItem>()), Times.Exactly(2));
            Assert.Equal(backlogItem2.Object, backlogItem1.Object.BacklogItems[0]);
            Assert.Equal(backlogItem3.Object, backlogItem1.Object.BacklogItems[1]);
            Assert.True(backlogItem2.Object.State.Finished);
            Assert.True(backlogItem3.Object.State.Finished);
            Assert.Equal(2, backlogItem1.Object.BacklogItems.Count);
            Assert.Equal(BacklogItemType.Task, backlogItem1.Object.Type);
            Assert.IsType<BacklogComponentStateDone>(backlogItem1.Object.State);
            Assert.IsType<BacklogComponentStateDone>(backlogItem2.Object.State);
            Assert.IsType<BacklogComponentStateDone>(backlogItem3.Object.State);
        }

        [Fact]
        public void Should_StartBacklogItems()
        {
            //arrange
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem2 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem3 = new Mock<BacklogItem>() { CallBase = true };
            backlogItem1.SetupProperty(x => x.State, new BacklogComponentStateToDo());
            backlogItem2.SetupProperty(x => x.State, new BacklogComponentStateToDo());
            backlogItem3.SetupProperty(x => x.State, new BacklogComponentStateToDo());

            //act
            backlogItem1.Object.Add(backlogItem2.Object);
            backlogItem1.Object.Add(backlogItem3.Object);
            backlogItem1.Object.Start();

            //assert
            backlogItem1.Verify(x => x.Add(It.IsAny<BacklogItem>()), Times.Exactly(2));
            Assert.Equal(backlogItem2.Object, backlogItem1.Object.BacklogItems[0]);
            Assert.Equal(backlogItem3.Object, backlogItem1.Object.BacklogItems[1]);
            Assert.False(backlogItem2.Object.State.Finished);
            Assert.False(backlogItem3.Object.State.Finished);
            Assert.Equal(2, backlogItem1.Object.BacklogItems.Count);
            Assert.Equal(BacklogItemType.Task, backlogItem1.Object.Type);
            Assert.IsType<BacklogComponentStateDoing>(backlogItem1.Object.State);
            Assert.IsType<BacklogComponentStateDoing>(backlogItem2.Object.State);
            Assert.IsType<BacklogComponentStateDoing>(backlogItem3.Object.State);
        }

        [Fact]
        public void Should_CancelBacklogItems()
        {
            //arrange
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem2 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem3 = new Mock<BacklogItem>() { CallBase = true };
            backlogItem1.SetupProperty(x => x.State, new BacklogComponentStateDoing());
            backlogItem2.SetupProperty(x => x.State, new BacklogComponentStateDoing());
            backlogItem3.SetupProperty(x => x.State, new BacklogComponentStateToDo());

            //act
            backlogItem1.Object.Add(backlogItem2.Object);
            backlogItem1.Object.Add(backlogItem3.Object);
            backlogItem1.Object.Cancel();

            //assert
            backlogItem1.Verify(x => x.Add(It.IsAny<BacklogItem>()), Times.Exactly(2));
            Assert.Equal(backlogItem2.Object, backlogItem1.Object.BacklogItems[0]);
            Assert.Equal(backlogItem3.Object, backlogItem1.Object.BacklogItems[1]);
            Assert.False(backlogItem2.Object.State.Finished);
            Assert.False(backlogItem3.Object.State.Finished);
            Assert.Equal(2, backlogItem1.Object.BacklogItems.Count);
            Assert.Equal(BacklogItemType.Task, backlogItem1.Object.Type);
            Assert.IsType<BacklogComponentStateToDo>(backlogItem1.Object.State);
            Assert.IsType<BacklogComponentStateToDo>(backlogItem2.Object.State);
            Assert.IsType<BacklogComponentStateToDo>(backlogItem3.Object.State);
        }

        [Fact]
        public void Should_FailToFinshBacklogItems()
        {
            //arrange
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem2 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem3 = new Mock<BacklogItem>() { CallBase = true };
            backlogItem1.SetupProperty(x => x.State, new BacklogComponentStateDoing());
            backlogItem2.SetupProperty(x => x.State, new BacklogComponentStateDoing());
            backlogItem3.SetupProperty(x => x.State, new BacklogComponentStateDoing());
            backlogItem1.SetupGet(x => x.Finished).Returns(false);
            backlogItem2.SetupGet(x => x.Finished).Returns(true);
            backlogItem3.SetupGet(x => x.Finished).Returns(false);

            //act
            backlogItem1.Object.Add(backlogItem2.Object);
            backlogItem1.Object.Add(backlogItem3.Object);
            backlogItem1.Object.Finish();

            //assert
            backlogItem1.Verify(x => x.Add(It.IsAny<BacklogItem>()), Times.Exactly(2));
            Assert.Equal(backlogItem2.Object, backlogItem1.Object.BacklogItems[0]);
            Assert.Equal(backlogItem3.Object, backlogItem1.Object.BacklogItems[1]);
            Assert.Equal(2, backlogItem1.Object.BacklogItems.Count);
            Assert.IsType<BacklogComponentStateDoing>(backlogItem1.Object.State);
            Assert.IsType<BacklogComponentStateDone>(backlogItem2.Object.State);
            Assert.IsType<BacklogComponentStateDoing>(backlogItem3.Object.State);
        }

        [Fact]
        public void Should_DoNothingWhenAddingOrRemoving()
        {
            //arrange
            Mock<BacklogTask> backlogTask = new Mock<BacklogTask>() { CallBase = true };
            Mock<BacklogComponent> backlogComponent = new Mock<BacklogComponent>() { CallBase = true };

            //act
            backlogTask.Object.Add(backlogComponent.Object);
            backlogTask.Object.Remove(backlogComponent.Object);
            backlogComponent.Object.Add(backlogTask.Object);
            backlogComponent.Object.Remove(backlogTask.Object);

            //assert
            backlogComponent.Verify(x => x.Add(It.IsAny<BacklogTask>()), Times.Exactly(1));
            backlogComponent.Verify(x => x.Remove(It.IsAny<BacklogTask>()), Times.Exactly(1));
            backlogTask.Verify(x => x.Add(It.IsAny<BacklogComponent>()), Times.Exactly(1));
            backlogTask.Verify(x => x.Remove(It.IsAny<BacklogComponent>()), Times.Exactly(1));
        }

        [Fact]
        public void Should_AddBeforeBacklogItem()
        {
            //arrange
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem2 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem3 = new Mock<BacklogItem>() { CallBase = true };

            //act
            backlogItem1.Object.Add(backlogItem2.Object);
            backlogItem1.Object.AddBefore(backlogItem3.Object, backlogItem2.Object);

            //assert
            backlogItem1.Verify(x => x.Add(It.IsAny<BacklogItem>()), Times.Exactly(1));
            backlogItem1.Verify(x => x.AddBefore(It.IsAny<BacklogItem>(), It.IsAny<BacklogItem>()), Times.Exactly(1));
            Assert.Equal(backlogItem3.Object, backlogItem1.Object.BacklogItems[0]);
            Assert.Equal(backlogItem2.Object, backlogItem1.Object.BacklogItems[1]);
            Assert.Equal(2, backlogItem1.Object.BacklogItems.Count);
        }

        [Fact]
        public void Should_AddAfterBacklogItem()
        {
            //arrange
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem2 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem3 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem4 = new Mock<BacklogItem>() { CallBase = true };

            //act
            backlogItem1.Object.Add(backlogItem2.Object);
            backlogItem1.Object.Add(backlogItem3.Object);
            backlogItem1.Object.AddAfter(backlogItem4.Object, backlogItem2.Object);

            //assert
            backlogItem1.Verify(x => x.Add(It.IsAny<BacklogItem>()), Times.Exactly(2));
            backlogItem1.Verify(x => x.AddAfter(It.IsAny<BacklogItem>(), It.IsAny<BacklogItem>()), Times.Exactly(1));
            Assert.Equal(backlogItem2.Object, backlogItem1.Object.BacklogItems[0]);
            Assert.Equal(backlogItem4.Object, backlogItem1.Object.BacklogItems[1]);
            Assert.Equal(3, backlogItem1.Object.BacklogItems.Count);
        }

        [Fact]
        public void Should_RemoveBacklogItem()
        {
            //arrange
            Mock<BacklogItem> backlogItem1 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem2 = new Mock<BacklogItem>() { CallBase = true };
            Mock<BacklogItem> backlogItem3 = new Mock<BacklogItem>() { CallBase = true };

            //act
            backlogItem1.Object.Add(backlogItem2.Object);
            backlogItem1.Object.Add(backlogItem3.Object);

            backlogItem1.Object.Remove(backlogItem2.Object);

            //assert
            backlogItem1.Verify(x => x.Add(It.IsAny<BacklogItem>()), Times.Exactly(2));
            backlogItem1.Verify(x => x.Remove(It.IsAny<BacklogItem>()), Times.Exactly(1));
            Assert.Equal(backlogItem3.Object, backlogItem1.Object.BacklogItems[0]);
            Assert.Single(backlogItem1.Object.BacklogItems);
        }
    }
}
