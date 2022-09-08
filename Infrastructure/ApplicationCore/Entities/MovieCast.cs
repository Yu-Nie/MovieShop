using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class MovieCast
    {
        public int MovieId { get; set; }
        public int CastId { get; set; }

        [MaxLength(450)]
        public string Character { get; set; }

        // navigation
        public Movie Movie { get; set; }
        public Cast Cast { get; set; }
    }
}
