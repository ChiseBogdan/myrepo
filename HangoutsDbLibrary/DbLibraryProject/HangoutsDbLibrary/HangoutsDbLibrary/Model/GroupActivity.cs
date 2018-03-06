using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class GroupActivity
    {
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public int ActivityId { get; set; }

        public Activity Activity { get; set; }
    }
}
