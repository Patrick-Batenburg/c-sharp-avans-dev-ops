namespace AvansDevOps
{
    public class SprintStateFinished : SprintState
    {
        public SprintStateFinished()
        {
        }

        public override void Start(Sprint sprint)
        {
            sprint.CurrentState = new SprintStateActive();
        }

        public override void Cancel(Sprint sprint)
        {
            sprint.CurrentState = new SprintStateCanceled();
        }
        public override void Close(Sprint sprint)
        {
            sprint.CurrentState = new SprintStateClosed();
        }

        public override SprintStateType Type { get { return SprintStateType.Finished; } }

        public override string StateDescription { get { return "This sprint is finished."; } }
    }
}
