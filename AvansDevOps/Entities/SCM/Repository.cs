using System;
using System.Collections.Generic;

namespace AvansDevOps
{
    public class Repository
    {
        public Repository()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Branches = new List<RepositoryBranch>();
        }

        public string Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebsiteUrl { get; set; }
        public RepositoryType Type { get; set; }
        public List<RepositoryBranch> Branches { get; set; }
        public RepositoryBranch DefaultBranch { get; set; }
    }
}
