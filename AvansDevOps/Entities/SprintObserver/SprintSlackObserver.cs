namespace AvansDevOps
{
    public class SprintSlackObserver : IObserver
    {
        public SprintSlackObserver(NotificationHandler notificationHandler)
        {
            this.NotificationHandler = notificationHandler;
        }

        public virtual void Update(ISubject subject)
        {
            if (subject is Sprint sprint && sprint.CurrentState.Type == SprintStateType.Finished)
            {
                this.NotificationHandler.SendMessage("");
            }
        }

        private NotificationHandler NotificationHandler { get; set; }
    }
}
