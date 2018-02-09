using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CapstoneBlackstone.C_SharpClasses;
using System.Data;

namespace CapstoneBlackstone
{
    public partial class VenturePage : System.Web.UI.Page
    {
        Data d = new Data();
        DbMethods dbm = new DbMethods();
        SessionMethods sm = new SessionMethods();
        Venture ventureObj = new Venture();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (dbm.CheckIfAdminExists((string)Session["TU_ID"]))
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
            if (Session["Authenticated"] == null)
            {
                Response.Redirect("default.aspx");
            }
            else if (Request.QueryString["name"] == null)
            {
                Response.Redirect("Search.aspx");
            }
            else
            {
                string ventureName = Request.QueryString["name"];
                int vId = Convert.ToInt32(dbm.GetVentureID(ventureName));
                Venture v = dbm.GetVenture(vId);

                h2VentureName.InnerText = v.name;
                spVentureContact.InnerText = v.contactEmail;
                pAboutUs.InnerText = v.aboutUs;
                imgLogo.ImageUrl = d.ConvertToImage(v.Picture);
                h3VentureDesc.InnerText = v.description;
                spVenturePhone.InnerText = v.contactPhoneNumber;
                spVentureLinkedIn.InnerText = v.contactLinkedIn;
                lblPrimaryContactEmail.Text = v.primaryContactEmail;

                //put venture data into session
                sm.storeVentureDataInSession(vId);
                ventureObj = (Venture)Session["ventureObj"];
                //Bind Repeaters
                this.rptMembersAndRoles.DataSource = ventureObj.memberNameAndRoleList;
                this.rptMembersAndRoles.DataBind();
                this.rptStaticMembersAndRoles.DataSource = ventureObj.staticMembersList;
                this.rptStaticMembersAndRoles.DataBind();
                this.rptVentureSkills.DataSource = ventureObj.AllVentureSkills;
                this.rptVentureSkills.DataBind();

                //Make sure only members of a venture are able to edit this particular venture
                Expert expertProfileObj = sm.getAllTheExpertInfo((string)Session["TU_ID"]);
                string user_name = expertProfileObj.username;
                DataTable usernamesTable = dbm.GetUsernamesForVenture(vId).Tables[0];
                int isMember = 0;
                for (int i = 0; i < usernamesTable.Rows.Count && isMember == 0; i++)
                {

                    if (usernamesTable.Rows[i][0].ToString() == user_name)
                    {
                        isMember = 1;
                        editVenture.Visible = true;
                    }
                }//End of Restricting Edit Venture
            }
        }

        protected void editVenture_Click(object sender, EventArgs e)
        {
            string currentVentureName = h2VentureName.InnerText;
            Session["currentVentureName"] = currentVentureName;
            Response.Redirect("EditVenture.aspx");
        }

        protected void btnMember_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "btnMemberClick")
            {
                string userName = e.CommandArgument.ToString();
                //Do something
                Response.Redirect("ExpertPage.aspx?username=" + userName);
            }
        }
    }
}