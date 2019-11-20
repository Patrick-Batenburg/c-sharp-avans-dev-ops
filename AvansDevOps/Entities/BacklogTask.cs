namespace AvansDevOps
{
    public class BacklogTask : BacklogComponent
    {
        public BacklogTask() : base()
        {
        }

        public BacklogTask(BacklogComponentState backlogComponentState) : base(backlogComponentState)
        {
        }

        public override bool CanFinish { get { return this.State.CanFinish; } }
    }
}
