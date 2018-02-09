using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneBlackstone
{
    public class ExpertVenture
    {
        public int tuID { get; set; }
        public int ventureID { get; set; }
        public string role { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string lastUpdateUser { get; set; }
    }
}