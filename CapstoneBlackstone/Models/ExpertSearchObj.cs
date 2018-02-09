using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneBlackstone.Models
{
    public class ExpertSearchObj
    {
        public int VentureID { get; set; }
        public string Name { get; set; }
        public string SkillName { get; set; }
        public byte[] Picture { get; set; }
        public string image { get; set; }
        public string VentureDesc { get; set; }
        public List<string> AllVentureSkills { get; set; }
        public string cdsAllVentureSkills { get; set; }
        public ExpertSearchObj()
        {
            this.AllVentureSkills = new List<string>();
        }
        public int VentureRank { get; set; }
    }
}