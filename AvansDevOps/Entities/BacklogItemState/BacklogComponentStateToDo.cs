namespace AvansDevOps
{
    public class BacklogComponentStateToDo : BacklogComponentState
    {
        public override void Start(BacklogComponent backlogComponent)
        {
            backlogComponent.State = new BacklogComponentStateDoing();
        }
    }
}
