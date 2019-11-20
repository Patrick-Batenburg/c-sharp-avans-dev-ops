using System;
using System.Collections.Generic;

namespace AvansDevOps
{
    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
            this.Comments = new List<Comment>();
        }

        public virtual void Add(Comment comment)
        {
            this.Comments.Add(comment);
        }

        public string Id { get; private set; }
        public User Author { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get;  set; }
        public List<Comment> Comments { get; private set; }
    }
}