using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Activity
    {

        public Activity()
        {
            GroupActivity = new List<GroupActivity>();
        }
        public int Id { get; set; }
        public String Description { get; set; }
        public String BeginTimeActivity { get; set; }
        public String EndTimeActivity { get; set; }

        public List<GroupActivity> GroupActivity { get; set; }

        public Location Location { get; set; }

    }
}
