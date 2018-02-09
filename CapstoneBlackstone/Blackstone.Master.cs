using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Connection;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CapstoneBlackstone
{
    public partial class Blackstone : System.Web.UI.MasterPage
    {
        DbMethods objDB = new DbMethods();
        Expert ex = new Expert();

        /*Security Variables - Preventing Cross-Site Forgery after successfuly Login*/
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        /*Loading events handled for added security*/
        protected void Page_Init(object sender, EventArgs e)
        {
            //First, check for the existence of the Anti-XSS cookie
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;

            //If the CSRF cookie is found, parse the token from the cookie.
            //Then, set the global page variable and view state user
            //key. The global variable will be used to validate that it matches 
            //in the view state form field in the Page.PreLoad method.
            if (requestCookie != null
                && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                //Set the global token variable so the cookie value can be
                //validated against the value in the view state form field in
                //the Page.PreLoad method.
                _antiXsrfTokenValue = requestCookie.Value;

                //Set the view state user key, which will be validated by the
                //framework during each request
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            //If the CSRF cookie is not found, then this is a new session.
            else
            {
                //Generate a new Anti-XSRF token
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");

                //Set the view state user key, which will be validated by the
                //framework during each request
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                //Create the non-persistent CSRF cookie
                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    //Set the HttpOnly property to prevent the cookie from
                    //being accessed by client side script
                    HttpOnly = true,

                    //Add the Anti-XSRF token to the cookie value
                    Value = _antiXsrfTokenValue
                };

                //If we are using SSL, the cookie should be set to secure to
                //prevent it from being sent over HTTP connections
                if (FormsAuthentication.RequireSSL &&
                    Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }

                //Add the CSRF cookie to the response
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            //During the initial page load, add the Anti-XSRF token and user
            //name to the ViewState
            if (!IsPostBack)
            {
                //Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;

                //If a user name is assigned, set the user name
                ViewState[AntiXsrfUserNameKey] =
                       Context.User.Identity.Name ?? String.Empty;
            }
            //During all subsequent post backs to the page, the token value from
            //the cookie should be validated against the token in the view state
            //form field. Additionally user name should be compared to the
            //authenticated users name
            else
            {
                //Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] !=
                         (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of " +
                                        "Anti-XSRF token failed.");
                }
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Tip: You should add all of your security checks and methods here*/
            Secure_My_Session();
            if(!IsPostBack)
                FillControlls();
        }

        public void FillControlls()
        {
            if ((Expert)Session["expertProfileObj"] != null)
            {
                ex = (Expert)Session["expertProfileObj"];
                if (ex.roleVentureNameList.Count != 0)
                {
                    ddlMyVentures.Visible = true;
                    List<string> localVentureNameList = new List<string>();
                    localVentureNameList.Add("My Ventures");
                    //DataSet myDS = objDB.GetAllVentureNamesByTuid((string)Session["TU_ID"]);
                    for (int i = 0; i < ex.roleVentureNameList.Count; i++)
                    {
                        localVentureNameList.Add(ex.roleVentureNameList[i].Item1);
                    }
                    foreach (string ventureName in localVentureNameList)
                    {
                        ddlMyVentures.Items.Add(new ListItem(ventureName));
                    }
                    this.ddlMyVentures.DataBind();
                }
                else
                    ddlMyVentures.Visible = false;
                
                lblLoggedIn.Text = "You are logged in as: " + ex.firstName + " " + ex.lastName;
            }
        }

        protected void ddlMyVentures_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("VenturePage.aspx?name=" + ddlMyVentures.SelectedValue.ToString());
        }

        /*Marking Session object as secured*/
        protected void Secure_My_Session()
        {
            if (Response.Cookies.Count > 0)
            {
                foreach (string s in Response.Cookies.AllKeys)
                {
                    if (s == FormsAuthentication.FormsCookieName || s.ToLower() == "asp.net_sessionid")
                    {
                        Response.Cookies[s].Secure = true;
                    }
                }
            }
        }

        /*Navigation Methods*/
        protected void lb_Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("default.aspx");
        }

        
    }
}