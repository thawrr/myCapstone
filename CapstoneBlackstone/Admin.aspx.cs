using Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CapstoneBlackstone
{
    public partial class Admin1 : System.Web.UI.Page
    {


        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null)
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("default.aspx");
            }
            else if (Session["AdminToken"] == null)
            {
                Response.Redirect("ExpertPage.aspx");
            }
            DisplayExperts();
            DisplayVentures();
          
    
        }


        private void DisplayExperts()
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllExperts";

            // Execute the stored procedure using the DBConnect object and the SQLCommand object
            DataSet myDS = objDB.GetDataSetUsingCmdObj(objCommand);

            gvDisplayExperts.DataSource = myDS;
            gvDisplayExperts.DataBind();
            gvDisplayExperts.HeaderRow.TableSection = TableRowSection.TableHeader;


        }



        private void DisplayVentures ()
        {

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllVentures";

            DataSet myDS = objDB.GetDataSetUsingCmdObj(objCommand);

            gvDisplayVentures.DataSource = myDS;
            gvDisplayVentures.DataBind();
            gvDisplayVentures.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        protected void gvDisplayVentures_Sorting(object sender, GridViewSortEventArgs e)
        {
           
        }


        protected void gvDisplayExperts_Sorting(object sender, GridViewSortEventArgs e)
        {


        }

        protected void btnDownloadExperts_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=Experts.xls");
            Response.ContentType = "application/excel";

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            gvDisplayExperts.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
                     
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void btnDownloadVentures_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=Experts.xls");
            Response.ContentType = "application/excel";

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            gvDisplayVentures.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }
    }
}