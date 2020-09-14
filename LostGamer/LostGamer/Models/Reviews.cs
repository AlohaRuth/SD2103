using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class Reviews
    {
        public Reviews()
        {
            UserReviews = new HashSet<UserReviews>();
        }

        public int Id { get; set; }
        public int? StarsId { get; set; }
        public string Review { get; set; }
        public DateTime? DateMade { get; set; }

        public Stars Stars { get; set; }
        public ICollection<UserReviews> UserReviews { get; set; }
    }
}
