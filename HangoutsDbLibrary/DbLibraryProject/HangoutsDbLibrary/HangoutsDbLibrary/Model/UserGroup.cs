using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class UserGroup
    {

        public int UserId { get; set; }
        public User User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public override string ToString()
        {
            return UserId + " " + GroupId;
        }

    }
}
