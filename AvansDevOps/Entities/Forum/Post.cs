using System;
using System.Collections.Generic;

namespace AvansDevOps
{
    public class Post
    {
        private string title;
        private string content;
        private DateTime dateModified;

        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;
            this.Comments = new List<Comment>();
        }

        public Post(BacklogItem backlogItem, string content, User author) : this()
        {
            this.BacklogItem = backlogItem;
            this.Title = backlogItem.Title;
            this.Content = content;
            this.Author = author;
        }

        public virtual void Add(Comment comment)
        {
            if (!IsClosed)
            {
                this.Comments.Add(comment);
            }
        }

        public string Id { get; private set; }
        public virtual string Title
        {
            get => this.title;
            set
            {
                if (!string.IsNullOrEmpty(value) && !IsClosed)
                {
                    this.DateModified = DateTime.Now;
                    this.title = value;
                }
            }
        }

        public virtual User Author { get; set; }
        public virtual string Content
        {
            get => this.content;
            set
            {
                if (!IsClosed)
                {
                    this.DateModified = DateTime.Now;
                    this.content = value;
                }
            }
        }
        public virtual DateTime DateCreated { get; private set; }
        public virtual DateTime DateModified
        {
            get => this.dateModified;
            set
            {
                if (!IsClosed)
                {
                    this.dateModified = value;
                }
            }
        }
        public virtual bool IsClosed { get { return this.BacklogItem?.State?.Finished == true; } }
        public virtual List<Comment> Comments { get; private set; }
        public BacklogItem BacklogItem { get; private set; }

    }
}
