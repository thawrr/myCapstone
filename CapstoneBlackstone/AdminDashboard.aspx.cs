using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneBlackstone.C_SharpClasses;

namespace CapstoneBlackstone
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        DbMethods DbMethodsObj = new DbMethods();
        Validation valid = new Validation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null)
            {
                Response.Redirect("default.aspx");
            }
            else if(Session["AdminToken"] == null)
            {
                Response.Redirect("ExpertPage.aspx");
            }
            else if (!IsPostBack)
            {
                gvAdmins.DataSource = DbMethodsObj.GetAllAdmins();
                gvAdmins.DataBind();
            }
        }

        protected void btnAddNewAdmin_Click(object sender, EventArgs e)
        {
            if (valid.IsBlank(txtAdminFirstName.Text) || valid.IsBlank(txtAdminLastName.Text) || valid.IsBlank(txtAdminTUID.Text))
            {
                lblRequired.Visible = true;
            }
            else
            {
                string tuID = txtAdminTUID.Text;
                int count = Convert.ToInt32(DbMethodsObj.CheckIfDeactivatedAdminExists(tuID));
                if (count == 1)
                {
                    DbMethodsObj.ReactivateAdmin(tuID);
                }
                else
                {
                    string firstName = txtAdminFirstName.Text;
                    string lastName = txtAdminLastName.Text;
                    DateTime lastUpdateDate = DateTime.Now;
                    string lastUpdateUser = lastName + ", " + firstName;

                    DbMethodsObj.CreateAdmin(tuID, firstName, lastName, lastUpdateDate, lastUpdateUser);
                }
                gvAdmins.DataSource = DbMethodsObj.GetAllAdmins();
                gvAdmins.DataBind();

                txtAdminTUID.Text = "";
                txtAdminLastName.Text = "";
                txtAdminFirstName.Text = "";
            }
            
        }

        protected void btnRemoveAdmin_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < gvAdmins.Rows.Count; row++)
            {
                CheckBox CBox;
                CBox = (CheckBox)gvAdmins.Rows[row].FindControl("chkSelect");

                if (CBox.Checked)
                {
                    if(gvAdmins.Rows[row].Cells[1].Text == "902626610")
                    {
                        lblSuperAdmin.Visible = true;
                    }
                    else
                    {
                        string tuID = gvAdmins.Rows[row].Cells[1].Text;
                        DbMethodsObj.DeactivateAdmin(tuID);
                    }
                    
                }
            }
            gvAdmins.DataSource = DbMethodsObj.GetAllAdmins();
            gvAdmins.DataBind();
        }
    }
}