using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class User
    {

        public User()
        {
            Interests = new List<Interest>();
            UserGroups = new List<UserGroup>();
        }

        public int Id { get; set; }

        public string Username { get; set; }
        public string Fullname { get; set; }

        public string Password { get; set; }

        public List<Interest> Interests { get; set; }
        public List<UserGroup> UserGroups { get; set; }

        public override string ToString()
        {
            return Id + " " + Username + " " + Fullname;
        }
    }
}
