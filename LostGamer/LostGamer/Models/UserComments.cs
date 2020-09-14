using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class UserComments
    {
        public int Id { get; set; }
        public int? UserProfilesId { get; set; }
        public int? CommentId { get; set; }

        public Comments Comment { get; set; }
        public UserProfiles UserProfiles { get; set; }
    }
}
