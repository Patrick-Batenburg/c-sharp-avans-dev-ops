using AvansDevOps;
using Moq;
using System;
using System.Text;
using Xunit;

namespace AvansDevOpsTests
{
    public class RepositoryTests
    {
        [Fact]
        public void Should_CreateRepository()
        {
            //arrange
            string name = "a name";
            string description = "some content";
            string websiteUrl = "https://example.com/";
            RepositoryBranch branch = new RepositoryBranch()
            {
                Name = "master"
            };
            Mock<Repository> repository = new Mock<Repository>() { CallBase = true };

            //act
            repository.Object.Name = name;
            repository.Object.Description = description;
            repository.Object.WebsiteUrl = websiteUrl;
            repository.Object.DefaultBranch = branch;
            repository.Object.Type = RepositoryType.Git;

            //assert
            Assert.Equal(name, repository.Object.Name);
            Assert.Equal(description, repository.Object.Description);
            Assert.Equal(websiteUrl, repository.Object.WebsiteUrl);
            Assert.Equal(branch, repository.Object.DefaultBranch);
            Assert.Equal(RepositoryType.Git, repository.Object.Type);
        }

        [Fact]
        public void Should_CreateRepositoryCommit()
        {
            //arrange
            string name = "a name";
            string description = "some content";
            string websiteUrl = "https://example.com/";
            User user1 = new User()
            {
                FirstName = "Patrick",
                MiddleName = "van",
                LastName = "Batenburg",
                Email = "example@mail.com"
            };
            RepositoryBranch branch = new RepositoryBranch()
            {
                Name = "master"
            };
            RepositoryCommit commit = new RepositoryCommit()
            {
                Author = user1,
                Message = "init commit",
                Branch = branch
            };
            commit.Size = Encoding.ASCII.GetBytes(commit.Message).ToString();

            Mock<Repository> repository = new Mock<Repository>() { CallBase = true };

            //act
            branch.Add(commit);

            repository.Object.Name = name;
            repository.Object.Description = description;
            repository.Object.WebsiteUrl = websiteUrl;
            repository.Object.DefaultBranch = branch;
            repository.Object.Type = RepositoryType.SVN;

            //assert
            Assert.Equal(name, repository.Object.Name);
            Assert.Equal(description, repository.Object.Description);
            Assert.Equal(websiteUrl, repository.Object.WebsiteUrl);
            Assert.Equal(branch, repository.Object.DefaultBranch);
            Assert.Equal(commit, branch.Commits[0]);
            Assert.Single(branch.Commits);
            Assert.Equal(RepositoryType.SVN, repository.Object.Type);
        }

        [Fact]
        public void Should_CreateRepositoryPullRequest()
        {
            //arrange
            RepositoryBranch master = new RepositoryBranch()
            {
                Name = "master"
            };
            RepositoryBranch develop = new RepositoryBranch()
            {
                Name = "develop"
            };

            Mock<RepositoryPullRequest> repositoryPullRequest = new Mock<RepositoryPullRequest>() { CallBase = true };

            //act
            repositoryPullRequest.Object.SourceBranch = develop;
            repositoryPullRequest.Object.DestinationBranch = master;

            //assert
            Assert.Equal(develop, repositoryPullRequest.Object.SourceBranch);
            Assert.Equal(master, repositoryPullRequest.Object.DestinationBranch);
        }
    }
}
