﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ReviewMovieModel
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate => DateTime.Now;
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }
    }
}
