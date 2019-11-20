namespace AvansDevOps
{
    public class PipelineMavenTestTask : IPipelineTask
    {
        public PipelineMavenTestTask()
        {
            this.Name = "Maven Test";
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
