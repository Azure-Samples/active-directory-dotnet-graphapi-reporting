# WebApp-GraphAPI-Reporting
Web application sample that calls the new Azure AD Graph API reporting capabilities

Please refer to the "Azure AD Graph - Reports and Events" article for information on configuring your Azure AD tenant, to allow application access.  

After you've configured your tenant, update the following key/value pairs in the Web.Config file with your tenant and application configuration information :

add key="ida:Domain" value="<your-tenant-dns>"                               (DNS name for your AAD tenant)
add key="ida:ClientId" value="nnnnnnnn-nnnn-nnnn-nnnn-nnnnnnnnnnnn"          (GUID for your AAD application)
add key="ida:AppKey" value="nnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn"    (Secret key for your AAD application)


