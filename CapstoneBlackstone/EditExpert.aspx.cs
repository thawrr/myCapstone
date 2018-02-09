using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapstoneBlackstone.C_SharpClasses;
using System.Diagnostics;
using System.Collections;
using Connection;
using System.Data.SqlClient;
using System.IO;

namespace CapstoneBlackstone
{
    public partial class EditExpertPage : System.Web.UI.Page
    {
        DbMethods DbMethods = new DbMethods();//http://localhost:55996/EditExpert.aspx.cs
        TempleUser.StudentObj studentObj = new TempleUser.StudentObj();
        Validation vlad = new Validation();
        CapstoneBlackstone.C_SharpClasses.Data templeData = new CapstoneBlackstone.C_SharpClasses.Data();
        SessionMethods sm = new SessionMethods();
        Data d = new Data();
        Expert expertObj = new Expert();

        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("Entering Page_Load");
            string username;
            if (Session["Authenticated"] == null)
            {
                username = null;
                Session.Clear();
                Session.Abandon();
                Response.Redirect("default.aspx");
            }
            else if (!IsPostBack)
            {
                expertObj = (Expert)Session["expertProfileObj"];
                FillControls();
            }
        }//end page load method

        // Fill page controls with updated information
        public void FillControls()
        {
            expertObj = (Expert)Session["expertProfileObj"];
            if (expertObj != null)
            {
                SkillGroupDropdown.DataSource = DbMethods.GetAllSkillGroups();
                SkillGroupDropdown.DataValueField = "SkillGroupID";
                SkillGroupDropdown.DataTextField = "SkillGroupName";
                SkillGroupDropdown.DataBind();
                SkillGroupDropdown.SelectedIndex = SkillGroupDropdown.Items.IndexOf(SkillGroupDropdown.Items.FindByValue(expertObj.skillGroupID.ToString()));

                txtFirstName.Text = expertObj.firstName;
                txtLastName.Text = expertObj.lastName;
                txtUsername.Text = expertObj.username;
                txtEmail.Text = expertObj.email;
                txtPhoneNumber.Text = expertObj.phoneNumber;
                txtLinkedIn.Text = expertObj.linkedIn;
                aboutMe.Text = expertObj.aboutMe;

                //get all Expert's skills and put them on the page
                if (expertObj.AllExpertSkills.Count == 0)
                {
                    lblDeleteTest.Text = "You (the Expert) don't have any skils in your profile. Please add some on this page.";
                    gvDeleteSkills.DataSource = null;
                    gvDeleteSkills.DataBind();
                }
                else
                {
                    this.gvDeleteSkills.DataSource = expertObj.AllExpertSkills;
                    this.gvDeleteSkills.DataBind();
                }
            }//end if
        }//end fill controls
        
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
        }

        protected void BtnSubmitNewSkill_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Entering BtnSubmitNewSkill_Click");
            int test = 0;
            int result;
            expertObj = (Expert)Session["expertProfileObj"];

            if (string.IsNullOrWhiteSpace(txtAddNewSkill.Text)==false)
            {
                //search expert's skillset to see if the skill added already exsists
                //search all skill to see if the skill added exsists
                //if not to both, add a new skill
                foreach (Tuple<int,String> skill in expertObj.AllExpertSkills)//test if skill added is already in Expert's Skill Set
                {
                    if(skill.Item2.Equals(txtAddNewSkill.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        test = -1;
                        break;
                    }
                }
                if (test != -1)
                {
                    List<Skills> allSkills = DbMethods.GetSkills();
                    foreach (Skills skill in allSkills)//handle adding the skill
                    {
                        if (skill.SkillName.Equals(txtAddNewSkill.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            result = DbMethods.AddSkillToUser(expertObj.tuID, skill.SkillID);//adding excisting skill to expert
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
                    if(test != -2)
                    {
                        result = DbMethods.AddNewSkillToUser(expertObj.tuID, txtAddNewSkill.Text);//adding brandnew skill
                        if (result != -1)
                            lblAddSkillTest.Text = "Success! New Skill was added to Skills and Expert_Skills Tables.";
                        else
                            lblAddSkillTest.Text = "Error: Process did not execute successfully. :(";
                    }
                    sm.storeExpertDataInSession();//now get the whole expert object
                    FillControls();
                }
                else
                    lblAddSkillTest.Text = "Cannot add this skill. you already have this skill in your skill set.";
            }//end if statement for textBoxes
            else
                lblAddSkillTest.Text = "Error: Invalid input.";
        }//end add new skill

        protected void gvDeleteSkills_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "gvCommandDelete")
            {
                expertObj = (Expert)Session["expertProfileObj"];
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow selectedRow = gvDeleteSkills.Rows[index];
                string s = selectedRow.Cells[0].Text;//get skill name from selected row to be deleted.
                int count = 0;//test counter if no matches were found in forEach loop

                foreach (Tuple<int, String> skill in expertObj.AllExpertSkills)
                {
                    if (skill.Item2.Equals(s, StringComparison.OrdinalIgnoreCase))
                    {
                        count++;//found something
                        int result = DbMethods.DeleteExpertSkill(expertObj.tuID, skill.Item1);
                        if (result != -1)
                        {
                            lblGvDeleteTest.Text = "Skill has been deleted.";
                            sm.storeExpertDataInSession();//now get the whole expert object
                            FillControls();
                        }
                        else
                            lblGvDeleteTest.Text = "Unable to restore file. DataBase error. Please try again.";
                    }
                    if (count == 0)
                        lblGvDeleteTest.Text = "could not find skill name in local DataSet. :(";
                }
            }//end Delete Skill Row Command
        }//end gvDeleteSkills_RowCommand

        protected void btnDeactivateExpert_Click(object sender, EventArgs e)
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
            string tuID = (string)Session["TU_ID"];
            DbMethods.DeactivateExpert(tuID);
            Response.Redirect("default.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblPicCheckType.Visible = false;
            lblPicCheckSize.Visible = false;
            byte[] imageBytes;
            Expert expertProfileObj = (Expert)Session["expertProfileObj"];
            string username = expertProfileObj.username;
            int count = Convert.ToInt32(DbMethods.CheckIfUsernameExists(txtUsername.Text));
            if (count >= 1 && txtUsername.Text != username)
            {
                lblUsernameCheck.Visible = true;
            }
            else if (vlad.IsBlank(txtFirstName.Text) || vlad.IsBlank(txtLastName.Text) || vlad.IsBlank(txtEmail.Text) || vlad.IsBlank(txtUsername.Text) || vlad.IsBlank(txtEmail.Text))
            {
                lblRequired.Visible = true;
            }
            else
            {
                string fileExtension = Path.GetExtension(fileNew1.PostedFile.FileName);//uploaded file extension
                int iFileSize = fileNew1.PostedFile.ContentLength;//uploaded file size
                
                if (fileNew1.FileContent != null && fileNew1.HasFile == true)
                {//if you have a file in the file upload control
                    if (vlad.TestForLegalImageTypes(fileExtension) == false)
                    {//fail
                        lblPicCheckType.Visible = true;
                        lblPicCheckType.Text = fileExtension + " file extension is not allowed. Please use .png, .gif, .jpg, .jpeg, .pdf, .pcd, .fpx, .tif instead";
                    }
                    else if(iFileSize >= 90000)
                    {//fail
                        lblPicCheckSize.Visible = true;
                        lblPicCheckSize.Text = "Your file size is "+ iFileSize+" bytes. Please reduce the size to less than 90 KB (90000 bytes).";
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        var img = System.Drawing.Image.FromStream(fileNew1.FileContent);
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        imageBytes = ms.ToArray();
                        Session.Add("picture", imageBytes);

                        Expert updatedExpert = new Expert
                        {
                            firstName = txtFirstName.Text,
                            lastName = txtLastName.Text,
                            username = txtUsername.Text,
                            email = txtEmail.Text,
                            phoneNumber = txtPhoneNumber.Text,
                            linkedIn = txtLinkedIn.Text,
                            aboutMe = aboutMe.Text,
                            picture = imageBytes,
                            skillGroupID = Convert.ToInt32(SkillGroupDropdown.SelectedItem.Value)
                        };
                        int x = DbMethods.UpdateExpert(updatedExpert, (string)Session["TU_ID"]);
                        sm.storeExpertDataInSession();
                        FillControls();
                        Response.Redirect("ExpertPage.aspx?username=" + updatedExpert.username);
                    }
                }
                else
                {//file upload control was empty, so use old profile pic
                    Expert updatedExpert = new Expert
                    {
                        firstName = txtFirstName.Text,
                        lastName = txtLastName.Text,
                        username = txtUsername.Text,
                        email = txtEmail.Text,
                        phoneNumber = txtPhoneNumber.Text,
                        linkedIn = txtLinkedIn.Text,
                        aboutMe = aboutMe.Text,
                        picture = expertProfileObj.picture,
                        skillGroupID = Convert.ToInt32(SkillGroupDropdown.SelectedItem.Value)
                    };
                    int x = DbMethods.UpdateExpert(updatedExpert, (string)Session["TU_ID"]);
                    sm.storeExpertDataInSession();
                    FillControls();
                    Response.Redirect("ExpertPage.aspx?username=" + updatedExpert.username);
                }
            }
            
        }
    }//end class
}//end nameSPace