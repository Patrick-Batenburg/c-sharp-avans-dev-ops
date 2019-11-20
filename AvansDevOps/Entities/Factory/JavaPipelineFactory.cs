namespace AvansDevOps
{
    public class JavaPipelineFactory : PipelineFactory
    {
        public override Pipeline CreateBuildPipeline()
        {
            Pipeline pipeline = CreateBasePipeline();
            pipeline.Name = "Java Build Pipeline with Maven";

            return pipeline;
        }

        public override Pipeline CreateReleasePipeline()
        {
            Pipeline pipeline = CreateBasePipeline();
            pipeline.Name = "Java Release Pipeline with Maven";
            pipeline.Add(new PipelinePublishArtifactTask());

            return pipeline;
        }

        private Pipeline CreateBasePipeline()
        {
            Pipeline pipeline = new Pipeline();
            pipeline.Add(new PipelineMavenPackageTask());
            pipeline.Add(new PipelineMavenTestTask());

            return pipeline;
        }
    }
}
