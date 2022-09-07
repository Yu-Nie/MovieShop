using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseMovieModel
    {
        public int UseId { get; set; }
        public int MovieId { get; set; }
        public Guid PurchaseNumber => Guid.NewGuid();
        public DateTime PurchaseDateTime => DateTime.Now;

    }
}
