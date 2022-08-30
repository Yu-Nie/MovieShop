using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Favorite
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }

        // navigation
        public Movie Movie { get; set; }
        public Genre User { get; set; }
    }
}
