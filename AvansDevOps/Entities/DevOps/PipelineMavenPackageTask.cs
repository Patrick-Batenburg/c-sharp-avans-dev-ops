namespace AvansDevOps
{
    public class PipelineMavenPackageTask : IPipelineTask
    {
        public PipelineMavenPackageTask()
        {
            this.Name = "Maven Package";
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
