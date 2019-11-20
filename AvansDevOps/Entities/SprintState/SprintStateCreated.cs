namespace AvansDevOps
{
    public class SprintStateCreated : SprintState
    {
        public SprintStateCreated()
        {
        }

        public override void Start(Sprint sprint)
        {
            sprint.CurrentState = new SprintStateActive();
        }

        public override SprintStateType Type { get { return SprintStateType.New; } }
        public override string StateDescription { get { return "This sprint is new.";  } }
        public override bool Editable { get { return true; } }
    }
}
