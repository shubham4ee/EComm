using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserServer.DAL.Models
{
    public class UserProjects 
    {
        public Guid UserId { get; set; }
        
        public Guid ProjectId { get; set; }

        // navigation prop
        public User User { get; set; }
        public Project Project { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

    }
}
