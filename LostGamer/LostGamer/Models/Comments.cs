using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class Comments
    {
        public Comments()
        {
            UserComments = new HashSet<UserComments>();
        }

        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime? DatePosted { get; set; }
        public int? UserProfilesId { get; set; }

        public UserProfiles UserProfiles { get; set; }
        public ICollection<UserComments> UserComments { get; set; }
    }
}
