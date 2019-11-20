namespace AvansDevOps
{
    public abstract class BacklogComponentState
    {
        public virtual void Start(BacklogComponent backlogComponent)
        {
        }

        public virtual void Finish(BacklogComponent backlogComponent)
        {
        }

        public virtual void Cancel(BacklogComponent backlogComponent)
        {
        }

        public virtual bool Finished { get; set; }
        public virtual bool CanFinish { get; set; }
    }
}
