using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }
        public decimal? Budget { get; set; } // question mark makes it nullable
        public decimal? Revenue { get; set; }
        public string ImdbUrl { get; set; } = null!;
        public string TmdbUrl { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public string BackdropUrl { get; set; } = null!;
        public string OriginalLanguage { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
        public string? CreateBy { get; set; }



    }
}
