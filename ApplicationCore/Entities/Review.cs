﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Review
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public decimal Rating { get; set; }
        public string ReviewText { get; set; }

        // navigation
        public Movie Movie { get; set; }
        public Genre User { get; set; }
    }
}
