using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapstoneBlackstone.C_SharpClasses;

namespace CapstoneBlackstone
{
    public partial class EditExpertProfile : System.Web.UI.Page
    {
        DbMethods DbMethods = new DbMethods();
        TempleUser.StudentObj studentObj = new TempleUser.StudentObj();
        Validation vlad = new Validation();
        AutoComplete ac = new AutoComplete();//AJAX AutoComplete for AddSkill TextBox

        protected void Page_Load(object sender, EventArgs e)
        {
            studentObj.tuid = "912884978";//TEMP value until CreateExpert Page is done.
            FillControls();
        }

        public void FillControls()
        {


            DataSet dsAllExpertDeleteSkills = DbMethods.GetExpertsSkills(studentObj.tuid);
            //bind Experts Skills to Delete Gridview
            if (vlad.IsDataSetEmpty(dsAllExpertDeleteSkills) == false)
                lblDeleteTest.Text = "your DataSet is EMPTY.";
            else
            {
                gvDeleteSkills.DataSource = dsAllExpertDeleteSkills.Tables[0];
                gvDeleteSkills.DataBind();//bind
                lblDeleteTest.Text = "your method returned data! :)";
            }
        }//end fill controls

        protected void btnDeleteSkill_Click(object sender, EventArgs e)
        {
            List<int> skillIdList = new List<int>();

            foreach (GridViewRow row in gvDeleteSkills.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked)
                    {
                        skillIdList.Add(Convert.ToInt32(row.Cells[2]));
                    }
                }
            }
            foreach (var sId in skillIdList)
            {
                int result = DbMethods.DeleteExpertSkill(studentObj.tuid, sId);//passing a long string of SkillIds into the method

                if (result != -1)
                    lblDeleteTest.Text = "Success! New Skill was added to Skills and Expert_Skills Tables.";
                else
                    lblDeleteTest.Text = "Error: Process did not execute successfully. :(";
            }

            //string idString = string.Join(",", skillIdList.Select(n => n.ToString()).ToArray());
        }//end delete button

        protected void BtnSubmitNewSkill_Click(object sender, EventArgs e)
        {
            int txtWordCount = 0;
            Int32.TryParse(txtAddNewSkill.Text, out txtWordCount);

            if (vlad.IsBlank(txtAddNewSkill.Text) == true)
            {
                lblAddSkillTest.Text = "You did not add anything to the text box.";
            }
            else if (vlad.IsText(txtAddNewSkill) && vlad.ValidateWordCount(txtAddNewSkill.ToString(), txtWordCount) == true)
            {
                //if complete, submit skill to DB
                int result = DbMethods.AddNewSkillToUser(studentObj.tuid, txtAddNewSkill.Text);
                if (result != -1)
                    lblAddSkillTest.Text = "Success! New Skill was added to Skills and Expert_Skills Tables.";
                else
                    lblAddSkillTest.Text = "Error: Process did not execute successfully. :(";
            }
            else
                lblAddSkillTest.Text = "Error: Invalid input.";
        }


        /*
        protected void ddlSkills_SelectedIndexChanged(object sender, EventArgs e)
        {

            //get skill id from ddlSkills
            int newSkill = Convert.ToInt32(ddlSkills.SelectedValue);
            //add skill to profile (cause postBack)
            int result = DbMethods.AddSkillToUser(studentObj.tuid, newSkill);
            if (result != -1)
                lblAddSkillTest.Text = "Success! New Skill was added to Skills and Expert_Skills Tables.";
            else
                lblAddSkillTest.Text = "Error: Process did not execute successfully. :(";

        }*/

    }//end Class
}//end nameSpace