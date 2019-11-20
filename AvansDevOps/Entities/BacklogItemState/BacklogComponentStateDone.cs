namespace AvansDevOps
{
    public class BacklogComponentStateDone : BacklogComponentState
    {
        public override void Cancel(BacklogComponent backlogComponent)
        {
            backlogComponent.State = new BacklogComponentStateToDo();
        }

        public override bool Finished { get { return true; } }
    }
}
