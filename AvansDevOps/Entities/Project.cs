using System;
using System.Collections.Generic;

namespace AvansDevOps
{
    public class Project
    {
        public Project()
        {
            this.Id = Guid.NewGuid().ToString();
            this.DateCreated = DateTime.Now;
            this.Pipelines = new List<Pipeline>();
            this.Sprints = new List<Sprint>();
            this.Developers = new List<User>();
            this.Repositories = new List<Repository>();
            this.Posts = new List<Post>();
        }

        public Project(IExportHandler exportHandler) : this()
        {
            this.ExportHandler = exportHandler;
        }

        public virtual void Add(User developer)
        {
            this.Developers.Add(developer);
        }

        public virtual void Add(Pipeline pipeline)
        {
            this.Pipelines.Add(pipeline);
        }

        public virtual void Add(Sprint sprint)
        {
            this.Sprints.Add(sprint);
        }

        public virtual void Remove(User developer)
        {
            this.Developers.Remove(developer);
        }

        public virtual void Remove(Pipeline pipeline)
        {
            this.Pipelines.Remove(pipeline);
        }

        public virtual void Remove(Sprint sprint)
        {
            if (sprint.CurrentState.Type == SprintStateType.New)
            {
                this.Sprints.Remove(sprint);
            }
        }

        public virtual void AddBuildPipeline(PipelineFactory factory)
        {
            if (factory != null)
            {
                this.Add(factory.CreateBuildPipeline());
            }
        }

        public virtual void AddReleasePipeline(PipelineFactory factory)
        {
            if (factory != null)
            {
                this.Add(factory.CreateReleasePipeline());
            }
        }

        public virtual void GenerateReport()
        {
            this.ExportHandler.Export(this.Sprints);
        }

        public string Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User ProductOwner { get; set; }
        public User ScrumMaster { get; set; }
        public DateTime DateCreated { get; private set; }
        public IExportHandler ExportHandler { get; set; }
        public List<Pipeline> Pipelines { get; set; }
        public List<Sprint> Sprints { get; set; }
        public List<User> Developers { get; set; }
        public List<Repository> Repositories { get; set; }
        public List<Post> Posts { get; set; }
    }
}
