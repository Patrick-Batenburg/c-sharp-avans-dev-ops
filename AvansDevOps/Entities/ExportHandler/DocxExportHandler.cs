using System;

namespace AvansDevOps
{
    public class DocxExportHandler : IExportHandler
    {
        public void Export(object data)
        {
            if (HasHeader)
            {
                Console.WriteLine("adding header");
            }

            if (HasFooter)
            {
                Console.WriteLine("adding footer");
            }

            Console.WriteLine("Exporting");
        }

        public bool HasHeader { get; set; }
        public bool HasFooter { get; set; }
    }
}
