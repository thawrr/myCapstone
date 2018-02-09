using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using Connection;
using CapstoneBlackstone.C_SharpClasses;
using System.Diagnostics;

namespace CapstoneBlackstone
{
    public class DbMethods
    {
        SqlCommand objCommand = new SqlCommand();
        Skills skillz = new Skills();
        SkillGroup sg = new SkillGroup();

        public DataSet GetExpertVentures(string TUID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllExpertVentures";
            objCommand.Parameters.Clear();

            DataSet myDS = objDB.GetDataSetUsingCmdObj(objCommand);

          //  gvExpertsVentures.DataSource = myDS;
          //  gvExpertsVentures.DataBind();
            return myDS;
        }
       
        public List<Skills> GetSkills()
        {
            DataSet ds = new DataSet();
            List<Skills> AllExpertSkills = new List<Skills>();
            
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllSkills";
            objCommand.Parameters.Clear();
            
            ds = objDB.GetDataSetUsingCmdObj(objCommand);//contains all skillIds and SkillNames for the specific expert
            AllExpertSkills = Data.CreateListFromTable<Skills>(ds.Tables[0]);

            return AllExpertSkills;
        }

        public DataSet GetExpertsSkills(string TUID)
        {//for ExpertPage.aspx repeater

            DBConnect connection = new DBConnect();
            DataSet ds = new DataSet();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllExpertSkillsByTuId";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@tuId", TUID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            ds = connection.GetDataSetUsingCmdObj(objCommand);//contains all skillIds and SkillNames for the specific expert
            return ds;
        }//end GetExpertsSkills

        public string GetTUIDByUsername(string username)
        {
            DBConnect objDB = new DBConnect();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetTUIDByUsername";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@username", username);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            DataSet results = objDB.GetDataSetUsingCmdObj(objCommand);
            return (string) results.Tables[0].Rows[0][0];
        }

        public int AddSkillToUser(string TUID, int newSkill)
        {//click event in ddlAddSkill

            DBConnect objDB = new DBConnect();
            DataSet ds = new DataSet();
            //Update Skill_Expert with TUID and the Skill ID ONLY IF it EXCISTS
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddSkill";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@tuid", TUID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 18;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@skillId", newSkill);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            objCommand.Parameters.Add(inputParameter);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);
            Debug.WriteLine("Entering AddSkillToUser after DB call");

            return result;
        }

        public int AddNewSkillToUser(string TUID, string brandNewSkill)
        {//click event for add btn. add brand new skill to system and user.

            //adds new skill to Skills Table, gets back newly created SkillId,
            //then calls AddSkill Stored Procedure and adds the skill to the Expert's Profile.

            DBConnect objDB = new DBConnect();
            DataSet ds = new DataSet();
       
            //Update Skill_Expert with TUID and the Skill ID ONLY IF it EXCISTS
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddNewUniqueSkill";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@tuid", TUID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 19;
            objCommand.Parameters.Add(inputParameter);

            //pass in TUID and Skill name into method
            //add skill name to Skills Table and get new skill ID 
            //add the SkillId and TuId to Skill_Expert Table
            //return a value indicating if the whole process was successful
            inputParameter = new SqlParameter("@brandNewSkill", brandNewSkill);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 100;
            objCommand.Parameters.Add(inputParameter);

            int newSkillId = objDB.DoUpdateUsingCmdObj(objCommand);//got new skill ID
            Debug.WriteLine("Entering AddNewSkillToUser after DB call");

            return newSkillId;
            //int result = AddSkillToUser(TUID, newSkillId);//send new skillID and tuID to be added to Skill_Expert Profile
            //return result;
        }

        public int DeleteExpertSkill(string TUID, int idString)
        {

            DBConnect connection = new DBConnect();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteExpertSkillById";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@tuId", TUID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@skillId", idString);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            int result = connection.DoUpdateUsingCmdObj(objCommand);//contains all skillIds and SkillNames for the specific expert
            return result;
        }

        public DataSet GetExpertsSkillsBySkillName(string searchSkillName)
        {//for ExpertPage.aspx repeater

            DBConnect objDB = new DBConnect();
            DataSet ds = new DataSet();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetSkillNamesBySkillName";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@searchSN", searchSkillName);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 200;
            objCommand.Parameters.Add(inputParameter);

