namespace AvansDevOps
{
    public class PipelineDotNetRestoreTask : IPipelineTask
    {
        public PipelineDotNetRestoreTask()
        {
            this.Name = ".NET Restore";
        }

        public virtual int Execute()
        {
            this.Logs = "Success!";
            return 0;
        }

        public virtual string Logs { get; set; }
        public virtual string Name { get; set; }
    }
}
