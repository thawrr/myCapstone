using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using CapstoneBlackstone.C_SharpClasses;
using Connection;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CapstoneBlackstone
{
    public partial class EditVenture : System.Web.UI.Page
    {
        DbMethods DbMethodsObj = new DbMethods();
        Validation valid = new Validation();
        Venture ventureObj = new Venture();
        SessionMethods sm = new SessionMethods();
        Data d = new Data();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null)
            {
                Response.Redirect("default.aspx");
            }
            else if (!IsPostBack)
            {
                string ventureName = Session["currentVentureName"].ToString();
                int ventureID = Convert.ToInt32(DbMethodsObj.GetVentureID(ventureName));
                sm.storeVentureDataInSession(ventureID);
                ventureObj = (Venture)Session["ventureObj"];
                FillControls();
            }
        }

        public void FillControls()
        {
            if(ventureObj != null)
            {
                if(ventureObj.memberNameAndRoleList.Count != 0)
                {
                    gvVentureMembers.DataSource = ventureObj.memberNameAndRoleList;//just userNames
                    gvVentureMembers.DataBind();
                }
                else
                {
                    gvVentureMembers.DataSource = null;
                    gvVentureMembers.DataBind();
                }
                
                if(ventureObj.staticMembersList.Count != 0)
                {
                    string[] staticMemberIDKeys = new string[1];
                    staticMemberIDKeys[0] = "Item3";
                    gvStaticMembers.DataSource = ventureObj.staticMembersList;
                    gvStaticMembers.DataKeyNames = staticMemberIDKeys;
                    gvStaticMembers.DataBind();
                }
                else
                {
                    string[] staticMemberIDKeys = new string[1];
                    staticMemberIDKeys[0] = "Item3";
                    gvStaticMembers.DataSource = null;
                    gvStaticMembers.DataKeyNames = staticMemberIDKeys;
                    gvStaticMembers.DataBind();
                }
                
                if (ventureObj.AllVentureSkills.Count != 0)
                {
                    gvDeleteSkills.DataSource = ventureObj.AllVentureSkills;
                    gvDeleteSkills.DataBind();
                }
                else
                {
                    gvDeleteSkills.DataSource = null;
                    gvDeleteSkills.DataBind();
                }
                
                txtVentureName.Text = ventureObj.name;
                txtDescription.Text = ventureObj.description;
                txtAboutUs.Text = ventureObj.aboutUs;
                txtPrimaryEmail.Text = ventureObj.primaryContactEmail;
                txtEmail.Text = ventureObj.contactEmail;
                txtPhoneNumber.Text = ventureObj.contactPhoneNumber;
                txtLinkedIn.Text = ventureObj.contactLinkedIn;
            }
        }

        protected void gvDeleteSkills_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ventureObj = (Venture)Session["ventureObj"];
            if (e.CommandName == "gvCommandDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = gvDeleteSkills.Rows[index];
                string s = selectedRow.Cells[0].Text;//get skill name from selected row to be deleted.
                int count = 0;//test counter if no matches were found in forEach loop

                foreach (Tuple<int, String> skill in ventureObj.AllVentureSkills)
                {
                    if (skill.Item2.Equals(s, StringComparison.OrdinalIgnoreCase))
                    {
                        count++;//found something
                        int result = DbMethodsObj.DeleteVentureSkill(ventureObj.ventureID, skill.Item1);
                        if (result != -1)
                        {
                            lblGvDeleteTest.Text = "Skill has been deleted.";
                        }
                        else
                            lblGvDeleteTest.Text = "Unable to restore file. DataBase error. Please try again.";
                    }
                }
                if (count == 0)
                    lblGvDeleteTest.Text = "could not find skill name in local DataSet. :(";
            }//end Delete Skill Row Command
            sm.storeVentureDataInSession(ventureObj.ventureID);//now get the whole expert object
            ventureObj = (Venture)Session["ventureObj"];
            FillControls();
        }//end click event

        protected void btnSubmitExpert_Click(object sender, EventArgs e)
        {
            lblMemberRequired.Visible = false;
            lblEmailNotInSystem.Visible = false;
            int count = Convert.ToInt32(DbMethodsObj.CheckIfExpertEmailExists(txtExpertEmail.Text));
            ventureObj = (Venture)Session["ventureObj"];
            if (valid.IsBlank(txtExpertEmail.Text) || valid.IsBlank(txtExpertRole.Text))
            {
                lblMemberRequired.Visible = true;
            }
            else if (count < 1)
            {
                lblEmailNotInSystem.Visible = true;
            }
            else
            {
                string email = txtExpertEmail.Text;
                string TUID = DbMethodsObj.GetExpertTUID(email).ToString();
                string role = txtExpertRole.Text;
                DateTime lastUpdateDate = DateTime.Now;
                Expert expertProfileObj = (CapstoneBlackstone.Expert)Session["expertProfileObj"];
                string lastUpdateUser = expertProfileObj.lastName + ", " + expertProfileObj.firstName;

                DataSet ventureMember = new DataSet();
                ventureMember = DbMethodsObj.CheckIfVentureMemberExists(ventureObj.ventureID, TUID);

                if (ventureMember.Tables[0].Rows.Count != 0)
                {
                    DbMethodsObj.ReactivateVentureMember(TUID, ventureObj.ventureID, role);
                    txtExpertEmail.Text = "";
                    txtExpertRole.Text = "";
                    sm.storeVentureDataInSession(ventureObj.ventureID);
                    ventureObj = (Venture)Session["ventureObj"];
                    FillControls();
                }
                else
                {
                    DbMethodsObj.CreateVentureMember(TUID, ventureObj.ventureID, role, lastUpdateDate, lastUpdateUser);
                    txtExpertEmail.Text = "";
                    txtExpertRole.Text = "";
                    sm.storeVentureDataInSession(ventureObj.ventureID);
                    ventureObj = (Venture)Session["ventureObj"];
                    FillControls();
                }
            }
        }

        protected void btnRemoveExpert_Click(object sender, EventArgs e)
        {
            ventureObj = (Venture)Session["ventureObj"];
            for (int row = 0; row < gvVentureMembers.Rows.Count; row++)
            {
                CheckBox CBox;
                CBox = (CheckBox)gvVentureMembers.Rows[row].FindControl("chkSelect");

                if (CBox.Checked)
                {
                    string tuid = DbMethodsObj.GetTUIDByUsername(gvVentureMembers.Rows[row].Cells[1].Text);
                    DbMethodsObj.DeactivateVentureMember(ventureObj.ventureID, tuid);
                }
            }
            sm.storeVentureDataInSession(ventureObj.ventureID);
            ventureObj = (Venture)Session["ventureObj"];
            FillControls();
        }

        protected void btnSubmitStaticMember_Click(object sender, EventArgs e)
        {
            ventureObj = (Venture)Session["ventureObj"];
            if (valid.IsBlank(txtFirstName.Text) || valid.IsBlank(txtLastName.Text) || valid.IsBlank(txtRole.Text))
            {
                lblStaticRequired.Visible = true;
            }
            else
            {
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string role = txtRole.Text;
                DateTime lastUpdateDate = DateTime.Now;
                Expert expertProfileObj = (CapstoneBlackstone.Expert)Session["expertProfileObj"];
                string lastUpdateUser = expertProfileObj.lastName + ", " + expertProfileObj.firstName;

                DbMethodsObj.CreateStaticMember(ventureObj.ventureID, firstName, lastName, role, lastUpdateDate, lastUpdateUser);
                txtRole.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                sm.storeVentureDataInSession(ventureObj.ventureID);
                ventureObj = (Venture)Session["ventureObj"];
                FillControls();
            }
            
        }

        protected void btnRemoveStaticMember_Click(object sender, EventArgs e)
        {
            ventureObj = (Venture)Session["ventureObj"];
            for (int row = 0; row < gvStaticMembers.Rows.Count; row++)
            {
                CheckBox CBox;
                CBox = (CheckBox)gvStaticMembers.Rows[row].FindControl("chkSelect");

                if (CBox.Checked)
                {
                    int staticMemberID = Convert.ToInt32(gvStaticMembers.DataKeys[row].Values[0]);
                    DbMethodsObj.DeactivateStaticMemberByID(staticMemberID);
                }
            }
            sm.storeVentureDataInSession(ventureObj.ventureID);
            ventureObj = (Venture)Session["ventureObj"];
            FillControls();
        }

        protected void btnDeactivateVenture_Click(object sender, EventArgs e)
        {
            btnYes.Visible = true;
            btnNo.Visible = true;
            lblAreYouSure.Visible = true;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            btnNo.Visible = false;
            btnYes.Visible = false;
            lblAreYouSure.Visible = false;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            DbMethodsObj.DeactivateVenture(ventureObj.ventureID);
            Expert expertProfileObj = (CapstoneBlackstone.Expert)Session["expertProfileObj"];
            Response.Redirect("ExpertPage.aspx?username=" + expertProfileObj.username);
        }

        protected void BtnSubmitNewSkill_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Entering BtnSubmitNewSkill_Click");
            ventureObj = (Venture)Session["ventureObj"];
            int test = 0;
            int result;
            if (string.IsNullOrWhiteSpace(txtAddNewSkill.Text) == false)
            {
                //search expert's skillset to see if the skill added already exsists
                //search all skill to see if the skill added exsists
                //if not to both, add a new skill
                foreach (Tuple<int, String> skill in ventureObj.AllVentureSkills)//test if skill added is already in Expert's Skill Set
                {
                    if (skill.Item2.Equals(txtAddNewSkill.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        test = -1;
                        break;
                    }
                }
                if (test != -1)
                {
                    List<Skills> allSkills = DbMethodsObj.GetSkills();
                    foreach (Skills skill in allSkills)//handle adding the skill
                    {
                        if (skill.SkillName.Equals(txtAddNewSkill.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            result = DbMethodsObj.AddSkillToVenture(ventureObj.ventureID, skill.SkillID);//adding excisting skill to expert
                            if (result != -1)
                            {
                                lblAddSkillTest.Text = "Success! New Skill was added to Skills and Expert_Skills Tables.";
                                test = -2;
                                break;
                            }
                            else
                                lblAddSkillTest.Text = "Error: Process did not execute successfully. :(";
                            break;
                        }
                    }
                    if (test != -2)
                    {
                        result = DbMethodsObj.AddNewSkillToVenture(ventureObj.ventureID, txtAddNewSkill.Text);//adding brandnew skill
                        if (result != -1)
                            lblAddSkillTest.Text = "Success! A New Skill was added to the Ventures wanted Skills.";
                        else
                            lblAddSkillTest.Text = "Error: Process did not execute successfully. :(";
                    }
                    sm.storeVentureDataInSession(ventureObj.ventureID);//now get the whole expert object
                    ventureObj = (Venture)Session["ventureObj"];
                    FillControls();
                }
                else
                    lblAddSkillTest.Text = "Cannot add this skill. you already have this skill in your skill set.";
            }//end if statement for textBoxes
            else
                lblAddSkillTest.Text = "Error: Invalid input.";
        }

        protected void btnEditVenture_Click(object sender, EventArgs e)
        {
            lblPicCheckType.Visible = false;
            lblPicCheckSize.Visible = false;
            ventureObj = (Venture)Session["ventureObj"];
            lblRequired.Visible = false;
            lblVentureNameCheck.Visible = false;
            int count = Convert.ToInt32(DbMethodsObj.CheckIfVentureNameExists(txtVentureName.Text));
            if (valid.IsBlank(txtVentureName.Text) || valid.IsBlank(txtPrimaryEmail.Text) || valid.IsBlank(txtEmail.Text) || valid.IsBlank(txtDescription.Text))
            {
                lblRequired.Visible = true;
            }
            else if (count >= 1 && txtVentureName.Text != ventureObj.name)
            {
                lblVentureNameCheck.Visible = true;
            }
            else
            {
                Venture v = new Venture();

                v.ventureID = Convert.ToInt32(DbMethodsObj.GetVentureID(ventureObj.name));
                v.name = txtVentureName.Text;
                v.description = txtDescription.Text;
                v.aboutUs = txtAboutUs.Text;
                v.contactEmail = txtEmail.Text;
                v.contactPhoneNumber = txtPhoneNumber.Text;
                v.contactLinkedIn = txtLinkedIn.Text;
                v.primaryContactEmail = txtPrimaryEmail.Text;
                v.lastUpdateDate = DateTime.Now;
                Expert expertProfileObj = (CapstoneBlackstone.Expert)Session["expertProfileObj"];
                v.lastUpdateUser = expertProfileObj.lastName + ", " + expertProfileObj.firstName;

                if (FileUpload1.HasFile)
                {
                    string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);//uploaded file extension
                    int iFileSize = FileUpload1.PostedFile.ContentLength;//uploaded file size
                    if (valid.TestForLegalImageTypes(fileExtension) == false)
                    {//fail
                        lblPicCheckType.Visible = true;
                        lblPicCheckType.Text = fileExtension + " file extension is not allowed. Please use .png, .gif, .jpg, .jpeg, .pdf, .pcd, .fpx, .tif instead";
                    }
                    else if (iFileSize >= 90000)
                    {//fail
                        lblPicCheckSize.Visible = true;
                        lblPicCheckSize.Text = "Your file size is " + iFileSize + " bytes. Please reduce the size to less than 90 KB (90000 bytes).";
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        var img = System.Drawing.Image.FromStream(FileUpload1.FileContent);
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        v.Picture = ms.ToArray();
                        int result = DbMethodsObj.UpdateVenture(v);
                        Response.Redirect("VenturePage.aspx?name=" + ventureObj.name);
                    }
                }
                else
                {
                    v.Picture = ventureObj.Picture;
                    int result = DbMethodsObj.UpdateVenture(v);
                    Response.Redirect("VenturePage.aspx?name=" + ventureObj.name);
                }
            }
        }//end edit venture click event

        //autoComplete method for textBox
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchSkills(string prefixText, int count)
        {
            //change later to search local dataSet

            DBConnect connection = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetSkillNamesBySkillName";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@searchSN", prefixText);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 200;
            objCommand.Parameters.Add(inputParameter);

            DataSet ds = connection.GetDataSetUsingCmdObj(objCommand);//contains all skillIds and SkillNames for the specific expert

            //change later to search local dataSet
            DataTable dt = ds.Tables[0];

            List<string> skills = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                skills.Add(row["SkillName"].ToString());//add all returned skillNames to List if string
            }
            return skills;//returned search result of skillNames
        }//end web service

    }//end class
}//end nameSpace
