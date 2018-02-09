using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneBlackstone
{
    public class StaticMember
    {
        public int staticMemberID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string role { get; set; }
        public int ventureID { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string lastUpdateUser { get; set; }
    }
}