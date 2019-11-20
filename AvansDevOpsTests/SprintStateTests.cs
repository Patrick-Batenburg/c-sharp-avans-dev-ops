using AvansDevOps;
using Moq;
using System;
using Xunit;

namespace AvansDevOpsTests
{
    public class SprintStateTests
    {
        [Fact]
        public void Should_CreateSprint()
        {
            //arrange
            Sprint sprint2 = new Sprint(new SprintStateCreated());


            string name = "Sprint Example";
            Mock<Sprint> sprint = new Mock<Sprint>(new SprintStateCreated()) { CallBase = true };

            //act
            sprint.Object.Name = name;

            //assert
            Assert.Equal(name, sprint.Object.Name);
            Assert.IsType<SprintStateCreated>(sprint.Object.CurrentState);
            Assert.Null(sprint.Object.PreviousState);
            Assert.True(sprint.Object.Editable);
        }

        [Fact]
        public void Should_StartSprint()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>(new SprintStateCreated()) { CallBase = true };

            //act
            sprint.Object.Start();

            //assert
            Assert.IsType<SprintStateActive>(sprint.Object.CurrentState);
            Assert.IsType<SprintStateCreated>(sprint.Object.PreviousState);
            Assert.Equal("This sprint is active.", sprint.Object.CurrentState.StateDescription);
            Assert.Equal(SprintStateType.Active, sprint.Object.CurrentState.Type);
            Assert.False(sprint.Object.Editable);
        }

        [Fact]
        public void Should_FinishSprint()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>() { CallBase = true };
            sprint.Object.CurrentState = new SprintStateActive();

            //act
            sprint.Object.Finish();

            //assert
            Assert.IsType<SprintStateFinished>(sprint.Object.CurrentState);
            Assert.IsType<SprintStateActive>(sprint.Object.PreviousState);
            Assert.Equal("This sprint is finished.", sprint.Object.CurrentState.StateDescription);
            Assert.Equal(SprintStateType.Finished, sprint.Object.CurrentState.Type);
            Assert.False(sprint.Object.Editable);
        }

        [Fact]
        public void Should_CancelActiveSprint()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>() { CallBase = true };
            sprint.Object.CurrentState = new SprintStateActive();

            //act
            sprint.Object.Cancel();

            //assert
            Assert.IsType<SprintStateCanceled>(sprint.Object.CurrentState);
            Assert.IsType<SprintStateActive>(sprint.Object.PreviousState);
            Assert.Equal("This sprint has been canceled.", sprint.Object.CurrentState.StateDescription);
            Assert.Equal(SprintStateType.Canceled, sprint.Object.CurrentState.Type);
            Assert.False(sprint.Object.Editable);
        }

        [Fact]
        public void Should_CancelFinishedSprint()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>() { CallBase = true };
            sprint.Object.CurrentState = new SprintStateFinished();

            //act
            sprint.Object.Cancel();

            //assert
            Assert.IsType<SprintStateCanceled>(sprint.Object.CurrentState);
            Assert.IsType<SprintStateFinished>(sprint.Object.PreviousState);
            Assert.Equal("This sprint has been canceled.", sprint.Object.CurrentState.StateDescription);
            Assert.Equal(SprintStateType.Canceled, sprint.Object.CurrentState.Type);
            Assert.False(sprint.Object.Editable);
        }

        [Fact]
        public void Should_ReactivateFinishedSprint()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>() { CallBase = true };
            sprint.Object.CurrentState = new SprintStateFinished();

            //act
            sprint.Object.Start();

            //assert
            Assert.IsType<SprintStateActive>(sprint.Object.CurrentState);
            Assert.IsType<SprintStateFinished>(sprint.Object.PreviousState);
            Assert.Equal("This sprint is active.", sprint.Object.CurrentState.StateDescription);
            Assert.Equal(SprintStateType.Active, sprint.Object.CurrentState.Type);
            Assert.False(sprint.Object.Editable);
        }

        [Fact]
        public void Should_CloseFinishedSprint()
        {
            //arrange
            Mock<Sprint> sprint = new Mock<Sprint>() { CallBase = true };
            sprint.Object.CurrentState = new SprintStateFinished();

            //act
            sprint.Object.Close();

            //assert
            Assert.IsType<SprintStateClosed>(sprint.Object.CurrentState);
            Assert.IsType<SprintStateFinished>(sprint.Object.PreviousState);
            Assert.Equal("This sprint is closed.", sprint.Object.CurrentState.StateDescription);
            Assert.Equal(SprintStateType.Closed, sprint.Object.CurrentState.Type);
            Assert.False(sprint.Object.Editable);
        }

        [Fact]
        public void Should_DoNothingWhenOtherStatesCalled()
        {
            //arrange
            Mock<Sprint> sprint1 = new Mock<Sprint>(new SprintStateCreated()) { CallBase = true };
            Mock<Sprint> sprint2 = new Mock<Sprint>() { CallBase = true };
            sprint2.Object.CurrentState = new SprintStateActive();

            //act
            sprint1.Object.Close();
            sprint1.Object.Cancel();
            sprint1.Object.Finish();
            sprint2.Object.Start();

            //assert
            Assert.IsType<SprintStateCreated>(sprint1.Object.CurrentState);
            Assert.IsType<SprintStateActive>(sprint2.Object.CurrentState);
            Assert.Null(sprint1.Object.PreviousState);
            Assert.IsType<SprintStateCreated>(sprint2.Object.PreviousState);
            Assert.Equal("This sprint is new.", sprint1.Object.CurrentState.StateDescription);
            Assert.Equal("This sprint is active.", sprint2.Object.CurrentState.StateDescription);
            Assert.Equal(SprintStateType.New, sprint1.Object.CurrentState.Type);
            Assert.Equal(SprintStateType.Active, sprint2.Object.CurrentState.Type);
            Assert.True(sprint1.Object.Editable);
            Assert.False(sprint2.Object.Editable);
        }
    }
}
