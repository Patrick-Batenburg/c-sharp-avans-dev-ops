namespace AvansDevOps
{
    public class PipelineDotNetPublishTask : IPipelineTask
    {
        public PipelineDotNetPublishTask()
        {
            this.Name = ".NET Publish";
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
