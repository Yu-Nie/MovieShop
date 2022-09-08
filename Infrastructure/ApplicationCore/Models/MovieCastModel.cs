using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MovieCastModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string Character { get; set; }
        public ICollection<MovieCast>? movieCast { get; set; }
    }

}
