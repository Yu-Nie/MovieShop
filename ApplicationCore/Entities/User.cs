using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(128)]
        public string FirstName { get; set; }

        [MaxLength(128)]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(1024)]
        public string HashedPassword { get; set; }

        [MaxLength(16)]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(1024)]
        public string Salt { get; set; }

        public bool IsLocked { get; set; }
        public string ProfilePictureUrl { get; set; } = null!;


    }
}
