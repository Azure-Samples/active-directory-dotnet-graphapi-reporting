using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

// Missing references? Install: ADAL
// Go to Package manager console, under Tools, in visual studio and run:
// Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace SampleAPIProject
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Security.Authentication;

    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Set up URL for first item in DropDownReports combo
                if (String.IsNullOrEmpty((txtUrl.Text)))
                {
                    DropDownReports.SelectedIndex = 0;
                    txtUrl.Text = DropDownReports.SelectedValue;
                }

                // Call the respective REST API
                if (Request.QueryString.Count > 0)
                {
                    CallWebRequest();
                }
            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            // Clear error text box
            LblError.Text = "";

            // Call the respective REST API
            CallWebRequest();
        }

        private string GetToken()
        {
            string token = "";

            try
            {
                // Attempt to acquire an OAuth access token for the given application configuration
                AuthenticationContext authContext =
                    new AuthenticationContext(String.Format("{0}/{1}",
                        ConfigurationManager.AppSettings["ida:AADInstance"],
                        ConfigurationManager.AppSettings["ida:Domain"]));

                ClientCredential credential = new ClientCredential(ConfigurationManager.AppSettings["ida:ClientID"],
                    ConfigurationManager.AppSettings["ida:AppKey"]);

                AuthenticationResult result =
                    authContext.AcquireToken(ConfigurationManager.AppSettings["ida:GraphResourceId"], credential);
                token = result.AccessToken;
            }
            catch (AuthenticationException ex)
            {
                LblError.Text = String.Format("Acquiring a token failed with the following error: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    //You should implement retry and back-off logic per the guidance given here:http://msdn.microsoft.com/en-us/library/dn168916.aspx
                    //InnerException Message will contain the HTTP error status codes mentioned in the link above
                    LblError.Text += String.Format("Error detail: {0}", ex.InnerException.Message);
                }
            }

            return token;
        }

        private void CallWebRequest()
        {
            try
            {
                // 1. Build up the HTTP request, add "Authorization" header with bearer token
                // 2. Attempt to call the reporting service REST endpoint specified in txtUrl 
                string token = lblToken.Text = GetToken();

                string uri = txtUrl.Text;
                WebRequest request = WebRequest.Create(uri);

                request.Headers.Add("Authorization", String.Format("Bearer {0}", token));
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        txtResults.Text = reader.ReadToEnd();
                    }
                }

            }
            catch (WebException ex)
            {
                LblError.Text = ex.Message;
            }
        }

        protected void DropDownReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the URL textbox accordingly, based on current combobox selection
            txtUrl.Text = DropDownReports.Text;    
        }
    }
}