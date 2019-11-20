using AvansDevOps;
using Moq;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace AvansDevOpsTests
{
    public class ProjectTests
    {
        [Fact]
        public void Should_CreateProject()
        {
            User user1 = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg"
            };
            User user2 = new User()
            {
                FirstName = "Patrick2",
                MiddleName = "van",
                LastName = "Batenburg"
            };
            string name = "A name";
            string description = "A description";

            //arrange
            Mock<Project> mock = new Mock<Project>() { CallBase = true };

            //act
            mock.Object.ProductOwner = user1;
            mock.Object.ScrumMaster = user2;
            mock.Object.Name = name;
            mock.Object.Description = description;

            //assert
            Assert.Equal(user1, mock.Object.ProductOwner);
            Assert.Equal(user2, mock.Object.ScrumMaster);
            Assert.Equal(name, mock.Object.Name);
            Assert.Equal(description, mock.Object.Description);
        }

        [Fact]
        public void Should_AddDeveloper()
        {
            //arrange
            User user1 = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg"
            };
            User user2 = new User()
            {
                FirstName = "Patrick2",
                MiddleName = "van",
                LastName = "Batenburg"
            };
            Mock<Project> mock = new Mock<Project>() { CallBase = true };

            //act
            mock.Object.Add(user1);
            mock.Object.Add(user2);

            //assert
            mock.Verify(x => x.Add(It.IsAny<User>()), Times.Exactly(2));
            Assert.Equal(user1, mock.Object.Developers[0]);
            Assert.Equal(user2, mock.Object.Developers[1]);
            Assert.Equal(2, mock.Object.Developers.Count);
        }

        [Fact]
        public void Should_RemoveDeveloper()
        {
            //arrange
            User user1 = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg"
            };
            User user2 = new User()
            {
                FirstName = "Patrick2",
                MiddleName = "van",
                LastName = "Batenburg"
            };
            User user3 = new User()
            {
                FirstName = "Patrick3",
                MiddleName = "van",
                LastName = "Batenburg"
            };
            Mock<Project> mock = new Mock<Project>() { CallBase = true };

            //act
            mock.Object.Add(user1);
            mock.Object.Add(user2);
            mock.Object.Add(user3);
            mock.Object.Remove(user1);

            //assert
            mock.Verify(x => x.Add(It.IsAny<User>()), Times.Exactly(3));
            mock.Verify(x => x.Remove(It.IsAny<User>()), Times.Exactly(1));
            Assert.Equal(user2, mock.Object.Developers[0]);
            Assert.Equal(user3, mock.Object.Developers[1]);
            Assert.Equal(2, mock.Object.Developers.Count);
        }

        [Fact]
        public void Should_AddSprint()
        {
            //arrange
            Sprint sprint1 = new Sprint();
            Sprint sprint2 = new Sprint();
            Sprint sprint3 = new Sprint();
            Mock<Project> mock = new Mock<Project>() { CallBase = true };

            //act
            mock.Object.Add(sprint1);
            mock.Object.Add(sprint2);
            mock.Object.Add(sprint3);

            //assert
            mock.Verify(x => x.Add(It.IsAny<Sprint>()), Times.Exactly(3));
            Assert.Equal(3, mock.Object.Sprints.Count);
            Assert.Equal(sprint1, mock.Object.Sprints[0]);
            Assert.Equal(sprint2, mock.Object.Sprints[1]);
            Assert.Equal(sprint3, mock.Object.Sprints[2]);
        }

        [Fact]
        public void Should_RemoveCreatedSprint()
        {
            //arrange
            Sprint sprint1 = new Sprint(new SprintStateCreated());
            Sprint sprint2 = new Sprint(new SprintStateActive());
            Sprint sprint3 = new Sprint(new SprintStateCreated());
            Mock<Project> mock = new Mock<Project>() { CallBase = true };

            //act
            mock.Object.Add(sprint1);
            mock.Object.Add(sprint2);
            mock.Object.Add(sprint3);
            mock.Object.Remove(sprint1);
            mock.Object.Remove(sprint2);
            mock.Object.Remove(sprint3);

            //assert
            mock.Verify(x => x.Add(It.IsAny<Sprint>()), Times.Exactly(3));
            mock.Verify(x => x.Remove(It.IsAny<Sprint>()), Times.Exactly(3));
            Assert.Equal(sprint2, mock.Object.Sprints[0]);
            Assert.Single(mock.Object.Sprints);
        }

        [Fact]
        public void Should_RemovePipeline()
        {
            //arrange
            Pipeline pipeline1 = new Pipeline()
            {
                Name = "new pipeline1"
            };
            Pipeline pipeline2 = new Pipeline()
            {
                Name = "new pipeline2"
            };
            Pipeline pipeline3 = new Pipeline()
            {
                Name = "new pipeline3"
            };
            Mock<Project> mock = new Mock<Project>() { CallBase = true };

            //act
            mock.Object.Add(pipeline1);
            mock.Object.Add(pipeline2);
            mock.Object.Add(pipeline3);
            mock.Object.Remove(pipeline1);

            //assert
            mock.Verify(x => x.Add(It.IsAny<Pipeline>()), Times.Exactly(3));
            mock.Verify(x => x.Remove(It.IsAny<Pipeline>()), Times.Exactly(1));
            Assert.Equal(pipeline2, mock.Object.Pipelines[0]);
            Assert.Equal(pipeline3, mock.Object.Pipelines[1]);
            Assert.Equal(2, mock.Object.Pipelines.Count);
        }

        [Fact]
        public void Should_AddCsharpReleasePipeline_WithAFactory()
        {
            //arrange
            Mock<Project> mock = new Mock<Project>() { CallBase = true };
            PipelineFactory factory = new CsharpPipelineFactory();

            //act
            mock.Object.AddReleasePipeline(factory);

            //assert
            mock.Verify(x => x.AddReleasePipeline(It.IsAny<PipelineFactory>()), Times.Exactly(1));
            mock.Verify(x => x.Add(It.IsAny<Pipeline>()), Times.Exactly(1));
            Assert.Single(mock.Object.Pipelines);
        }

        [Fact]
        public void Should_AddCsharpBuildPipeline_WithAFactory()
        {
            //arrange
            Mock<Project> mock = new Mock<Project>() { CallBase = true };
            PipelineFactory factory = new CsharpPipelineFactory();

            //act
            mock.Object.AddBuildPipeline(factory);

            //assert
            mock.Verify(x => x.AddBuildPipeline(It.IsAny<PipelineFactory>()), Times.Exactly(1));
            mock.Verify(x => x.Add(It.IsAny<Pipeline>()), Times.Exactly(1));
            Assert.Single(mock.Object.Pipelines);
        }

        [Fact]
        public void Should_AddJavaReleasePipeline_WithAFactory()
        {
            //arrange
            Mock<Project> mock = new Mock<Project>() { CallBase = true };
            PipelineFactory factory = new JavaPipelineFactory();

            //act
            mock.Object.AddReleasePipeline(factory);

            //assert
            mock.Verify(x => x.AddReleasePipeline(It.IsAny<PipelineFactory>()), Times.Exactly(1));
            mock.Verify(x => x.Add(It.IsAny<Pipeline>()), Times.Exactly(1));
            Assert.Single(mock.Object.Pipelines);
        }

        [Fact]
        public void Should_AddJavaBuildPipeline_WithAFactory()
        {
            //arrange
            Mock<Project> mock = new Mock<Project>() { CallBase = true };
            PipelineFactory factory = new JavaPipelineFactory();

            //act
            mock.Object.AddBuildPipeline(factory);

            //assert
            mock.Verify(x => x.AddBuildPipeline(It.IsAny<PipelineFactory>()), Times.Exactly(1));
            mock.Verify(x => x.Add(It.IsAny<Pipeline>()), Times.Exactly(1));
            Assert.Single(mock.Object.Pipelines);
        }

        /*
        [Fact]
        public void Should_Export_Burndown()
        {
            // Arrange
            Mock<IExportHandler> export = new Mock<IExportHandler>();

            Sprint sprint = new Sprint();
            Report report = new BurndownchartReport
            {
                ExportFormat = export.Object
            };
            // Act
            sprint.Report = report;

            // Assert
            Assert.Equal(sprint.Report.Data, new UTF8Encoding(true).GetBytes("This is a Burndownchart Report"));
        }*/
    }
}
