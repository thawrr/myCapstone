using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapstoneBlackstone.C_SharpClasses;
using System.IO;
using System.Web.SessionState;

namespace CapstoneBlackstone
{
    public partial class ExpertPage1 : System.Web.UI.Page, IRequiresSessionState
    {
        DbMethods DbMethods = new DbMethods();
        TempleUser.StudentObj studentObj = new TempleUser.StudentObj();
        Validation vlad = new Validation();
        Data d = new Data();
        Expert expertObj = new Expert();
        SessionMethods sm = new SessionMethods();
        
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (DbMethods.CheckIfAdminExists((string)Session["TU_ID"]))
            {
                this.MasterPageFile = "~/Admin.Master";
            }
            else
            {
                this.MasterPageFile = "~/Blackstone.Master";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string username;
            expertObj = (Expert)Session["expertProfileObj"];

            if (Session["Authenticated"] == null)
            {
                username = null;
                Session.Clear();
                Session.Abandon();
                Response.Redirect("default.aspx");
            }
            else if (Request.QueryString["username"] != null)
            {
                username = Request.QueryString["username"];
                studentObj.tuid = DbMethods.GetTUIDByUsername(username);
                expertObj = sm.getAllTheExpertInfo(studentObj.tuid);
            }
            else
            {
                studentObj.tuid = DbMethods.GetTUIDByUsername(expertObj.username);
                expertObj = sm.getAllTheExpertInfo(studentObj.tuid);
            }           

            if (!IsPostBack)
                FillControls();

            if(studentObj.tuid != (string)Session["TU_ID"])
            {
                btnEditExpertPage.Visible = false;
            }
        }//end page load

        // Fill page controls with updated information
        public void FillControls()
        {
            if (expertObj != null)
            {
                if (expertObj.picture != null)
                {
                    imgProfilePic.ImageUrl = d.ConvertToImage(expertObj.picture);//returns string url 
                }
                else
                {//in case expert's profile pic is null
                    imgProfilePic.ImageUrl = "Images/TUOwls_logo.png";
                }
                if (studentObj.tuid != (string)Session["TU_ID"])
                {
                    Expert e = sm.getAllTheExpertInfo(studentObj.tuid);
                    h2Name.InnerText = e.firstName + " " + e.lastName;
                    h3SkillGroup.InnerText = e.SkillGroupName;
                    spCollege.InnerText = e.college;
                    spMajor.InnerText = e.major;
                    spEmail.InnerText = e.email;
                    spPhone.InnerText = e.phoneNumber;
                    spLinkedIn.InnerText = e.linkedIn;
                    pAboutMe.InnerText = e.aboutMe;

                    if (e.roleVentureNameList.Count == 0)
                    {
                        this.rptVentureNames.DataSource = null;
                        this.rptVentureNames.DataBind();
                    }
                    else
                    {
                        this.rptVentureNames.DataSource = e.roleVentureNameList;
                        this.rptVentureNames.DataBind();
                    }

                    if (e.AllExpertSkills.Count == 0)
                    {
                        this.rptExpertSkills.DataSource = null;
                        this.rptExpertSkills.DataBind();
                    }
                    else
                    {
                        this.rptExpertSkills.DataSource = e.AllExpertSkills;
                        this.rptExpertSkills.DataBind();
                    }
                }
                else
                {
                    h2Name.InnerText = expertObj.firstName + " " + expertObj.lastName;
                    h3SkillGroup.InnerText = expertObj.SkillGroupName;
                    spCollege.InnerText = expertObj.college;
                    spMajor.InnerText = expertObj.major;
                    spEmail.InnerText = expertObj.email;
                    spPhone.InnerText = expertObj.phoneNumber;
                    spLinkedIn.InnerText = expertObj.linkedIn;
                    pAboutMe.InnerText = expertObj.aboutMe;

                    if (expertObj.roleVentureNameList.Count == 0)
                    {
                        this.rptVentureNames.DataSource = null;
                        this.rptVentureNames.DataBind();
                    }
                    else
                    {
                        this.rptVentureNames.DataSource = expertObj.roleVentureNameList;
                        this.rptVentureNames.DataBind();
                    }

                    if (expertObj.AllExpertSkills.Count == 0)
                    {
                        this.rptExpertSkills.DataSource = null;
                        this.rptExpertSkills.DataBind();
                    }
                    else
                    {
                        this.rptExpertSkills.DataSource = expertObj.AllExpertSkills;
                        this.rptExpertSkills.DataBind();
                    }
                }
            }//end fill controls
        }

        protected void BtnVenture_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "BtnVentureClick")
            {
                string ventureName = e.CommandArgument.ToString();
                //Do something
                Response.Redirect("VenturePage.aspx?name=" + ventureName);
            }
        }//end VentureSearchesExpert Repeater ClickEvent

        protected void btnEditExpertPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditExpert.aspx");
        }
    }//end class
}//end name space