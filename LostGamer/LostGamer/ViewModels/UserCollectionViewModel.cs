using LostGamer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostGamer.ViewModels
{
    public class UserCollectionViewModel
    {
        public IEnumerable<Comments> comments;
        public IEnumerable<Guides> guides;
        public IEnumerable<UserProfiles> userProfiles;
        public IEnumerable<Games> games;
        public IEnumerable<UserGuidesColletion> userGuidesColletions;
        public IEnumerable<UserComments> userComments;
        public IEnumerable<UserReviews> userReviews;
    }
}
