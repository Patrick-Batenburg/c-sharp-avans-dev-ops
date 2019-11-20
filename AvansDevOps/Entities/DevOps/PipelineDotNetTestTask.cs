namespace AvansDevOps
{
    public class PipelineDotNetTestTask : IPipelineTask
    {
        public PipelineDotNetTestTask()
        {
            this.Name = ".NET Test";
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
