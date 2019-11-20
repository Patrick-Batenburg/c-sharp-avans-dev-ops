namespace AvansDevOps
{
    public interface IExportHandler
    {
        void Export(object data);
        bool HasHeader { get; set; }
        bool HasFooter { get; set; }
    }
}