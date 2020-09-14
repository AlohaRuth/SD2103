using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class Rating
    {
        public Rating()
        {
            Games = new HashSet<Games>();
        }

        public int Id { get; set; }
        public string RatingNum { get; set; }

        public ICollection<Games> Games { get; set; }
    }
}
