using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class GroupAdmin
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Group Group { get; set; }

        public int GroupAdminForeignKey { get; set; }

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}
