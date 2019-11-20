using System;

namespace AvansDevOps
{
    public abstract class BacklogComponent
    {
        protected BacklogComponent()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Score = 0;
            this.Priority = 0;
        }

        protected BacklogComponent(BacklogComponentState backlogComponentState) : this()
        {
            this.State = backlogComponentState;
        }

        public virtual void Start()
        {
            this.State.Start(this);
        }

        public virtual void Finish()
        {
            this.State.Finish(this);
        }

        public virtual void Cancel()
        {
            this.State.Cancel(this);
        }

        public virtual void Add(BacklogComponent backlogComponent)
        {
        }

        public virtual void Remove(BacklogComponent backlogComponent)
        {
        }

        public virtual string Id { get; private set; }
        public virtual string Title { get; set; }
        public virtual int Priority { get; set; }
        public virtual string Description { get; set; }
        public virtual User AssignedTo { get; set; }
        public virtual int Score { get; set; }
        public virtual BacklogComponentState State { get; set; }
        public virtual bool Finished { get { return this.State.Finished; } }
        public abstract bool CanFinish { get; }
    }
}
