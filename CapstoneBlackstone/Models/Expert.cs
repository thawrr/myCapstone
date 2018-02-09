using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneBlackstone
{
    public class Expert
    {
        public Expert()
        {
            this.roleVentureNameList = new List<Tuple<string, string>>();
            this.AllExpertSkills = new List<Tuple<int, string>>();
        }
        
        public string tuID { get; set; }
        public string username { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string college { get; set; }
        public string major { get; set; }
        public string gender { get; set; }
        public string ethnicity { get; set; }
        public string email { get; set; }
        public DateTime dateJoined { get; set; }
        public string phoneNumber { get; set; }
        public string aboutMe { get; set; }
        public string linkedIn { get; set; }
        public byte[] picture { get; set; }
        public Boolean isActive { get; set; }
        public int skillGroupID { get; set; }
        public int genderID { get; set; }
        public int ethnicityID { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string lastUpdateUser { get; set; }
        public string SkillGroupName { get; set; }
        public string role { get; set; }
        public int ExpertRank { get; set; }

        public List<Tuple<string, string>> roleVentureNameList { get; set; }
        public List<Tuple<int,string>> AllExpertSkills { get; set; }

    }
}