using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Group
    {
        public Group()
        {
            UserGroups = new List<UserGroup>();
            Plans = new List<Plan>();
            GroupActivity = new List<GroupActivity>();

        }
        public int Id { get; set; }

        public string Name { get; set; }

        public Chat Chat { get; set; }

        public GroupAdmin Admin { get; set; }
    
        public List<UserGroup> UserGroups { get; set; }

        public List<GroupActivity> GroupActivity { get; set; }

        public List<Plan> Plans { get; set; }

        public override string ToString()
        {
            return Id + " " + Name;
        }




    }
}
