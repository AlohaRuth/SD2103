using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class Games
    {
        public Games()
        {
            Guides = new HashSet<Guides>();
            UserGuidesColletion = new HashSet<UserGuidesColletion>();
        }

        public int Id { get; set; }
        public string GameTitle { get; set; }
        public int? PlatformsId { get; set; }
        public int? CategoryId { get; set; }
        public int? RatingId { get; set; }
        public string Synopsis { get; set; }
        public string CoverLogo { get; set; }
        public int? CompanyId { get; set; }

        public Categories Category { get; set; }
        public Company Company { get; set; }
        public Platforms Platforms { get; set; }
        public Rating Rating { get; set; }
        public ICollection<Guides> Guides { get; set; }
        public ICollection<UserGuidesColletion> UserGuidesColletion { get; set; }
    }
}
