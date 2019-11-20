using System.Collections.Generic;
using System.Linq;

namespace AvansDevOps
{
    public class BacklogItem : BacklogComponent
    {
        public BacklogItem() : base()
        {
            this.BacklogItems = new List<BacklogComponent>();
            this.Type = BacklogItemType.Task;
        }

        public BacklogItem(BacklogComponentState backlogComponentState) : base(backlogComponentState)
        {
            this.BacklogItems = new List<BacklogComponent>();
            this.Type = BacklogItemType.Task;
        }

        public virtual void AddBefore(BacklogComponent newBacklogItem, BacklogComponent before)
        {
            int index = this.BacklogItems.IndexOf(before);

            if (index >= 0)
            {
                this.BacklogItems.Insert(index, newBacklogItem);
            }
        }

        public virtual void AddAfter(BacklogComponent newBacklogItem, BacklogComponent after)
        {
            int index = this.BacklogItems.IndexOf(after);

            if (index >= 0)
            {
                this.BacklogItems.Insert(index + 1, newBacklogItem);
            }
        }

        public override void Add(BacklogComponent backlogComponent)
        {
            BacklogItems.Add(backlogComponent);
        }

        public override void Remove(BacklogComponent backlogComponent)
        {
            BacklogItems.Remove(backlogComponent);
        }

        public override void Start()
        {
            foreach (BacklogComponent backlogComponent in this.BacklogItems)
            {
                backlogComponent.State.Start(backlogComponent);
            }

            this.State.Start(this);
        }

        public override void Finish()
        {
            foreach (BacklogComponent backlogComponent in this.BacklogItems)
            {
                backlogComponent.State.Finish(backlogComponent);
            }

            this.State.Finish(this);
        }

        public override void Cancel()
        {
            foreach (BacklogComponent backlogComponent in this.BacklogItems)
            {
                backlogComponent.State.Cancel(backlogComponent);
            }

            this.State.Cancel(this);
        }

        public override bool CanFinish
        {
            get
            {
                return !this.BacklogItems.Any(x => !x.Finished);
            }
        }

        public virtual List<BacklogComponent> BacklogItems { get; set; }
        public virtual BacklogItemType Type { get; set; }
    }
}
