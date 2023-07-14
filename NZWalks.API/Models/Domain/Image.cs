using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        [NotMapped]  // excluded from DB mapping
        public IFormFile File { get; set; }

        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }

        public double FileSizeInBytes { get; set; }

        public string FilePath { get; set; }
    }
}
