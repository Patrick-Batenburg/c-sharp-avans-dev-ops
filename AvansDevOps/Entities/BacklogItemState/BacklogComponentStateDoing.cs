namespace AvansDevOps
{
    public class BacklogComponentStateDoing : BacklogComponentState
    {

        public override void Finish(BacklogComponent backlogComponent)
        {
            if (backlogComponent.Finished)
            {
                backlogComponent.State = new BacklogComponentStateDone();
            }
        }

        public override void Cancel(BacklogComponent backlogComponent)
        {
            backlogComponent.State = new BacklogComponentStateToDo();
        }

        public override bool CanFinish { get { return true; } }
    }
}
