using System;

namespace AvansDevOps
{
    public abstract class SprintState
    {
        protected SprintState()
        {
        }

        public virtual void Start(Sprint sprint)
        {
        }

        public virtual void Finish(Sprint sprint)
        {
        }

        public virtual void Cancel(Sprint sprint)
        {
        }
        public virtual void Close(Sprint sprint)
        {
        }

        public abstract SprintStateType Type { get; }
        public abstract string StateDescription { get; }
        public virtual bool Editable { get; }
    }
}
