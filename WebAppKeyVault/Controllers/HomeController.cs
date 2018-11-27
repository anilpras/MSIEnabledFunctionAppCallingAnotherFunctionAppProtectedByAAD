using System;
using System.Web.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebAppKeyVault.Controllers
{
    public class HomeController : Controller
    {
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            AzureServiceTokenProvider _azToken = new AzureServiceTokenProvider();
            HttpClient _client = new HttpClient();

            try
            {

                string _token = await _azToken.GetAccessTokenAsync("https://clrprofiler.azurewebsites.net").ConfigureAwait(false);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var url = "https://clrprofiler.azurewebsites.net/";
                var result = await _client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Token = $"It worked";
                }
                else
                {
                    ViewBag.Token = $"Need more work to make it work :)";
                }

                ViewBag.statuCode = $"result: {result.StatusCode}";
      
            }
            catch (Exception exp)
            {
                ViewBag.Error = $"Error: {exp.Message}";
            }

            return View();
        }
       
    }
}