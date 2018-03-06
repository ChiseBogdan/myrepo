using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Location
    {
        public Location()
        {
            Activities = new List<Activity>();
        }

        public int Id { get; set; }

        public String City { get; set; }

        public String Adress { get; set; }

        public List<Activity> Activities { get; set; }


      
    }
}
