namespace AvansDevOps
{
    public class SprintStateCanceled : SprintState
    {
        public SprintStateCanceled()
        {
        }

        public override SprintStateType Type { get { return SprintStateType.Canceled; } }
        public override string StateDescription { get { return "This sprint has been canceled.";  } }
    }
}
