using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class CastDetailsModel
    {
        public CastDetailsModel()
        {
            Movies = new List<MovieDetailsModel>();
            Casts = new List<MovieCastModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePath { get; set; }
        public string Character { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }

        public List<MovieDetailsModel> Movies { get; set; }
        public List<MovieCastModel> Casts { get; set; }
    }
}