            ds = objDB.GetDataSetUsingCmdObj(objCommand);//contains all skillIds and SkillNames for the specific expert
            return ds;
        }

        public DataSet GetSkillNamesBasedOnSkillGroupId(int sgid)
        {
            DBConnect objDB = new DBConnect();
            DataSet ds = new DataSet();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetSkillNamesbySGID";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@sgid", sgid);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            inputParameter.Size = 200;
            objCommand.Parameters.Add(inputParameter);

            ds = objDB.GetDataSetUsingCmdObj(objCommand);//contains all skillIds and SkillNames for the specific expert
            return ds;
        }

        
        
        public DataSet GetSkillNamesOwnedByAllTuids()
        {
            DBConnect objDB = new DBConnect();
            DataSet ds = new DataSet();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetSkillNamesPossessedByExperts";
            objCommand.Parameters.Clear();

            ds = objDB.GetDataSetUsingCmdObj(objCommand);//contains all skillIds and SkillNames for the specific expert
            return ds;
        }


        private List<Param> BuildUpdateExpertParams(Expert e, string tuid)
        {
            List<Param> p = new List<Param>();
            p.Add(new Param("@firstName", e.firstName, SqlDbType.VarChar));            
            p.Add(new Param("@lastName", e.lastName, SqlDbType.VarChar));
            p.Add(new Param("@username", e.username, SqlDbType.VarChar));
            p.Add(new Param("@email", e.email, SqlDbType.VarChar));
            p.Add(new Param("@phoneNumber", e.phoneNumber, SqlDbType.VarChar));            
            p.Add(new Param("@linkedIn", e.linkedIn, SqlDbType.VarChar));
            p.Add(new Param("@aboutMe", e.aboutMe, SqlDbType.VarChar));
            p.Add(new Param("@tuid", tuid, SqlDbType.Int));
            p.Add(new Param("@picture", e.picture, SqlDbType.VarBinary));
            p.Add(new Param("@skillGroupID", e.skillGroupID, SqlDbType.Int));
            return p;
        }

        public int UpdateExpert (Expert e, string tuid)
        {
            DBConnect objDB = new DBConnect();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateExpert";
            objCommand.Parameters.Clear();

            List<Param> p = BuildUpdateExpertParams(e, tuid);

            foreach (Param param in p)
            {
                SqlParameter inputParameter = new SqlParameter(param.Name, param.Val);
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = param.Type;
                objCommand.Parameters.Add(inputParameter);
            }

            int result = objDB.DoUpdateUsingCmdObj(objCommand);
            return result;
        }

        private List<Param> BuildCreateExpertParams(Expert e)
        {
            List<Param> p = new List<Param>();
            p.Add(new Param("@tuid", e.tuID, SqlDbType.Int));
            p.Add(new Param("@username", e.username, SqlDbType.VarChar));
            p.Add(new Param("@lastName", e.lastName, SqlDbType.VarChar));
            p.Add(new Param("@firstName", e.firstName, SqlDbType.VarChar));
            p.Add(new Param("@college", e.college, SqlDbType.VarChar));
            p.Add(new Param("@major", e.major, SqlDbType.VarChar));
            p.Add(new Param("@email", e.email, SqlDbType.VarChar));
            p.Add(new Param("@dateJoined", e.dateJoined, SqlDbType.DateTime));
            p.Add(new Param("@phoneNumber", e.phoneNumber, SqlDbType.VarChar));
            p.Add(new Param("@aboutMe", e.aboutMe, SqlDbType.VarChar));
            p.Add(new Param("@linkedIn", e.linkedIn, SqlDbType.VarChar));
            p.Add(new Param("@picture", e.picture, SqlDbType.VarBinary));
            p.Add(new Param("@isActive", e.isActive, SqlDbType.Bit));
            p.Add(new Param("@skillGroupID", e.skillGroupID, SqlDbType.Int));
            p.Add(new Param("@genderID", e.genderID, SqlDbType.Int));
            p.Add(new Param("@ethnicityID", e.ethnicityID, SqlDbType.Int));
            p.Add(new Param("@lastUpdate", e.lastUpdateDate, SqlDbType.DateTime));
            p.Add(new Param("@lastUpdateUser", e.lastUpdateUser, SqlDbType.VarChar));
            return p;
        }

        private List<Param> BuildCreateVentureParams(Venture v)
        {
            List<Param> p = new List<Param>();
            p.Add(new Param("@Name", v.name, SqlDbType.VarChar));
            p.Add(new Param("@Description", v.description, SqlDbType.VarChar));
            p.Add(new Param("@AboutUs", v.aboutUs, SqlDbType.VarChar));
            p.Add(new Param("@ContactEmail", v.contactEmail, SqlDbType.VarChar));
            p.Add(new Param("@ContactPhoneNumber", v.contactPhoneNumber, SqlDbType.VarChar));
            p.Add(new Param("@ContactLinkedIn", v.contactLinkedIn, SqlDbType.VarChar));
            p.Add(new Param("@Picture", v.Picture, SqlDbType.VarBinary));
            p.Add(new Param("@PrimaryContactEmail", v.primaryContactEmail, SqlDbType.VarChar));
            p.Add(new Param("@IsActive", v.isActive, SqlDbType.Bit));
            p.Add(new Param("@LastUpdateDate", v.lastUpdateDate, SqlDbType.DateTime));
            p.Add(new Param("@LastUpdateUser", v.lastUpdateUser, SqlDbType.VarChar));
            return p;
        }

        public int CreateVenture(Venture v)
        {
            DBConnect objDB = new DBConnect();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CreateVenture";
            objCommand.Parameters.Clear();

            List<Param> p = BuildCreateVentureParams(v);

            foreach (Param param in p)
            {
                SqlParameter inputParameter = new SqlParameter(param.Name, param.Val);
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = param.Type;
                objCommand.Parameters.Add(inputParameter);
            }

            int result = objDB.DoUpdateUsingCmdObj(objCommand);
            return result;
        }

        public int CreateExpert(Expert e)
        {
            DBConnect objDB = new DBConnect();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CreateExpert";
            objCommand.Parameters.Clear();

            List<Param> p = BuildCreateExpertParams(e);

            foreach (Param param in p)
            {
                SqlParameter inputParameter = new SqlParameter(param.Name, param.Val);
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = param.Type;
                objCommand.Parameters.Add(inputParameter);
            }

            int result = objDB.DoUpdateUsingCmdObj(objCommand);
            return result;
        }

        public DataSet GetAllSkillGroups()
        {
            DBConnect objDB = new DBConnect();
            return objDB.GetDataSet("SELECT SkillGroupID, SkillGroupName FROM SkillGroup");
        }

        public DataSet GetAllEthnicities()
        {
            DBConnect objDB = new DBConnect();
            return objDB.GetDataSet("SELECT EthnicityID, EthnicityName FROM Ethnicity");
        }

        public DataSet GetExpertInfo(string TUID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllExpertInfo";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@TUID", TUID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            DataSet expertInfo = objDB.GetDataSetUsingCmdObj(objCommand);

            return expertInfo;
        }
        
        public DataSet GetAllGenders()
        {
            DBConnect objDB = new DBConnect();
            return objDB.GetDataSet("SELECT GenderID, GenderName FROM Gender");
        }
        
        public Object CheckIfExpertExists(string TUID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CheckIfExpertExists";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@TUID", TUID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 10;
            objCommand.Parameters.Add(inputParameter);

            Object count = objDB.ExecuteScalarFunction(objCommand);

            return count;
        }

        public void SetExpertIsActiveTrue()
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "SetExpertIsActiveTrue";
            objCommand.Parameters.Clear();

            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public Object GetSkillGroupName(int SkillGroupID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetSkillGroupName";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@SkillGroupID", SkillGroupID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            inputParameter.Size = 10;
            objCommand.Parameters.Add(inputParameter);

            Object skillGroupName = objDB.ExecuteScalarFunction(objCommand);

            return skillGroupName;
        }

        public Venture GetVenture(int ventureId)
        {
            Venture v = new Venture();

            DBConnect objdb = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetVentureById";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@ventureId", ventureId);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            objCommand.Parameters.Add(inputParameter);

            DataSet results = objdb.GetDataSetUsingCmdObj(objCommand);

            v.ventureID = (int) results.Tables[0].Rows[0][0];
            v.name = (string) results.Tables[0].Rows[0][1];
            v.description = (string) results.Tables[0].Rows[0][2];
            v.aboutUs = (string) results.Tables[0].Rows[0][3];
            v.contactEmail = (string) results.Tables[0].Rows[0][4];
            v.contactPhoneNumber = (string) results.Tables[0].Rows[0][5];
            v.contactLinkedIn = (string) results.Tables[0].Rows[0][6];
            v.Picture = (byte[]) results.Tables[0].Rows[0][7];
            v.primaryContactEmail = (string) results.Tables[0].Rows[0][8];
            v.isActive = (bool) results.Tables[0].Rows[0][9];
            v.lastUpdateDate = (DateTime) results.Tables[0].Rows[0][10];
            v.lastUpdateUser = (string) results.Tables[0].Rows[0][11];

            return v;
        }
        

        public DataSet GetExpertRoleAndVenture(string TUID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetExpertRoleAndVenture";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@TUID", TUID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 10;
            objCommand.Parameters.Add(inputParameter);

            DataSet roleAndVentureName = objDB.GetDataSetUsingCmdObj(objCommand);

            return roleAndVentureName;
        }

        public DataSet GetAllSkillGroupNames()
        {
            DBConnect objDB = new DBConnect();
            DataSet allSGNs = new DataSet();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllSkillGroupNames";
            objCommand.Parameters.Clear();
            
            allSGNs = objDB.GetDataSetUsingCmdObj(objCommand);
            return allSGNs;
        }

        public DataSet SearchVentures(string skillName)//experts searches ventures
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "SearchVenture";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@skillNames", skillName);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 300;
            objCommand.Parameters.Add(inputParameter);

            DataSet ventureInfo = objDB.GetDataSetUsingCmdObj(objCommand);

            return ventureInfo;
        }

        public DataSet SearchVenturesByName(string ventureName)//experts searches ventures
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "SearchVenturesByName";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@ventureName", ventureName);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 300;
            objCommand.Parameters.Add(inputParameter);

            DataSet ventureInfo = objDB.GetDataSetUsingCmdObj(objCommand);

            return ventureInfo;
        }

        public DataSet SearchVenturesBasedOnDesc(string ventureDesc)//experts searches ventures
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "SearchVenturesBasedOnDesc";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@ventureDesc", ventureDesc);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            objCommand.Parameters.Add(inputParameter);

            DataSet ventureInfo = objDB.GetDataSetUsingCmdObj(objCommand);

            return ventureInfo;
        }

        public DataSet SearchExperts(string skillName, int skillGroupId)//venture searches experts
        {//, string fn, string ln
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "SearchExpert";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@skillNames", skillName);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 300;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@skillGroupId", skillGroupId);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            DataSet expertInfo = objDB.GetDataSetUsingCmdObj(objCommand);

            return expertInfo;
        }
        
        public int AddNewSkillToVenture(int VentureID, string newSkill)
        {

            DBConnect objDB = new DBConnect();
            DataSet ds = new DataSet();


            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddNewUniqueSkillVenture";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@ventureID", VentureID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@newSkill", newSkill);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            objCommand.Parameters.Add(inputParameter);

            int newSkillId = objDB.DoUpdateUsingCmdObj(objCommand);
            Debug.WriteLine("Entering AddNewSkillToExpert");

            return newSkillId;
        }

        public int AddSkillToVenture(int VENTUREID, int newSkill)
        {

            DBConnect objDB = new DBConnect();
            DataSet ds = new DataSet();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddSkillVenture";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@ventureID", VENTUREID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@skillId", newSkill);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            objCommand.Parameters.Add(inputParameter);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);
            Debug.WriteLine("Entering AddSkillToVenture");

            return result;
        }


        public int DeleteVentureSkill(int VentureID, int skillID)
        {
            DBConnect connection = new DBConnect();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteVentureSkillById";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@ventureID", VentureID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@skillId", skillID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            objCommand.Parameters.Add(inputParameter);

            int result = connection.DoUpdateUsingCmdObj(objCommand);
            return result;
        }



        public void CreateVentureMember(string TUID, int ventureID, string role, DateTime lastUpdateDate, string lastUpdateUser)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "CreateVentureMember";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@TUID", TUID);
            objCommand.Parameters.AddWithValue("@VentureID", ventureID);
            objCommand.Parameters.AddWithValue("@Role", role);
            objCommand.Parameters.AddWithValue("@LastUpdateDate", lastUpdateDate);
            objCommand.Parameters.AddWithValue("@LastUpdateUser", lastUpdateUser);

            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public DataSet GetUsernamesForVenture(int ventureID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUsernamesForVenture";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@VentureID", ventureID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            inputParameter.Size = 10;
            objCommand.Parameters.Add(inputParameter);

            DataSet usernamesForVenture = objDB.GetDataSetUsingCmdObj(objCommand);

            return usernamesForVenture;
        }

        public Object GetExpertTUID(string email)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetExpertTUID";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@email", email);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            Object TUID = objDB.ExecuteScalarFunction(objCommand);

            return TUID;
        }

        public Object GetVentureID(string name)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetVentureID";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@name", name);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            Object ventureID = objDB.ExecuteScalarFunction(objCommand);

            return ventureID;
        }

        public void DeactivateVentureMember(int ventureID, string TUID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "DeactivateVentureMember";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@VentureID", ventureID);
            objCommand.Parameters.AddWithValue("@TUID", TUID);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public void CreateStaticMember(int ventureID, string firstName, string lastName, string role, DateTime lastUpdateDate, string lastUpdateUser)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "CreateStaticMember";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@VentureID", ventureID);
            objCommand.Parameters.AddWithValue("@FirstName", firstName);
            objCommand.Parameters.AddWithValue("@LastName", lastName);
            objCommand.Parameters.AddWithValue("@Role", role);
            objCommand.Parameters.AddWithValue("@LastUpdateDate", lastUpdateDate);
            objCommand.Parameters.AddWithValue("@LastUpdateUser", lastUpdateUser);

            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public DataSet GetAllStaticMembersByVentureID(int ventureID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllStaticMembersByVentureID";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@VentureID", ventureID);

            DataSet staticMembers = objDB.GetDataSetUsingCmdObj(objCommand);

            return staticMembers;
        }

        public void DeactivateStaticMemberByID(int staticMemberID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "DeactivateStaticMemberByID";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@StaticMemberID", staticMemberID);
           
            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public void DeactivateVenture(int ventureID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "DeactivateVenture";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@VentureID", ventureID);

            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public void DeactivateExpert(string tuID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "DeactivateExpert";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@TUID", tuID);

            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public DataSet CheckIfVentureMemberExists(int ventureID, string tuID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CheckIfVentureMemberExists";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@TUID", tuID);
            objCommand.Parameters.AddWithValue("@VentureID", ventureID);

            DataSet ventureMember = objDB.GetDataSetUsingCmdObj(objCommand);

            return ventureMember;
        }

        public void ReactivateVentureMember(string tuID, int ventureID, string role)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "ReactivateVentureMember";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@TUID", tuID);
            objCommand.Parameters.AddWithValue("@VentureID", ventureID);
            objCommand.Parameters.AddWithValue("@Role", role);

            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public DataSet SearchExpertsALG(string skillNames)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "SearchVenturesASG";
            objCommand.Parameters.Clear();
            
            SqlParameter inputParameter = new SqlParameter("@skillNames", skillNames);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            DataSet results = objDB.GetDataSetUsingCmdObj(objCommand);

            return results;
        }

        public DataSet GetVentureSkills(int VENTUREID)
        {

            DBConnect connection = new DBConnect();
            DataSet ds = new DataSet();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllVentureSkillsByVentureID";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@ventureID", VENTUREID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.Int;
            objCommand.Parameters.Add(inputParameter);

            ds = connection.GetDataSetUsingCmdObj(objCommand);
            return ds;
        }

        public DataSet GetAllVentureNamesByTuid(string tuID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllVentureNamesByTuid";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@tuId", tuID);

            DataSet ventureNames = objDB.GetDataSetUsingCmdObj(objCommand);

            return ventureNames;
        }

       private List<Param> BuildUpdateVentureParams(Venture v)
        {
             List<Param> p = new List<Param>();
            p.Add(new Param("@Name", v.name, SqlDbType.VarChar));
            p.Add(new Param("@Description", v.description, SqlDbType.VarChar));
            p.Add(new Param("@AboutUs", v.aboutUs, SqlDbType.VarChar));
            p.Add(new Param("@ContactEmail", v.contactEmail, SqlDbType.VarChar));
            p.Add(new Param("@ContactPhoneNumber", v.contactPhoneNumber, SqlDbType.VarChar));
            p.Add(new Param("@ContactLinkedIn", v.contactLinkedIn, SqlDbType.VarChar));
            p.Add(new Param("@PrimaryContactEmail", v.primaryContactEmail, SqlDbType.VarChar));
            p.Add(new Param("@LastUpdateDate", v.lastUpdateDate, SqlDbType.DateTime));
            p.Add(new Param("@LastUpdateUser", v.lastUpdateUser, SqlDbType.VarChar));
            p.Add(new Param("@VentureID", v.ventureID, SqlDbType.Int));
            p.Add(new Param("@Picture", v.Picture, SqlDbType.VarBinary));
            return p;
        }

        public int UpdateVenture(Venture v)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateVenture";
            objCommand.Parameters.Clear();

            List<Param> p = BuildUpdateVentureParams(v);

            foreach (Param param in p)
            {
                SqlParameter inputParameter = new SqlParameter(param.Name, param.Val);
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = param.Type;
                objCommand.Parameters.Add(inputParameter);
            }

            int results = objDB.DoUpdateUsingCmdObj(objCommand);

            return results;
        }

        public DataSet GetVentureMembersAndRoles(int ventureID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetVentureMembersAndRoles";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@VentureID", ventureID);

            DataSet ventureMembersAndRoles = objDB.GetDataSetUsingCmdObj(objCommand);

            return ventureMembersAndRoles;
        }

        public DataSet GetAllAdmins()
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllAdmins";
            objCommand.Parameters.Clear();

            DataSet adminInfo = objDB.GetDataSetUsingCmdObj(objCommand);

            return adminInfo;
        }

        public void CreateAdmin(string tuID, string firstName, string lastName, DateTime lastUpdateDate, string lastUpdateUser)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "CreateAdmin";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@TUID", tuID);
            objCommand.Parameters.AddWithValue("@LastName", lastName);
            objCommand.Parameters.AddWithValue("@FirstName", firstName);
            objCommand.Parameters.AddWithValue("@LastUpdateDate", lastUpdateDate);
            objCommand.Parameters.AddWithValue("@LastUpdateUser", lastUpdateUser);

            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public void DeactivateAdmin(string tuID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "DeactivateAdmin";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@TUID", tuID);

            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public bool CheckIfAdminExists(string TUID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CheckIfAdminExists";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@TUID", TUID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 15;
            objCommand.Parameters.Add(inputParameter);

            int count = Convert.ToInt32(objDB.ExecuteScalarFunction(objCommand));

            return count == 1;
        }

        public void ReactivateAdmin(string tuID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand.CommandText = "ReactivateAdmin";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@TUID", tuID);
            
            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public Object CheckIfDeactivatedAdminExists(string TUID)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CheckIfDeactivatedAdminExists";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@TUID", TUID);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 10;
            objCommand.Parameters.Add(inputParameter);

            Object count = objDB.ExecuteScalarFunction(objCommand);

            return count;
        }

        public Object CheckIfUsernameExists(string username)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CheckIfUsernameExists";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@Username", username);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            Object count = objDB.ExecuteScalarFunction(objCommand);

            return count;
        }

        public Object CheckIfVentureNameExists(string name)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CheckIfVentureNameExists";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@Name", name);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            Object count = objDB.ExecuteScalarFunction(objCommand);

            return count;
        }

        public DataTable SearchExpertsByName(string firstName, string lastName)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "SearchExpertByName";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@fn", firstName);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@ln", lastName);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            DataTable dtResults = objDB.GetDataSetUsingCmdObj(objCommand).Tables[0];
            
            return dtResults;
        }
        
        public Object CheckIfExpertEmailExists(string email)
        {
            DBConnect objDB = new DBConnect();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CheckIfExpertEmailExists";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@Email", email);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 50;
            objCommand.Parameters.Add(inputParameter);

            Object count = objDB.ExecuteScalarFunction(objCommand);

            return count;
        }

    }//end DbMethods Class
}//end namespace