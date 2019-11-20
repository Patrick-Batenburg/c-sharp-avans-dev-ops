using System;

namespace AvansDevOps
{
    public interface IPipelineTask
    {
        int Execute();
        string Name { get; set; }
        string Logs { get; set; }
    }
}
