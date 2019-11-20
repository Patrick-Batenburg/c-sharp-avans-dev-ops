using System;
using System.Collections.Generic;

namespace AvansDevOps
{
    public class RepositoryBranch
    {
        public RepositoryBranch()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Commits = new List<RepositoryCommit>();
        }

        public virtual void Add(RepositoryCommit commit)
        {
            this.Commits.Add(commit);
        }

        public string Id { get; private set; }
        public string Name { get; set; }
        public List<RepositoryCommit> Commits { get; set; }
    }
}
