using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneBlackstone.C_SharpClasses;
using System.Data.SqlClient;
using System.Data;

namespace CapstoneBlackstone
{
    public partial class navBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //get ventureNames by TUID that's logged in
        public static List<string> SearchSkills(string prefixText, int count)
        {
            //change later to search local dataSet

            Connection.DBConnect connection = new Connection.DBConnect();
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

    }//end partial class
}