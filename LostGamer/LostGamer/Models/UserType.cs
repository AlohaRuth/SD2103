using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class UserType
    {
        public UserType()
        {
            UserProfiles = new HashSet<UserProfiles>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }

        public ICollection<UserProfiles> UserProfiles { get; set; }
    }
}
