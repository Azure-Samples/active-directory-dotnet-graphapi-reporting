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
- An Azure subscription (a free trial is sufficient)

You will also need to be comfortable with the following tasks:

- Using the Azure Management Portal (or working with your AAD administrator) to do configuration work 
- Using Git and Github to bring the sample down to your location machine
- Using Visual Studio to edit configuration files, build, and run the sample

Every Azure subscription has an associated AAD tenant.  If you don't already have an Azure subscription, you can get a free subscription by signing up at [http://wwww.windowsazure.com](http://www.windowsazure.com).  Many of the Azure AD features used by this sample are available free of charge.  For complete information on the reports that require AAD Premium, please refer to the article [View your access and usage reports](http://azure.microsoft.com/en-us/documentation/articles/active-directory-view-access-usage-reports/).

### Step 1: Configure a Web App application in your AAD tenant
Before you can run the sample application you will need to allow it to access your AAD tenant.  If you already have a Web Application configured that you would like to use, you can jump to Step 2.

To configure a new AAD application:

1. Log in to the [Azure management portal](http://manage.windowsazure.com), using credentials that have been granted service co-administrator access on the subscription which is trusting your AAD tenant, as well as Global Administrator access in the AAD tenant.
2. Select the AAD tenant you wish to use, and go to the "Applications" page
3. From there, you can use the "Add" feature to "Add a new application my organization is developing"
4. Provide a name (ie: WebApp-GraphAPI-Reporting or similar) for the new application
5. Be sure to select the "Web Application and/or Web API" type, and specify a  valid URL for "Sign-on URL" and "App ID URI", which can be http://localhost for the purposes of this sample
6. After you've added the new application, select it again so you can make additional changes.  
7. Then select "Configure", and go to the "Keys" section, where you will create a shared secret key
8. NOTE: YOU WILL NEED TO NOTE AND SAVE THE KEY FOR LATER.  Key creation is the only time where you will be able to see the key you've created.  The key is made visible on the page after you click "Save".  
9. While you are on this page, also note the "Client ID" GUID as you will use this and the key in step #3 below.
10. The last step is to make sure the sample app has permissions to read reports in your AAD tenant.  You provide permissions by going to the "Permissions to other applications" section of your newly created application's configuration page, on the "Azure Active Directory" row, specify "Read Directory Data" under "Delegated Permissions", and click "Save" again.
11. NOTE: the permission "Access your organization's directory" allows the application to access your organization's directory on behalf of the signed-in user - this is a delegation permission and must be consented by the Administrator for web apps (such as this demo app).
The permission "Enable sign-on and read users' profiles" allows users to sign in to the application with their organizational accounts and lets the application read the profiles of signed-in users, such as their email address and contact information - this is a delegation permission, and can be consented to by the user.
The other permissions, "Read Directory data" and "Read and write Directory data", are Delegation and Application Permissions, which only the Administrator can grant consent to.


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




