using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class UserReviews
    {
        public int Id { get; set; }
        public int? UserProfilesId { get; set; }
        public int? ReviewId { get; set; }

        public Reviews Review { get; set; }
        public UserProfiles UserProfiles { get; set; }
    }
}
