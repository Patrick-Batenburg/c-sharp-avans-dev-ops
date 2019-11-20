namespace AvansDevOps
{
    public abstract class PipelineFactory
    {
        public abstract Pipeline CreateBuildPipeline();
        public abstract Pipeline CreateReleasePipeline();
    }
}