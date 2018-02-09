using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneBlackstone
{
    public class SkillGroup
    {
        public int SkillGroupID { get; set; }
        public string SkillGroupName { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateUser { get; set; }
    }
}