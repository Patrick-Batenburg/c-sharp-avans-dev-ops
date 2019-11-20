using System;
using System.Collections.Generic;

namespace AvansDevOps
{
    public class Sprint : ISubject
    {
        private SprintState currentState;

        public Sprint()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SprintObservers = new List<IObserver>();
            this.BacklogComponents = new List<BacklogComponent>();
            this.Created = DateTime.Now;
            this.Ended = this.Created;
            this.Type = SprintType.Sprint0;

            if (this.CurrentState == null)
            {
                this.CurrentState = new SprintStateCreated();
            }
        }

        public Sprint(SprintState sprintState) : this()
        {
            this.CurrentState = null;
            this.CurrentState = sprintState;
        }

        public Sprint(SprintState sprintState, IExportHandler exportHandler) : this(sprintState)
        {
            this.ExportHandler = exportHandler;
        }

        public virtual void Start()
        {
            this.CurrentState.Start(this);
        }

        public virtual void Finish()
        {
            this.CurrentState.Finish(this);
        }

        public virtual void Cancel()
        {
            this.CurrentState.Cancel(this);
            this.Notify();
        }
        public virtual void Close()
        {
            this.CurrentState.Close(this);
            this.Notify();
        }

        public virtual void Attach(IObserver observer)
        {
            this.SprintObservers.Add(observer);
        }

        public virtual void Detach(IObserver observer)
        {
            this.SprintObservers.Remove(observer);
        }

        public virtual void Notify()
        {
            foreach (IObserver observer in this.SprintObservers)
            {
                observer.Update(this);
            }
        }

        public virtual void GenerateReport()
        {
            this.ExportHandler.Export(this);
        }

        public virtual void Add(BacklogComponent backlogComponent)
        {
            BacklogComponents.Add(backlogComponent);
        }

        public virtual void NextSprint()
        {
            if (this.CurrentState.Type == SprintStateType.Finished)
            {
                switch (this.Type)
                {
                    case SprintType.Sprint0:
                        this.Type = SprintType.DesignSprint;
                        this.CurrentState = new SprintStateCreated();
                        break;
                    case SprintType.DesignSprint:
                        this.Type = SprintType.HardeningSprint;
                        this.CurrentState = new SprintStateCreated();
                        break;
                    case SprintType.HardeningSprint:
                        this.Type = SprintType.Closing;
                        this.CurrentState = new SprintStateCreated();
                        break;
                }
            }
        }

        public virtual void StartRelease()
        {
            if (this.Type == SprintType.Closing && this.DevelopmentPipeline.Run() != 0)
            {
                this.Notify();
            }          
        }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual SprintState CurrentState
        {
            get
            {
                return this.currentState; 
            }
            set
            {
                this.PreviousState = currentState;
                currentState = value;
            }
        }
        public virtual SprintType Type { get; set; }
        public virtual SprintState PreviousState { get; set; }
        public virtual List<BacklogComponent> BacklogComponents { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Ended { get; set; }
        public virtual bool Editable { get { return this.CurrentState.Editable; } }
        public virtual int BacklogItemsToDo { get { return this.BacklogComponents.Count; } }
        private List<IObserver> SprintObservers { get; set; }
        public virtual IExportHandler ExportHandler { get; set; }
        public virtual PipelineBuild DevelopmentPipeline { get; set; }
    }
}
