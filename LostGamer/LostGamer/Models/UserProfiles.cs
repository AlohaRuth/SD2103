using System;
using System.Collections.Generic;

namespace LostGamer.Models
{
    public partial class UserProfiles
    {
        public UserProfiles()
        {
            Comments = new HashSet<Comments>();
            Guides = new HashSet<Guides>();
            UserComments = new HashSet<UserComments>();
            UserGuidesColletion = new HashSet<UserGuidesColletion>();
            UserReviews = new HashSet<UserReviews>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? UserTypeId { get; set; }
        public string UserAccountId { get; set; }

        public UserType UserType { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Guides> Guides { get; set; }
        public ICollection<UserComments> UserComments { get; set; }
        public ICollection<UserGuidesColletion> UserGuidesColletion { get; set; }
        public ICollection<UserReviews> UserReviews { get; set; }
    }
}
