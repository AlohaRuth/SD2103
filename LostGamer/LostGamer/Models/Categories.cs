using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Games = new HashSet<Games>();
        }

        public int Id { get; set; }
        public string Category { get; set; }

        public ICollection<Games> Games { get; set; }
    }
}
