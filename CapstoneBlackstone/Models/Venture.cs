using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneBlackstone
{
    public class Venture
    {

        public Venture()
        {
            this.memberNameAndRoleList = new List<Tuple<string, string, string>>();
            this.AllVentureSkills = new List<Tuple<int, string>>();
            this.staticMembersList = new List<Tuple<string, string, int>>();
        }


        public int ventureID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string aboutUs { get; set; }
        public string contactEmail { get; set; }
        public string contactPhoneNumber { get; set; }
        public string contactLinkedIn { get; set; }
        public byte[] Picture { get; set; }
        public string image { get; set; }
        public string primaryContactEmail { get; set; }
        public Boolean isActive { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public string lastUpdateUser { get; set; }
        public int VentureRank { get; set; }

        public List<Tuple<string, string, string>> memberNameAndRoleList { get; set; }
        public List<Tuple<int, string>> AllVentureSkills { get; set; }
        public List<Tuple<string, string, int>> staticMembersList { get; set; }
    }

}
    