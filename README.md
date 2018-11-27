# MSI Enabled FunctionApp Calling Another FunctionApp Protected By AAD

<h2>Steps</h2>
    
 Step# 1: Create a Function App and enable MSI - Lets call it a client Function App and it would be calling a Server(Another Function APP)
          https://docs.microsoft.com/en-us/azure/app-service/app-service-managed-service-identity 
             
    
 Step# 2: Once MSI is enabled, configure the Server Funcation App and enable the Easy Authentication, here is how you need to do that.
 
          Function App  >  
          Platform Features >  
          "Authentication / Authorization" > 
          Enable the "App Service Authentication" >
          "Log in with Azure Active Directory"
          "Authentication Providers" > 
          Select the "Express">
          click Ok and then SAVE
          
         Now here is a trick, Follow the above again, but this time instead of "Express" click on "Advanced". 
         You will get an option to  add Allowed Token Audiences, and then add the https://<WebAppProtectedByAAD>.azurewebsites.net 
         { which is logically a same  webapp}
         
         Code in the Client Function App would like below:
         
                string accessToken = awaitazureServiceTokenProvider.GetAccessTokenAsync
                ("https://<WebAppProtectedByAAD>.azurewebsites.net").ConfigureAwait(false);
           
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var url = "https:// WebAppProtectedByAAD.azurewebsites.net/";
                var result = await client.GetAsync(url);


     Step# 3: Finish
