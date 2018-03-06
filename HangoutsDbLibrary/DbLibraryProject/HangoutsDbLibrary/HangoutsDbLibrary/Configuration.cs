using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary
{
    public static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                return "Data Source=.;Initial Catalog=Hangouts;Integrated Security=True;";
            }
        }
    }
}
