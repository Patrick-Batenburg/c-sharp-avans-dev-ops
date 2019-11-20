using System;
using System.Collections.Generic;
using System.Text;

namespace AvansDevOps
{
    public class PipelineBuild
    {
        public PipelineBuild()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tasks = new List<IPipelineTask>();
            this.ExitCode = 0;
            this.Logs = "";
        }

        public virtual int Run()
        {
            StringBuilder stringBuilder = new StringBuilder();
            int exitCode = 0;

            foreach (IPipelineTask task in this.Tasks)
            {
                exitCode = task.Execute();
                stringBuilder.AppendFormat(task.Logs, Environment.NewLine);

                if (exitCode != 0)
                {
                    this.Logs = stringBuilder.ToString();
                    break;
                }
            }

            this.ExitCode = exitCode;
            this.Logs = stringBuilder.ToString();

            return exitCode;
        }

        public virtual string Id { get; private set; }
        public virtual int ExitCode { get; set; }
        public virtual string Logs { get; set; }
        public virtual List<IPipelineTask> Tasks { get; set; }
    }
}
