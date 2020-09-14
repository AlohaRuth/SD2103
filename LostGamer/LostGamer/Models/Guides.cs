using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class Guides
    {
        public Guides()
        {
            UserGuidesColletion = new HashSet<UserGuidesColletion>();
        }

        public int Id { get; set; }
        public string GuideTitle { get; set; }
        public string GuideContent { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? UserProfilesId { get; set; }
        public int? GameId { get; set; }

        public Games Game { get; set; }
        public UserProfiles UserProfiles { get; set; }
        public ICollection<UserGuidesColletion> UserGuidesColletion { get; set; }
    }
}
