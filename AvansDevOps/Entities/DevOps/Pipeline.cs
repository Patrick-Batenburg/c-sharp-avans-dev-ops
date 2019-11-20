using System;
using System.Collections.Generic;
using System.Linq;

namespace AvansDevOps
{
    public class Pipeline
    {
        public Pipeline()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tasks = new List<IPipelineTask>();
            this.Builds = new List<PipelineBuild>();
        }

        public virtual void Add(IPipelineTask task)
        {
            this.Tasks.Add(task);
        }

        public virtual void AddBefore(IPipelineTask newTask, IPipelineTask before)
        {
            int index = this.Tasks.IndexOf(before);

            if (index >= 0)
            {
                this.Tasks.Insert(index, newTask);
            }
        }

        public virtual void AddAfter(IPipelineTask newTask, IPipelineTask after)
        {
            int index = this.Tasks.IndexOf(after);

            if (index >= 0)
            {
                this.Tasks.Insert(index + 1, newTask);
            }
        }

        public virtual void Remove(IPipelineTask task)
        {
            this.Tasks.Remove(task);
        }

        public virtual string Id { get; private set; }
        public virtual string Name { get; set; }
        public virtual Repository Repository { get; set; }
        public virtual RepositoryBranch Branch { get; set; }
        public virtual List<IPipelineTask> Tasks { get; set; }
        public virtual List<PipelineBuild> Builds { get; set; }
    }
}
