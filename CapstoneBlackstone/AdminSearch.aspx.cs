using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneBlackstone.C_SharpClasses;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Connection;
using CapstoneBlackstone.Models;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CapstoneBlackstone
{
    public partial class AdminSearch : System.Web.UI.Page
    {
        Data d = new Data();
        Venture venture = new Venture();
        GvSearchObj gvSearchObj = new GvSearchObj();
        VentureSearchObj ventureSearchObj = new VentureSearchObj();
        DbMethods DbMethods = new DbMethods();
        Validation vlad = new Validation();
        List<GvSearchObj> SelectedSkillsList = new List<GvSearchObj>();
        List<VentureSearchObj> distinctTuIds = new List<VentureSearchObj>();
        SessionMethods sm = new SessionMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null)
            {
                Response.Redirect("default.aspx");
            }
            else if (Session["AdminToken"] == null)
            {
                Response.Redirect("ExpertPage.aspx");
            }
            else if (!IsPostBack)
                FillControls();

        }

        public void FillControls()
        {
            DataSet allSGNs = DbMethods.GetAllSkillGroupNames();//get dataSet from DB

            if (vlad.IsDataSetEmpty(allSGNs) == true)
                lblSearchError.Text = "your DataSet is EMPTY.";
            else
            {
                lblSearchError.Text = " ";

                int numberOfRows = allSGNs.Tables[0].Rows.Count;
                DataTable SkillGroup = allSGNs.Tables[0];
                DataRow addingNewRow = SkillGroup.NewRow();
                addingNewRow["SkillGroupID"] = "0";//"all Skill Groups has a value of 0 so I know to SELECT ALL when doing a search
                addingNewRow["SkillGroupName"] = "All Skill Groups";
                addingNewRow["LastUpdateDate"] = "2017-10-15 22:15:00.000";
                addingNewRow["LastUpdateUser"] = "John Willy Will";

                //add all skills option to dataTable
                SkillGroup.Rows.Add(addingNewRow);
                SkillGroup.AcceptChanges();

                ddlSkillGroup.DataSource = SkillGroup;
                ddlSkillGroup.DataTextField = "SkillGroupName";//shows the name each skill group
                ddlSkillGroup.DataValueField = "SkillGroupID";//each name  has a value of it's ID
                ddlSkillGroup.DataBind();

                Session.Add("SkillGroup", SkillGroup);//adding skillGroup table to session obj
            }
        }

        protected void ddlSkillGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clear both repeaters
            rpVentureSearchResults.DataSource = null;
            rpExpertSearchResults.DataSource = null;
            rpVentureSearchResults.DataBind();
            rpExpertSearchResults.DataBind();

            ShowSkills(ddlSkillGroup.SelectedValue);
        }

        public void ShowSkills(string skillGroupID)
        {
            //get skillNames associated with provided SkillGroupID
            int sgid = Convert.ToInt32(skillGroupID);

            //do error checkng later 10/16/17 1:48 am
            if (sgid != 0)
            {
                DataSet dsSkillNamesById = DbMethods.GetSkillNamesBasedOnSkillGroupId(sgid);
                DataTable dtSkillNamesById = dsSkillNamesById.Tables[0];

                gvShowSkillsBasedOnDdl.DataSource = dtSkillNamesById;
                gvShowSkillsBasedOnDdl.DataBind();

                if (vlad.IsDataSetEmpty(dsSkillNamesById) == false)
                    Session["Skills"] = dsSkillNamesById;//for the GridView Paging
                else
                    lblSearchError.Text = "no records to return :(";
            }
            else
            {//get all skills
                DataSet dsAllSkillNames = DbMethods.GetSkillNamesOwnedByAllTuids();
                DataTable dtAllSkillNames = dsAllSkillNames.Tables[0];
                gvShowSkillsBasedOnDdl.DataSource = dtAllSkillNames;
                gvShowSkillsBasedOnDdl.DataBind();

                if (vlad.IsDataSetEmpty(dsAllSkillNames) == false)
                    Session["Skills"] = dsAllSkillNames;//for the GridView Paging
                else
                    lblSearchError.Text = "no records to return :(";
            }
        }//end ShowSkills

        protected void gvShowSkillsBasedOnDdl_PageIndexChanging(Object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            SelectSkills();
            // Set the GridView to display the correct page
            gvShowSkillsBasedOnDdl.PageIndex = e.NewPageIndex;
            DataSet myDS = (DataSet)Session["Skills"];//all skills from DB call based on skillsGroup to search by
            DataTable dt = myDS.Tables[0];
            gvShowSkillsBasedOnDdl.DataSource = dt;
            gvShowSkillsBasedOnDdl.DataBind();
        }

        public List<GvSearchObj> SelectSkills()
        {
            for (int i = 0; i < gvShowSkillsBasedOnDdl.Rows.Count; i++)
            {
                CheckBox chk = gvShowSkillsBasedOnDdl.Rows[i].FindControl("chkSelect") as CheckBox;
                if (chk.Checked)
                {
                    //add selected skillName and add it's other fields too
                    SelectedSkillsList.Add(new GvSearchObj()
                    {
                        SkillName = gvShowSkillsBasedOnDdl.Rows[i].Cells[1].Text
                    });
                    //save what you got from the page that was being viewed before click event
                    Session["SelectedSkillsList"] = SelectedSkillsList;
                }
            }
            return SelectedSkillsList;
        }

        public void ClearControls()
        {
            gvShowSkillsBasedOnDdl.AllowPaging = false;
            gvShowSkillsBasedOnDdl.DataBind();
            // You can select some checkboxex on gridview over here..
            foreach (GridViewRow row in gvShowSkillsBasedOnDdl.Rows)
            {
                var cb = row.FindControl("chkSelect") as CheckBox;
                if (cb != null)
                    cb.Checked = false;

            }
            gvShowSkillsBasedOnDdl.AllowPaging = true;
            gvShowSkillsBasedOnDdl.DataBind();
            ddlSkillGroup.SelectedIndex = 0;//reset dropdown view
            SelectedSkillsList = null;//clear selected Skills for next search
            Session["SelectedSkillsList"] = SelectedSkillsList;
        }

        protected void Expert_Profile_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "ExpertProfileClick")
            {
                string userName = e.CommandArgument.ToString();
                //Do something
                Response.Redirect("ExpertPage.aspx?Username=" + userName);
            }
        }//end VentureSearchesExpert Repeater ClickEvent

        protected void Venture_Profile_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "VentureProfileClick")
            {
                string ventureName = e.CommandArgument.ToString();
                //Do something
                Response.Redirect("VenturePage.aspx?name=" + ventureName);
            }
        }//end VentureSearchesExpert Repeater ClickEvent


        protected void btnSearchExperts_Click(object sender, EventArgs e)//a venture company searches experts
        {
            Debug.WriteLine("Entering btnSearchExperts_");

            SelectedSkillsList = SelectSkills();
            List<VentureSearchObj> ventureSkillSearch = new List<VentureSearchObj>();
            List<VentureSearchObj> distinctTuIds = new List<VentureSearchObj>();
            DataSet dsVentureSkillSearchResults = new DataSet("VentureSkillSearchResults");
            int x = 0;
            lblSearchError.Text = " ";
            string txtTemp = txtFullName.Text.Replace(" ", String.Empty);

            //clear both repeaters
            rpVentureSearchResults.DataSource = null;
            rpExpertSearchResults.DataSource = null;
            rpVentureSearchResults.DataBind();
            rpExpertSearchResults.DataBind();

            if (!string.IsNullOrWhiteSpace(txtTemp) && Regex.IsMatch(txtTemp, "^[a-zA-Z]+$") == false)
            {
                lblSearchError.Text = "Illegal characters used. Please only use text in Search Experts by name.";
            }
            else if (SelectedSkillsList.Count == 0 && string.IsNullOrWhiteSpace(txtTemp))
            {
                lblSearchError.Text = "Please search by skills, expert name or both.";
            }
            else if (SelectedSkillsList.Count == 0 && !string.IsNullOrWhiteSpace(txtTemp))
            {//only get the expert with this name
             //we are searching for an expert that is close to the full name from the text box

                var names = txtFullName.Text.Split(' ');
                string firstName = "";
                string lastName = "";

                if (names.Length == 1)
                    firstName = names[0];
                else if (names.Length > 2)
                {
                    firstName = names[0];
                    lastName = names[1];
                }

                DataTable results = DbMethods.SearchExpertsByName(firstName, lastName);
                distinctTuIds = Data.CreateListFromTable<VentureSearchObj>(results);

                foreach (VentureSearchObj expert in distinctTuIds)
                {
                    if (expert.Picture != null)
                    {
                        expert.image = d.ConvertToImage((byte[])expert.Picture);//returns string url 
                    }
                    else
                    {//in case expert's profile pic is null
                        byte[] imageBytes;
                        MemoryStream ms = new MemoryStream();
                        System.Drawing.Image img = System.Drawing.Image.FromFile("Images/TUOwls_logo.png");
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        imageBytes = ms.ToArray();
                        expert.image = d.ConvertToImage(imageBytes);
                    }
                }

            }
            else
            {//search experts by skills and expert name
                int skillGroupId = Convert.ToInt32(ddlSkillGroup.SelectedValue);
                if (skillGroupId == 0)//use a modified version of searchVenture that accomodats SkillGroupId of 0
                {
                    //The Search Portion of the CLick Event
                    foreach (GvSearchObj gvObj in SelectedSkillsList)
                    {
                        DataSet results = DbMethods.SearchExpertsALG(gvObj.SkillName);//get search result of all experts that match the searched skillName
                        if (results.Tables.Count == 0)
                            break;
                        else
                        {
                            DataTable resultTable = results.Tables[0].Copy();
                            resultTable.TableName = "Table  " + x;
                            resultTable.AcceptChanges();
                            dsVentureSkillSearchResults.Tables.Add(resultTable);//add search results of one skill to DataSet as DataTable
                            x++;
                        }
                    }//END of the Search Portion of the CLick Event
                }
                else
                {
                    foreach (GvSearchObj gvObj in SelectedSkillsList)
                    {
                        DataSet results = DbMethods.SearchExperts(gvObj.SkillName, skillGroupId);//get search result of all experts that match the searched skillName
                        if (results.Tables.Count == 0)
                            break;
                        else
                        {
                            DataTable resultTable = results.Tables[0].Copy();
                            resultTable.TableName = "Table  " + x;
                            resultTable.AcceptChanges();
                            dsVentureSkillSearchResults.Tables.Add(resultTable);//add search results of one skill to DataSet as DataTable
                            x++;
                        }
                    }//END of the Search Portion of the CLick Event
                }
                if (dsVentureSkillSearchResults.Tables.Count == 0)
                {
                    lblSearchError.Text = "Your search query yeilded no results.";
                }
                else
                {
                    //Add all tables into one table
                    for (int z = 0; z < dsVentureSkillSearchResults.Tables.Count; z++)
                    {
                        dsVentureSkillSearchResults.Tables[0].Merge(dsVentureSkillSearchResults.Tables[z]);
                    }
                    //add to Mega List from first table in ds, b/c that has all merged tables
                    ventureSkillSearch = Data.CreateListFromTable<VentureSearchObj>(dsVentureSkillSearchResults.Tables[0]);

                    var temp = ventureSkillSearch.Select(o => o.TUID).Distinct();//get all unique tuids
                    foreach (var i in temp)
                    {//add unique tuids to a new list
                        distinctTuIds.Add(new VentureSearchObj { TUID = i.ToString() });//only contains TUIDs
                    }

                    foreach (VentureSearchObj dtuid in distinctTuIds)
                    {//loading each unique tuid with their skills into their object
                        var newList = (from n in ventureSkillSearch where n.TUID.Equals(dtuid.TUID, StringComparison.OrdinalIgnoreCase) select n.SkillName).Distinct();

                        DataTable currentTUID = DbMethods.GetExpertInfo(dtuid.TUID).Tables[0];//get that tuid's info and add all necessary data to the current object
                        dtuid.Username = currentTUID.Rows[0][1].ToString();
                        dtuid.LastName = currentTUID.Rows[0][2].ToString();
                        dtuid.FirstName = currentTUID.Rows[0][3].ToString();
                        dtuid.Email = currentTUID.Rows[0][4].ToString();

                        if (!Convert.IsDBNull(currentTUID.Rows[0][11]))
                        {
                            dtuid.image = d.ConvertToImage((byte[])currentTUID.Rows[0][11]);//returns string url 
                        }
                        else
                        {//in case expert's profile pic is null
                            byte[] imageBytes;
                            MemoryStream ms = new MemoryStream();
                            System.Drawing.Image img = System.Drawing.Image.FromFile("TUOwls_logo.png");
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            imageBytes = ms.ToArray();
                            dtuid.image = d.ConvertToImage(imageBytes);
                        }

                        foreach (var j in newList)
                        {//add all skillNames associated with the distinct tuid into its SkillName list in VentureSearch obj
                            dtuid.AllExpertSkills.Add(j.ToString());
                            dtuid.cdsAllExpertSkills += j.ToString() + " ";
                        }
                    }

                    //The Comparison Portion of the CLick Event
                    using (var gvSO = SelectedSkillsList.GetEnumerator())
                    using (var dtuid = distinctTuIds.GetEnumerator())
                    {
                        while (dtuid.MoveNext())
                        {
                            var dtuidItem = dtuid.Current;
                            while (gvSO.MoveNext())
                            {
                                var gvSoItem = gvSO.Current;

                                foreach (var k in dtuidItem.AllExpertSkills)
                                {
                                    if (k.ToString().Equals(gvSoItem.SkillName, StringComparison.OrdinalIgnoreCase))
                                        dtuidItem.ExpertRank = dtuidItem.ExpertRank + 1;
                                }
                            }
                        }
                    }//END of the Comparison Portion of the CLick Event
                }
            }//end else just searching by name 

            if (distinctTuIds.Count != 0)
            {
                List<VentureSearchObj> SortedList = distinctTuIds.OrderBy(o => o.ExpertRank).ToList();
                this.rpExpertSearchResults.DataSource = SortedList;
                this.rpExpertSearchResults.DataBind();
            }
            ClearControls();
            txtFullName.Text = string.Empty;//clear name textxBox
        }//end btnSearchExperts_Click

        public List<ExpertSearchObj> getVentureSkillDuplicatesAndData(List<ExpertSearchObj> expertSkillSearch)
        {
            List<ExpertSearchObj> distinctVentureIds = new List<ExpertSearchObj>();//distinct ventures with all their skills

            var temp = expertSkillSearch.Select(o => o.VentureID).Distinct();//get all unique tuids
            foreach (var i in temp)
            {//add unique tuids to a new list
                distinctVentureIds.Add(new ExpertSearchObj { VentureID = i });//only contains venture names
            }

            foreach (ExpertSearchObj dvid in distinctVentureIds)
            {//get all duplicates on put them into distinct objects
                var newList = (from n in expertSkillSearch where n.VentureID == dvid.VentureID select new { n.SkillName, n.VentureID, n.Name, n.Picture }).Distinct();//get all data

                //add venture data to each object
                Venture currentVenture = DbMethods.GetVenture(dvid.VentureID);//get that venture's data and load that into it's object
                dvid.VentureID = currentVenture.ventureID;
                dvid.Name = currentVenture.name;
                dvid.VentureDesc = currentVenture.description;
                if (currentVenture.Picture != null)
                {
                    dvid.image = d.ConvertToImage((byte[])currentVenture.Picture);//returns string url 
                }
                else
                {//in case expert's profile pic is null
                    byte[] imageBytes;
                    MemoryStream ms = new MemoryStream();
                    System.Drawing.Image img = System.Drawing.Image.FromFile("Images/TUOwls_logo.png");
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    imageBytes = ms.ToArray();
                    dvid.image = d.ConvertToImage(imageBytes);
                }
                //dvid.SkillName = currentVenture.skillName;
                foreach (var j in newList)
                {//add all skillNames associated with the distinct tuid into its SkillName list in VentureSearch obj
                    dvid.AllVentureSkills.Add(j.ToString());
                }
            }//end getting distinct ventures and adding data
            return distinctVentureIds;
        }

        protected void btnSearchVentures_Click(object sender, EventArgs e)
        {
            SelectedSkillsList = SelectSkills();
            Debug.WriteLine("Entering btnSearchVentures");
            List<GvSearchObj> list = Session["SelectedSkillsList"] as List<GvSearchObj>;
            lblSearchError.Text = " ";
            List<ExpertSearchObj> expertVentureNameSearch = new List<ExpertSearchObj>();//all ventures searched by their names
            List<ExpertSearchObj> expertVentureDescSearch = new List<ExpertSearchObj>();//all ventures searched by their descriptions
            List<ExpertSearchObj> expertSkillSearch = new List<ExpertSearchObj>();//skills
            List<ExpertSearchObj> distinctVentureIds = new List<ExpertSearchObj>();//distinct ventures with all their skills
            DataSet dsExpertSkillSearchResults = new DataSet("ExpertSkillSearchResults");
            lblSearchError.Text = " ";
            int x = 0;
            string txtTemp = txtPrefSearchVentureDesc.Text.Replace(" ", String.Empty);

            //clear both repeaters
            rpVentureSearchResults.DataSource = null;
            rpExpertSearchResults.DataSource = null;
            rpVentureSearchResults.DataBind();
            rpExpertSearchResults.DataBind();

            if (list == null && string.IsNullOrWhiteSpace(txtPrefSearchVentureDesc.Text))
            {//no, no
                lblSearchError.Text = "You must select some skills, enter a venture name, and/or enter a venture description to search for ventures.";
            }
            else if (!string.IsNullOrWhiteSpace(txtTemp) && Regex.IsMatch(txtTemp, "^[a-zA-Z]+$") == false)
            {
                lblSearchError.Text = "Illegal characters used. Please only use text in Search Ventures by description.";
            }
            else if (list != null && string.IsNullOrWhiteSpace(txtPrefSearchVentureDesc.Text))
            {//yes, no
             //search only by skills

                foreach (GvSearchObj gvObj in SelectedSkillsList)//get data from search parameter selectedSkills
                {
                    DataSet results = DbMethods.SearchVentures(gvObj.SkillName);//get search result of all experts that match the searched skillName
                    if (results.Tables.Count == 0)
                        break;
                    else
                    {
                        DataTable resultTable = results.Tables[0].Copy();
                        resultTable.TableName = "Table  " + x;
                        resultTable.AcceptChanges();
                        dsExpertSkillSearchResults.Tables.Add(resultTable);//add search results of one skill to DataSet as DataTable
                        x++;
                    }
                }//END of the Search Portion of the CLick Event

                //put that data into a list; Add all tables into one table
                for (int z = 0; z < dsExpertSkillSearchResults.Tables.Count; z++)
                {
                    dsExpertSkillSearchResults.Tables[0].Merge(dsExpertSkillSearchResults.Tables[z]);
                }
                //add to Mega List from first table in ds, b/c that has all merged tables
                expertSkillSearch = Data.CreateListFromTable<ExpertSearchObj>(dsExpertSkillSearchResults.Tables[0]);

                distinctVentureIds = getVentureSkillDuplicatesAndData(expertSkillSearch);

                //The Comparison Portion of the CLick Event
                using (var gvSO = SelectedSkillsList.GetEnumerator())
                using (var dtuid = distinctVentureIds.GetEnumerator())
                {//start search
                    while (dtuid.MoveNext())
                    {
                        var dtuidItem = dtuid.Current;
                        while (gvSO.MoveNext())
                        {
                            var gvSoItem = gvSO.Current;

                            foreach (var k in dtuidItem.AllVentureSkills)
                            {
                                if (k.ToString().Equals(gvSoItem.SkillName, StringComparison.OrdinalIgnoreCase))
                                    dtuidItem.VentureRank = dtuidItem.VentureRank + 1;
                            }
                        }
                    }
                }//end search 
            }//end else if
            else if (list == null && !string.IsNullOrWhiteSpace(txtPrefSearchVentureDesc.Text))
            {//no, yes
                if (!Regex.IsMatch(txtPrefSearchVentureDesc.Text, "^[a-zA-Z]", RegexOptions.IgnorePatternWhitespace))
                {
                    lblSearchError.Text = "invalid input: Please enter only letters when searching by Venture Description.";
                }
                else
                {
                    DataTable searchByVentureDescriptionResults = DbMethods.SearchVenturesBasedOnDesc(txtPrefSearchVentureDesc.Text).Tables[0];
                    if (searchByVentureDescriptionResults.Rows.Count == 0)
                    {
                        lblSearchError.Text = "Your search parameters didn't produce any results.";
                    }
                    else
                    {
                        distinctVentureIds = Data.CreateListFromTable<ExpertSearchObj>(searchByVentureDescriptionResults);

                        foreach (ExpertSearchObj ex in distinctVentureIds)
                        {
                            if (ex.Picture != null)
                            {
                                ex.image = d.ConvertToImage((byte[])ex.Picture);//returns string url 
                            }
                            else
                            {//in case expert's profile pic is null
                                byte[] imageBytes;
                                MemoryStream ms = new MemoryStream();
                                System.Drawing.Image img = System.Drawing.Image.FromFile("Images/TUOwls_logo.png");
                                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                imageBytes = ms.ToArray();
                                ex.image = d.ConvertToImage(imageBytes);
                            }
                        }
                        //The Comparison Portion of the CLick Event
                        using (var dtuid = distinctVentureIds.GetEnumerator())
                        {//start search
                            while (dtuid.MoveNext())
                            {
                                var dtuidItem = dtuid.Current;
                                if (dtuidItem.VentureDesc.Contains(txtPrefSearchVentureDesc.Text))
                                    dtuidItem.VentureRank = dtuidItem.VentureRank + 1;
                            }
                        }//end search
                    }
                }
            }
            else if (list != null && !string.IsNullOrWhiteSpace(txtPrefSearchVentureDesc.Text))
            {//yes, yes
                foreach (GvSearchObj gvObj in SelectedSkillsList)//get data from search parameter selectedSkills
                {
                    DataSet results = DbMethods.SearchVentures(gvObj.SkillName);//get search result of all experts that match the searched skillName
                    if (results.Tables.Count == 0)
                        break;
                    else
                    {
                        DataTable resultTable = results.Tables[0].Copy();
                        resultTable.TableName = "Table  " + x;
                        resultTable.AcceptChanges();
                        dsExpertSkillSearchResults.Tables.Add(resultTable);//add search results of one skill to DataSet as DataTable
                        x++;
                    }
                }//END of the Search Portion of the CLick Event

                //put that data into a list; Add all tables into one table
                for (int z = 0; z < dsExpertSkillSearchResults.Tables.Count; z++)
                {
                    dsExpertSkillSearchResults.Tables[0].Merge(dsExpertSkillSearchResults.Tables[z]);
                }
                //add to Mega List from first table in ds, b/c that has all merged tables
                expertSkillSearch = Data.CreateListFromTable<ExpertSearchObj>(dsExpertSkillSearchResults.Tables[0]);

                distinctVentureIds = getVentureSkillDuplicatesAndData(expertSkillSearch);

                //The Comparison Portion of the CLick Event
                using (var gvSO = SelectedSkillsList.GetEnumerator())
                using (var dtuid = distinctVentureIds.GetEnumerator())
                {//start search
                    while (dtuid.MoveNext())
                    {
                        var dtuidItem = dtuid.Current;
                        while (gvSO.MoveNext())
                        {
                            var gvSoItem = gvSO.Current;

                            foreach (var k in dtuidItem.AllVentureSkills)
                            {
                                if (k.ToString().Equals(gvSoItem.SkillName, StringComparison.OrdinalIgnoreCase))
                                    dtuidItem.VentureRank = dtuidItem.VentureRank + 1;
                            }
                        }
                    }
                }//end search
            }

            if (distinctVentureIds.Count != 0)
            {
                List<ExpertSearchObj> SortedList = distinctVentureIds.OrderBy(o => o.VentureRank).ToList();
                this.rpVentureSearchResults.DataSource = SortedList;
                this.rpVentureSearchResults.DataBind();
            }

            ClearControls();
            txtPrefSearchVentureDesc.Text = string.Empty;
        }//end Expert Searches Ventures click event
    }
}