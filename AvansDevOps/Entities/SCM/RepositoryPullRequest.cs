using System;

namespace AvansDevOps
{
    public class RepositoryPullRequest
    {
        public RepositoryPullRequest()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; private set; }
        public RepositoryBranch SourceBranch { get; set; }
        public RepositoryBranch DestinationBranch { get; set; }
    }
}
