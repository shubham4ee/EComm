using System.Text.Json.Serialization;

namespace UserServer.DAL.Models
{
    public class CADFile : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Guid UploadedBy { get; set; }
        public string ConversionStatus { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public Project Project { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public ModelMetadata Metadata { get; set; }
    }
}
