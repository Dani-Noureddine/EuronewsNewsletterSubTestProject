using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using RestSharp;

namespace EuronewsNewsletterSubTestProject
{
    public static class ApiUtils
    {
        private static ISettingsFile Credentials => new JsonSettingsFile("credentials.json");

        public static RestResponse SendRequest(string url, RestRequest request)
        {
            RestClient client = new RestClient(url);
            request.AddHeader("Authorization", "Bearer " + Credentials.GetValue<string>("AccessToken"));
            return client.Execute(request);
        }

    }
}
