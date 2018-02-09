using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Connection;
using System.Drawing;
using System.DirectoryServices;
using CapstoneBlackstone.C_SharpClasses;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CapstoneBlackstone.C_SharpClasses
{
    public class SessionMethods: System.Web.UI.Page
    {
        public void storeExpertDataInSession()
        {
            Expert expertProfileObj = new Expert();
            //add information from Session/LDAP
           

            expertProfileObj = getAllTheExpertInfo((string)Session["TU_ID"]);

            //store object in session
            Session["expertProfileObj"] = expertProfileObj;
        }

        public Expert getAllTheExpertInfo(string tuid)
        {
            DbMethods DbMethods = new DbMethods();
            Expert expertProfileObj = new Expert();

            //add information from ExpertProfile table
            DataSet expertInfoDS = DbMethods.GetExpertInfo(tuid);
            expertProfileObj.tuID = expertInfoDS.Tables[0].Rows[0][0].ToString();
            expertProfileObj.firstName = expertInfoDS.Tables[0].Rows[0][3].ToString();
            expertProfileObj.lastName = expertInfoDS.Tables[0].Rows[0][2].ToString();
            expertProfileObj.username = expertInfoDS.Tables[0].Rows[0][1].ToString();
            expertProfileObj.email = expertInfoDS.Tables[0].Rows[0][4].ToString();
            expertProfileObj.dateJoined = Convert.ToDateTime(expertInfoDS.Tables[0].Rows[0][5].ToString());
            expertProfileObj.phoneNumber = expertInfoDS.Tables[0].Rows[0][6].ToString();
            expertProfileObj.aboutMe = expertInfoDS.Tables[0].Rows[0][7].ToString();
            expertProfileObj.college = expertInfoDS.Tables[0].Rows[0][8].ToString();
            expertProfileObj.major = expertInfoDS.Tables[0].Rows[0][9].ToString();
            expertProfileObj.linkedIn = expertInfoDS.Tables[0].Rows[0][10].ToString();
            expertProfileObj.isActive = Convert.ToBoolean(expertInfoDS.Tables[0].Rows[0][12].ToString());
            expertProfileObj.skillGroupID = Convert.ToInt32(expertInfoDS.Tables[0].Rows[0][13].ToString());
            expertProfileObj.genderID = Convert.ToInt32(expertInfoDS.Tables[0].Rows[0][14].ToString());
            expertProfileObj.ethnicityID = Convert.ToInt32(expertInfoDS.Tables[0].Rows[0][15].ToString());
            expertProfileObj.lastUpdateDate = Convert.ToDateTime(expertInfoDS.Tables[0].Rows[0][16].ToString());
            expertProfileObj.lastUpdateUser = expertInfoDS.Tables[0].Rows[0][17].ToString();
            expertProfileObj.picture = (byte[]) expertInfoDS.Tables[0].Rows[0][11];

            //add skill group name
            expertProfileObj.SkillGroupName = DbMethods.GetSkillGroupName(expertProfileObj.skillGroupID).ToString();

            //add roles and venture names
            DataSet roleAndVentureNameDS = DbMethods.GetExpertRoleAndVenture(tuid);
            for (int i = 0; i < roleAndVentureNameDS.Tables[0].Select().Length; i++)
            {
                string role = roleAndVentureNameDS.Tables[0].Rows[i][0].ToString();
                string ventureName = roleAndVentureNameDS.Tables[0].Rows[i][1].ToString();

                expertProfileObj.roleVentureNameList.Add(new Tuple<string, string>(ventureName, role));
            }

            //get all expert sklls and add them to the expert object
            DataTable expertSklls = DbMethods.GetExpertsSkills(tuid).Tables[0];
            for (int z = 0; z < expertSklls.Rows.Count; z++)
            {
                int SkillID = Convert.ToInt32(expertSklls.Rows[z][0]);
                string SkillName = expertSklls.Rows[z][1].ToString();

                expertProfileObj.AllExpertSkills.Add(new Tuple<int, string>(SkillID, SkillName));
            }
            return expertProfileObj;
        }

        public void storeVentureDataInSession(int ventureID)
        {
            DbMethods DbMethods = new DbMethods();
            Venture ventureObj = new Venture();
            ventureObj = DbMethods.GetVenture(ventureID);

            //store all wanted skills
            DataSet allVentureSkills = DbMethods.GetVentureSkills(ventureID);
            for (int i = 0; i < allVentureSkills.Tables[0].Select().Length; i++)
            {
                int SkillID = (int)allVentureSkills.Tables[0].Rows[i][0];
                string SkillName = allVentureSkills.Tables[0].Rows[i][1].ToString();
                ventureObj.AllVentureSkills.Add(new Tuple<int, string>(SkillID, SkillName));
            }

            //store members and roles
            DataSet ventureMembersAndRolesDS = DbMethods.GetVentureMembersAndRoles(ventureID);
            for (int i = 0; i < ventureMembersAndRolesDS.Tables[0].Select().Length; i++)
            {
                string role = ventureMembersAndRolesDS.Tables[0].Rows[i][0].ToString();
                string firstName = ventureMembersAndRolesDS.Tables[0].Rows[i][1].ToString();
                string lastName = ventureMembersAndRolesDS.Tables[0].Rows[i][2].ToString();
                string memberName = firstName + " " + lastName;
                string username = ventureMembersAndRolesDS.Tables[0].Rows[i][3].ToString();
                

                ventureObj.memberNameAndRoleList.Add(new Tuple<string, string, string>(username, memberName, role));
            }
            //store static members and roles
            DataSet staticMembersAndRolesDS = DbMethods.GetAllStaticMembersByVentureID(ventureID);
            for (int i = 0; i < staticMembersAndRolesDS.Tables[0].Select().Length; i++)
            {
                int StaticMemberID = (int)staticMembersAndRolesDS.Tables[0].Rows[i][0];
                string firstName = staticMembersAndRolesDS.Tables[0].Rows[i][1].ToString();
                string lastName = staticMembersAndRolesDS.Tables[0].Rows[i][2].ToString();
                string role = staticMembersAndRolesDS.Tables[0].Rows[i][3].ToString();
                string staticMemberName = firstName + " " + lastName;
                

                ventureObj.staticMembersList.Add(new Tuple<string, string, int>(staticMemberName, role, StaticMemberID));
            }


            //store object in session
            Session["ventureObj"] = ventureObj;
        }
    }
}