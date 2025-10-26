using System.Text.Json.Serialization;

namespace UserServer.DAL.Models
{
    public class ModelMetadata : BaseEntity
    {
        public Guid CADFileId { get; set; }
        public string Metadata{ get; set; }

        // Navigation Properties
        
        public CADFile CADFile { get; set; }
    }
}
