namespace AvansDevOps
{
    public class PipelineDotNetBuildTask : IPipelineTask
    {
        public PipelineDotNetBuildTask()
        {
            this.Name = ".NET Build";
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
