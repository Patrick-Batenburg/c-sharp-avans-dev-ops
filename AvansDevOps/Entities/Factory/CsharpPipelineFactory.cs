namespace AvansDevOps
{
    public class CsharpPipelineFactory : PipelineFactory
    {
        public override Pipeline CreateBuildPipeline()
        {
            Pipeline pipeline = CreateBasePipeline();
            pipeline.Name = ".NET Core Build Pipeline";

            return pipeline;
        }

        public override Pipeline CreateReleasePipeline()
        {
            Pipeline pipeline = CreateBasePipeline();
            pipeline.Name = ".NET Core Release Pipeline";
            pipeline.Add(new PipelineDotNetPublishTask());
            pipeline.Add(new PipelinePublishArtifactTask());

            return pipeline;
        }

        private Pipeline CreateBasePipeline()
        {
            Pipeline pipeline = new Pipeline();
            pipeline.Add(new PipelineDotNetRestoreTask());
            pipeline.Add(new PipelineDotNetBuildTask());
            pipeline.Add(new PipelineDotNetTestTask());

            return pipeline;
        }
    }
}
