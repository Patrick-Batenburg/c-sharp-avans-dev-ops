using AvansDevOps;
using Moq;
using System;
using Xunit;

namespace AvansDevOpsTests
{
    public class PostTests
    {
        [Fact]
        public void Should_AddCommentToPost()
        {
            //arrange
            BacklogItem backlogItem = new BacklogItem()
            {
                Title = "new featue",
                State = new BacklogComponentStateDoing()             
            };
            string content = "some content";
            User user1 = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg",
                Email = "example@mail.com"
            };
            User user2 = new User()
            {
                FirstName = "Patrick2",
                MiddleName = "van",
                LastName = "Batenburg"
            };
            Comment comment = new Comment()
            {
                Author = user2,
                Content = "test"
            };
            Mock<Post> mock = new Mock<Post>(backlogItem, content, user1) { CallBase = true };

            //act
            mock.Object.Add(comment);

            //assert
            mock.Verify(x => x.Add(It.IsAny<Comment>()), Times.Exactly(1));
            Assert.Single(mock.Object.Comments);
            Assert.Equal(user1, mock.Object.Author);
            Assert.Equal(user1.Email, mock.Object.Author.Email);
            Assert.Equal(user1.FullName, mock.Object.Author.FullName);
            Assert.Equal(user2, mock.Object.Comments[0].Author);
            Assert.Equal(backlogItem.Title, mock.Object.Title);
            Assert.Equal(content, mock.Object.Content);
        }

        [Fact]
        public void ShouldNot_AddCommentToPost()
        {
            //arrange
            BacklogItem backlogItem = new BacklogItem()
            {
                Title = "new featue",
                State = new BacklogComponentStateDone()
            };
            string content = "some content";
            User user1 = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg",
                Email = "example@mail.com"
            };
            User user2 = new User()
            {
                FirstName = "Patrick2",
                MiddleName = "van",
                LastName = "Batenburg"
            };
            Comment comment = new Comment()
            {
                Author = user2,
                Content = "test"
            };
            Mock<Post> mock = new Mock<Post>(backlogItem, content, user1) { CallBase = true };

            //act
            mock.Object.Add(comment);

            //assert
            mock.Verify(x => x.Add(It.IsAny<Comment>()), Times.Exactly(1));
            Assert.Equal(user1, mock.Object.Author);
            Assert.Equal(user1.Email, mock.Object.Author.Email);
            Assert.Equal(user1.FullName, mock.Object.Author.FullName);
            Assert.Empty(mock.Object.Comments);
            Assert.Null(mock.Object.Title);
            Assert.Null(mock.Object.Content);
        }

        [Fact]
        public void Should_UpdateDateModifiedOnContentUpdate()
        {
            //arrange
            string content = "some content";
            string newContent = "some new content";
            Mock<Post> mock = new Mock<Post>() { CallBase = true };
            mock.SetupSet(x => x.Content = content);
            DateTime dateModified = mock.Object.DateModified;

            User user = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg"
            };

            //act
            mock.Object.Content = newContent;

            //assert
            Assert.Equal(newContent, mock.Object.Content);
            Assert.NotEqual(dateModified, mock.Object.DateModified);
        }

        [Fact]
        public void Should_UpdateDateModifiedOnTitleUpdate()
        {
            //arrange
            string title = "A post";
            string newTitle = "a new title";
            Mock<Post> mock = new Mock<Post>() { CallBase = true };
            mock.SetupSet(x => x.Title = title);
            DateTime dateModified = mock.Object.DateModified;

            User user = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg"
            };

            //act
            mock.Object.Title = newTitle;

            //assert
            Assert.Equal(newTitle, mock.Object.Title);
            Assert.NotEqual(dateModified, mock.Object.DateModified);
        }

        [Fact]
        public void Should_AddCommentToComment()
        {
            //arrange
            Mock<Comment> mock = new Mock<Comment>() { CallBase = true };

            User user = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg"
            };

            Comment comment = new Comment()
            {
                Author = user,
                Content = "test"
            };

            //act
            mock.Object.Add(comment);

            //assert
            mock.Verify(x => x.Add(It.IsAny<Comment>()), Times.Exactly(1));
            Assert.Single(mock.Object.Comments);
        }
    }
}
