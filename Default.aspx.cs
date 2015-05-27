using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

// Missing references? Install: ADAL
// Go to Package manager console, under Tools, in visual studio and run:
// Install-Package Microsoft.IdentityModel.Clients.ActiveDirectory;
// 
/* Only change these values in web.config
    <add key="ida:Domain" value="MyTenant.onMicrosoft.com"/>                        <!-- DNS name for your AAD tenant -->
    <add key="ida:ClientId" value="aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"/>          <!-- GUID for your AAD application -->
    <add key="ida:AppKey" value="abcd1234abcd1234abcd1234abcd1234abcd1234abcd"/>    <!-- Secret key for your AAD application -->
 */

namespace SampleAPIProject
{
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Security.Authentication;
    using System.Web.UI.WebControls;

    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateDropdown();

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

        private void PopulateDropdown()
        {
            string domainUrl = String.Format("{0}/{1}",
                ConfigurationManager.AppSettings["ida:GraphResourceId"],
                ConfigurationManager.AppSettings["ida:Domain"]);
            string basicUrl = domainUrl + "/reports/{0}?api-version=beta";

            DropDownReports.Items.Add(new ListItem("List of reports", domainUrl + "/reports?api-version=beta"));
            DropDownReports.Items.Add(new ListItem("OData Service Metadata", String.Format(basicUrl,"$metadata")));
            DropDownReports.Items.Add(new ListItem("Audit report for last 30 days", String.Format(basicUrl,"auditEvents")));
            DropDownReports.Items.Add(new ListItem("Audit report filtered by date range", String.Format(basicUrl,"auditEvents")+ ComputeDateString()));
            DropDownReports.Items.Add(new ListItem("Account Provisioning Events", String.Format(basicUrl, "accountProvisioningEvents")));
            DropDownReports.Items.Add(new ListItem("SignIns From Unknown Sources", String.Format(basicUrl, "signInsFromUnknownSourcesEvents")));
            DropDownReports.Items.Add(new ListItem("SignIns From IP Addresses With Suspicious Activity", String.Format(basicUrl, "signInsFromIPAddressesWithSuspiciousActivityEvents")));
            DropDownReports.Items.Add(new ListItem("SignIns From Multiple Geographies", String.Format(basicUrl, "signInsFromMultipleGeographiesEvents")));
            DropDownReports.Items.Add(new ListItem("SignIns From Possibily Infected Devices", String.Format(basicUrl, "signInsFromPossiblyInfectedDevicesEvents")));
            DropDownReports.Items.Add(new ListItem("Irregular Sign Ins", String.Format(basicUrl, "irregularSignInActivityEvents")));
            DropDownReports.Items.Add(new ListItem("Users with Anomalous SignIn Activity", String.Format(basicUrl, "allUsersWithAnomalousSignInEvents")));
            DropDownReports.Items.Add(new ListItem("SignIns after Multiple Failures", String.Format(basicUrl, "signInsAfterMultipleFailuresEvents")));
        }

        private string ComputeDateString()
        {
            string datefilter = "&$filter=eventTime gt {0} and eventTime lt {1}";
            var fromDate = DateTime.UtcNow.AddDays(-5).Date;
            var ToDate = DateTime.UtcNow.AddDays(-2).Date;

            return String.Format(datefilter, fromDate.ToString("yyyy-MM-dd"), ToDate.ToString("yyyy-MM-dd"));
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
                string token = GetToken();

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

                lblResult.Text = "Result: " + txtUrl.Text;

            }
            catch (WebException ex)
            {
                LblError.Text = ex.Message;
            }
        }

        protected void DropDownReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUrl.Text = DropDownReports.SelectedValue;
        }

    }
}
