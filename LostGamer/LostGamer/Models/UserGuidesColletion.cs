using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class UserGuidesColletion
    {
        public int Id { get; set; }
        public int? UserProfilesId { get; set; }
        public int? GameId { get; set; }
        public int? GuideId { get; set; }

        public Games Game { get; set; }
        public Guides Guide { get; set; }
        public UserProfiles UserProfiles { get; set; }
    }
}
