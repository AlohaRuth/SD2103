using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class Platforms
    {
        public Platforms()
        {
            Games = new HashSet<Games>();
        }

        public int Id { get; set; }
        public string PlatformName { get; set; }

        public ICollection<Games> Games { get; set; }
    }
}
