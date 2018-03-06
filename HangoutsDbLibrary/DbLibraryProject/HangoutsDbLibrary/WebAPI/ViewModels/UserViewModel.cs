using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class UserViewModel
    {
        public String Fullname { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }

        public int idUser { get; set; }
    }
}
