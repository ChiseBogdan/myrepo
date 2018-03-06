using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class ActivityViewModel
    {
        public int IdActivity { get; set; }

        public String Description { get; set; }

        public String BeginningTime{ get; set; }

        public String EndingTime { get; set; }
    }
}
