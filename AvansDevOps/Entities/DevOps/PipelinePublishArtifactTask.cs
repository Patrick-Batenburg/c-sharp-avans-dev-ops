namespace AvansDevOps
{
    public class PipelinePublishArtifactTask : IPipelineTask
    {
        public PipelinePublishArtifactTask()
        {
            this.Name = "Publish Artifact: drop";
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
