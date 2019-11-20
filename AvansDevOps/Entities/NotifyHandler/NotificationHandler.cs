using System;

namespace AvansDevOps
{
    public abstract class NotificationHandler : INotificationHandler
    {
        public virtual void SendMessage(string message)
        {
            this.Message = message;
            this.CollectReceiverData();
            this.ComposeMessageContent();
            this.ComposeMessage();
            this.DeliverMessage();
        }

        protected virtual void CollectReceiverData()
        {
            //Empty
        }

        protected abstract void ComposeMessageContent();

        protected abstract void DeliverMessage();

        protected virtual void ComposeMessage()
        {
        }

        protected string Message { get; set; }
    }
}
