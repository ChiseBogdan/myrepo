using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Model
{
    public class Chat
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

       public Plan Plan { get; set; }

       public int ChatPlanForeignKey { get; set; }
    }
}
