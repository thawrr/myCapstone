using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneBlackstone
{
    public class Administrator
    {
        public int AdministratorID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateUser { get; set; }
    }
}