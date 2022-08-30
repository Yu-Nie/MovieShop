using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }

        // navigation
        public Movie Role { get; set; }
        public Genre User { get; set; }
    }
}
