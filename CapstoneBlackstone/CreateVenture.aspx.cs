using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneBlackstone.C_SharpClasses;
using System.IO;

namespace CapstoneBlackstone
{
    public partial class CreateVenture : System.Web.UI.Page
    {
        Validation valid = new Validation();
        SessionMethods sessionObj = new SessionMethods();
        DbMethods db = new DbMethods();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null)
            {
                Response.Redirect("default.aspx");
            }
        }

        protected void btnCreateVenture_Click(object sender, EventArgs e)
        {
            lblEmailsMust.Visible = false;
            lblRequired.Visible = false;
            lblPicCheckSize.Visible = false;
            lblPicCheckType.Visible = false;
            string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);//uploaded file extension
            int iFileSize = FileUpload1.PostedFile.ContentLength;//uploaded file size
            int count = Convert.ToInt32(db.CheckIfVentureNameExists(txtVentureName.Text));
            if (valid.IsBlank(txtVentureName.Text) || valid.IsBlank(txtPrimaryContactEmail.Text) || valid.IsBlank(txtEmail.Text) || valid.IsBlank(txtDescription.Text))
            {
                lblRequired.Visible = true;
            }
            else if (count >= 1)
            {
                lblVentureNameCheck.Visible = true;
            }
            else if(FileUpload1.FileContent == null && FileUpload1.HasFile == false)
            {//no pic detected in control

            }
            else if(valid.TestForLegalImageTypes(fileExtension) == false)
            {//fail
                lblPicCheckType.Visible = true;
                lblPicCheckType.Text = fileExtension + " file extension is not allowed. Please use .png, .gif, .jpg, .jpeg, .pdf, .pcd, .fpx, .tif instead";
            }
            else if(iFileSize >= 90000)
            {//fail
                lblPicCheckSize.Visible = true;
                lblPicCheckSize.Text = "Your file size is " + iFileSize + " bytes. Please reduce the size to less than 90 KB (9000 bytes).";
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                var img = System.Drawing.Image.FromStream(FileUpload1.FileContent);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                Session["venture-pic-upload"] = imageBytes;

                Venture venture = new Venture
                {
                    name = txtVentureName.Text,
                    description = txtDescription.Text,
                    aboutUs = aboutUs.Text,
                    contactEmail = txtEmail.Text,
                    contactPhoneNumber = txtPhoneNumber.Text,
                    contactLinkedIn = txtLinkedIn.Text,
                    Picture = (byte[])Session["venture-pic-upload"],
                    primaryContactEmail = txtPrimaryContactEmail.Text,
                    isActive = true,
                    lastUpdateDate = DateTime.Now,
                    lastUpdateUser = txtVentureName.Text
                };
                //create new venture

                int result = db.CreateVenture(venture);

                //add venture info to session
                int ventureID = Convert.ToInt32(db.GetVentureID(venture.name));
                sessionObj.storeVentureDataInSession(ventureID);

                //add current user as a venture member
                string role = "Founder";
                Expert expertProfileObj = (CapstoneBlackstone.Expert)Session["expertProfileObj"];
                string lastUpdateUser = expertProfileObj.lastName + ", " + expertProfileObj.firstName;
                DateTime lastUpdateDate = DateTime.Now;
                string TUID = expertProfileObj.tuID;
                db.CreateVentureMember(TUID, ventureID, role, lastUpdateDate, lastUpdateUser);

                //redirect to venture page
                Response.Redirect("VenturePage.aspx?name=" + venture.name);
            }

            
        }



        protected void save_Click(object sender, EventArgs e)
        {
            string[] extentions = new string[] { ".jpg", ".jpeg", ".png", ".bmp" };
            string fileName = FileUpload1.FileName;
            byte[] imageBytes = SerializeFile(FileUpload1, fileName, extentions);
            Session["venture-pic-upload"] = imageBytes;
        }


        public static byte[] SerializeFile(FileUpload control, string fileName, string[] allowedExtentions, string virtualDownloadPath = "~/UploadedForms/")
        {
            Boolean fileOK = false;
            byte[] bytes = null;

            if (control.HasFile)
            {
                string fileExtention = System.IO.Path.GetExtension(control.FileName).ToLower();
                for (int i = 0; i < allowedExtentions.Length; i++)
                {
                    if (fileExtention == allowedExtentions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                String path = System.Web.Hosting.HostingEnvironment.MapPath(virtualDownloadPath);
                if (!System.IO.Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string extention = Path.GetExtension(control.PostedFile.FileName);
                string filePath = path + fileName + extention;
                control.PostedFile.SaveAs(filePath);
                bytes = System.IO.File.ReadAllBytes(filePath);

                //Put it in the SQL server
                return bytes;
            }

            return bytes;
        }
    }
}