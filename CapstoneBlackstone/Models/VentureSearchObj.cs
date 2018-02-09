using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CapstoneBlackstone.Models
{
    public class VentureSearchObj
    {
        public string TUID { get; set; }
        public string SkillName { get; set; }
        public string SkillGroupName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string image { get; set; }
        public byte[] Picture { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int ExpertRank { get; set; }
        public List<string> AllExpertSkills { get; set; }
        public string cdsAllExpertSkills { get; set; }
        public VentureSearchObj()
        {//initialize list to constructor
            this.AllExpertSkills = new List<string>();
        }
       
    }
}