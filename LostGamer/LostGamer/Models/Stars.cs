using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class Stars
    {
        public Stars()
        {
            Reviews = new HashSet<Reviews>();
        }

        public int Id { get; set; }
        public decimal StarsNum { get; set; }

        public ICollection<Reviews> Reviews { get; set; }
    }
}
