namespace AvansDevOps
{
    public class SprintStateClosed : SprintState
    {
        public SprintStateClosed()
        {
        }

        public override SprintStateType Type { get { return SprintStateType.Closed; } }
        public override string StateDescription { get { return "This sprint is closed."; } }
    }
}
