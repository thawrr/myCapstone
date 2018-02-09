using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneBlackstone
{
    public class Ethnicity
    {
        public int EthnicityID { get; set; }
        public string EthnicityName { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateUser { get; set; }
    }
}