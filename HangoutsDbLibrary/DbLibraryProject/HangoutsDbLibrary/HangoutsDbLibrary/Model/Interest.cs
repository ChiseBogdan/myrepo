using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Interest
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public User User { get; set; }

        public override string ToString()
        {
            return Id + " " + Description;

        }
    }
}
