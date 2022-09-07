﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseDetailsModel
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string PosterUrl { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid PurchaseNumber { get; set; }
        public DateTime PurchasedDate { get; set; }

    }
}
