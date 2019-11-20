using System;

namespace AvansDevOps
{
    public class RepositoryCommit
    {
        public RepositoryCommit()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateCreated = DateTime.Now;
        }

        public string Id { get; private set; }
        public string Message { get; set; }
        public User Author { get; set; }
        public string Size { get; set; }
        public DateTime DateCreated { get; private set; }
        public RepositoryBranch Branch { get; set; }
    }
}
