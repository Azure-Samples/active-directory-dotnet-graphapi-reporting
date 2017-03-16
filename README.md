---
services: active-directory
platforms: dotnet
author: dstrockis
---

# Using the Graph API Reporting Features

This web application sample demonstrates the capabilities of the Azure AD (AAD) Graph API Reporting and Events service, which is currently in Preview.  Please refer to the MSDN article [Azure AD Reports and Events (Preview)] (https://msdn.microsoft.com/en-us/library/azure/mt126081.aspx) for details on the API, and [Azure AD Graph API Versioning] (https://msdn.microsoft.com/en-us/library/azure/dn835125.aspx) for more details on using Graph API Preview features.

## How To Run This Sample

To run this sample you will need:

- Visual Studio 2013 or higher
- An Internet connection
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, please see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/) 

You will also need to be comfortable with the following tasks:

- Using the Azure Management Portal (or working with your AAD administrator) to do configuration work 
- Using Git and Github to bring the sample down to your location machine
- Using Visual Studio to edit configuration files, build, and run the sample

For complete information on the reports that require AAD Premium, please refer to the article [View your access and usage reports](http://azure.microsoft.com/en-us/documentation/articles/active-directory-view-access-usage-reports/).

### Step 1: Configure a Web App application in your AAD tenant
Before you can run the sample application you will need to allow it to access your AAD tenant.  If you already have a Web Application configured that you would like to use, you can jump to Step 2.

To configure a new AAD application:

1. Sign in to the [Azure portal](https://portal.azure.com).
2. On the top bar, click on your account and under the **Directory** list, choose the Active Directory tenant where you wish to register your application.
3. Click on **More Services** in the left hand nav, and choose **Azure Active Directory**.
4. Click on **App registrations** and choose **Add**.
5. Enter a friendly name for the application, for example 'WebApp-GraphAPI-Reporting' and select 'Web Application and/or Web API' as the Application Type. For the sign-on URL, enter the base URL for the sample, which is by default `https://localhost`. Click on **Create** to create the application.
6. While still in the Azure portal, choose your application, click on **Settings** and choose **Properties**.
7. Find the Application ID value and copy it to the clipboard.
8. For the App ID URI, enter the base URL for the sample, which is by default `https://localhost`.
9. From the Settings menu, choose **Keys** and add a key - select a key duration of either 1 year or 2 years. When you save this page, the key value will be displayed, copy and save the value in a safe location - you will need this key later to configure the project in Visual Studio - this key value will not be displayed again, nor retrievable by any other means, so please record it as soon as it is visible from the Azure Portal.
10. Configure Permissions for your application - in the Settings menu, choose the 'Required permissions' section, click on **Add**, then **Select an API**, and select 'Microsoft Graph' (this is the Graph API). Then, click on  **Select Permissions** and select 'Read Directory Data'. NOTE: the permission "Access directory as the signed-in user" allows the application to access your organization's directory on behalf of the signed-in user - this is a delegation permission and must be consented by the Administrator for web apps (such as this demo app). The permission "Sign in and read user profile" allows users to sign in to the application with their organizational accounts and lets the application read the profiles of signed-in users, such as their email address and contact information - this is a delegation permission, and can be consented to by the user. The other permissions, "Read Directory data" and "Read and write Directory data", are Delegation and Application Permissions, which only the Administrator can grant consent to.

Please refer to the prerequisites section in the "Azure AD Reports and Events" article in the MSDN library (under Services, Azure Active Directory, Graph API) for more depth on configuring an Azure AD tenant to enable an application to access your tenant.  

### Step 2:  Clone or download this repository

From your shell (ie: Git Bash, etc.) or command line, run the following command :

    git clone https://github.com/Azure-Samples/active-directory-dotnet-graphapi-reporting.git

### Step 3:  Edit, build, and run the sample in Visual Studio 2013
After you've configured your tenant and downloaded the sample app, you will need to go into the local sub directory in which the Visual Studio solution is stored (typically in <your-git-root-directory>\WebApp-GrapAPI-Reporting), and open the WebApp-GraphAPI-Reporting.sln Visual Studio solution.  Upon opening, navigate to the Web.config file and update the following key/value pairs, using your tenant and application configuration information from earlier :

    <add key="ida:Domain" value="MyTenant.onMicrosoft.com"/>                  		<!-- DNS name for your AAD tenant -->
    <add key="ida:ClientId" value="aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"/>    		<!-- GUID for your AAD application -->
    <add key="ida:AppKey" value="abcd1234abcd1234abcd1234abcd1234abcd1234abcd"/>	<!-- Secret key for your AAD application -->

When finished, you should be able to successfully build and run the application, which will present a web form UI which you can use for testing against your AAD tenant.

### Step 4:  Run the application with your own AAD tenant
Use the drop down list box at the top of the web page to select which endpoint you would like to call.  The first item in the list will invoke the $metadata endpoint, which will return the Service Metadata Document (CSDL).  The remaining items in the list will invoke all of the report endpoints, some with variants to demonstrate the supported query options.



