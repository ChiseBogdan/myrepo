using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Plan
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public String  BeginnigTimePlan { get; set; }
        public String EndTimePlan { get; set; }

        public Chat Chat { get; set; }

        public Group Group { get; set; }

    }
}
