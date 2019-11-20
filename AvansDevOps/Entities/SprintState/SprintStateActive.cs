namespace AvansDevOps
{
    public class SprintStateActive : SprintState
    {
        public SprintStateActive()
        {
        }

        public override void Finish(Sprint sprint)
        {
            sprint.CurrentState = new SprintStateFinished();
        }

        public override void Cancel(Sprint sprint)
        {
            sprint.CurrentState = new SprintStateCanceled();
        }

        public override SprintStateType Type { get { return SprintStateType.Active; } }

        public override string StateDescription { get { return "This sprint is active."; } }
    }
}