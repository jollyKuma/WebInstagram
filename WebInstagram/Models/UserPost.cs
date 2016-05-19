using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInstagram.Models
{
    public class UserPost
    {
        public User user { get; set; }
        public Post post { get; set; }
    }
    public class ViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}