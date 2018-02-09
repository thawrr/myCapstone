using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneBlackstone;
using CapstoneBlackstone.C_SharpClasses;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using Connection;
using System.Data;

namespace CapstoneBlackstone
{
    public partial class CreateProfile : System.Web.UI.Page
    {
        byte[] imageBytes;
        Validation valid = new Validation();
        DbMethods db = new DbMethods();
        SessionMethods sm = new SessionMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null)
            {
                Response.Redirect("default.aspx");
            }
            else if (!Page.IsPostBack)
            {
                SkillGroupDropdown.DataSource = db.GetAllSkillGroups();
                SkillGroupDropdown.DataTextField = "SkillGroupName";
                SkillGroupDropdown.DataValueField = "SkillGroupID";
                SkillGroupDropdown.DataBind();

                genderDropdown.DataSource = db.GetAllGenders();
                genderDropdown.DataTextField = "GenderName";
                genderDropdown.DataValueField = "GenderID";
                genderDropdown.DataBind();

                ethnicityDropdown.DataSource = db.GetAllEthnicities();
                ethnicityDropdown.DataTextField = "EthnicityName";
                ethnicityDropdown.DataValueField = "EthnicityID";
                ethnicityDropdown.DataBind();

                /*Security Session Variable*/
                Session["Authenticated"] = true;

                txtFirstName.Text = (string)Session["First_Name"];
                txtLastName.Text = (string)Session["Last_Name"];
                txtEmail.Text = (string)Session["Email"];
            }            
        }

        protected void save_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            var img = System.Drawing.Image.FromStream(FileUpload1.FileContent);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageBytes = ms.ToArray();
            Session.Add("picture", imageBytes);
        }

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
            Expert tempExpert = new Expert { tuID=(string)Session["TU_ID"] };
            int test = 0;
            int result;

            if (string.IsNullOrWhiteSpace(txtAddNewSkill.Text) == false)
            {
                //search expert's skillset to see if the skill added already exsists
                //search all skill to see if the skill added exsists
                //if not to both, add a new skill
                foreach (Tuple<int, String> skill in tempExpert.AllExpertSkills)//test if skill added is already in Expert's Skill Set
                {
                    if (skill.Item2.Equals(txtAddNewSkill.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        test = -1;
                        break;
                    }
                }
                if (test != -1)
                {
                    List<Skills> allSkills = db.GetSkills();
                    foreach (Skills skill in allSkills)//handle adding the skill
                    {
                        if (skill.SkillName.Equals(txtAddNewSkill.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            result = db.AddSkillToUser(tempExpert.tuID, skill.SkillID);//adding existing skill to expert
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
                        result = db.AddNewSkillToUser(tempExpert.tuID, txtAddNewSkill.Text);//adding brandnew skill
                        if (result != -1)
                            lblAddSkillTest.Text = "Success! New Skill was added to Skills and Expert_Skills Tables.";
                        else
                            lblAddSkillTest.Text = "Error: Process did not execute successfully. :(";
                    }
                    //sm.storeExpertDataInSession();//now get the whole expert object
                    gvDeleteSkills.DataSource = db.GetExpertsSkills(tempExpert.tuID);
                    gvDeleteSkills.DataBind();                    
                }
                else
                    lblAddSkillTest.Text = "Cannot add this skill. you already have this skill in your skill set.";
            }//end if statement for textBoxes
            else
                lblAddSkillTest.Text = "Error: Invalid input.";
        }
        

        protected void btnCreateProfile_Click(object sender, EventArgs e)
        {
            lblPicCheckSize.Visible = false;
            lblPicCheckType.Visible = false;
            int count = Convert.ToInt32(db.CheckIfUsernameExists(txtUsername.Text));
            if (valid.IsBlank(txtFirstName.Text) || valid.IsBlank(txtLastName.Text) || valid.IsBlank(txtEmail.Text) || valid.IsBlank(txtUsername.Text) || valid.IsBlank(txtEmail.Text))
            {
                lblRequired.Visible = true;
            }
            else if(count >= 1)
            {
                lblUsernameCheck.Visible = true;
            }
            else
            {
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);//uploaded file extension
                int iFileSize = FileUpload1.PostedFile.ContentLength;//uploaded file size
                if (FileUpload1.FileContent != null && FileUpload1.HasFile)
                {
                    if(valid.TestForLegalImageTypes(fileExtension)==false)
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
                        imageBytes = ms.ToArray();
                        Session.Add("picture", imageBytes);
                    }
                  
                }
                else
                {
                    string FilePath = System.Web.VirtualPathUtility.ToAbsolute("~/Images/TUOwls_logo.png");
                    FileStream fs = new FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    imageBytes = new byte[fs.Length];
                    fs.Read(imageBytes, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    MemoryStream ms = new MemoryStream();
                    Session.Add("picture", imageBytes);
                }              

                Expert newExpert = new Expert
                {
                    tuID = (string)Session["TU_ID"],
                    username = txtUsername.Text,
                    firstName = txtFirstName.Text,
                    lastName = txtLastName.Text,
                    college = (string)Session["School"],
                    major = (string)Session["Major_1"],
                    email = txtEmail.Text,
                    dateJoined = DateTime.Now,
                    phoneNumber = txtPhoneNumber.Text,
                    aboutMe = aboutMe.Text,
                    linkedIn = txtLinkedIn.Text,
                    picture = (byte[])Session["picture"],
                    isActive = true,
                    skillGroupID = Int32.Parse(SkillGroupDropdown.SelectedValue),
                    genderID = Int32.Parse(genderDropdown.SelectedValue),
                    ethnicityID = Int32.Parse(ethnicityDropdown.SelectedValue),
                    lastUpdateDate = DateTime.Now,
                    lastUpdateUser = txtLastName.Text + ", " + txtFirstName.Text
                };

                DbMethods dbmethods = new DbMethods();
                SessionMethods sessionMethodsObj = new SessionMethods();
                int result = dbmethods.CreateExpert(newExpert);
                
                sessionMethodsObj.storeExpertDataInSession();
                Response.Redirect("ExpertPage.aspx?username=" + newExpert.username);
                //Response.Write(result);
            }
        }
    }
}