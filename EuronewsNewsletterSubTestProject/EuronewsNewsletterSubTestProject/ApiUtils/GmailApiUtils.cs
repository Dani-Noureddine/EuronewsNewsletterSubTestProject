using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Newtonsoft.Json;
using RestSharp;

namespace EuronewsNewsletterSubTestProject
{
    public static class GmailApiUtils
    {
        private static ISettingsFile TestData => new JsonSettingsFile("testData.json");
        private static ISettingsFile UrlsAndEndpoints => new JsonSettingsFile("apiUrlsAndEndpoints.json");

        private static string urlOfUserEmail = string.Format(UrlsAndEndpoints.GetValue<string>("UrlOfUserEmail"), TestData.GetValue<string>("Email"));

        public static RestResponse? GetLatestMail()
        {
            string? id = GetUnreadEmailId();
            if (id == null)
            {
                return null;
            }
            string endpoint = string.Format(UrlsAndEndpoints.GetValue<string>("IdsEndpoint"),id);
            RestRequest request = new(endpoint);
            request.Method = Method.Get;
            RestResponse response = ApiUtils.SendRequest(urlOfUserEmail, request);;
            return response;
        }

        public static void MakeAllEmailsRead()
        {
            List<string> ids = GetAllEmailIds();
            foreach (string id in ids)
            {
                RestRequest request = new(UrlsAndEndpoints.GetValue<string>("BatchModifyMessagesEndpoint"));
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("ids", $"{id}");
                request.AddParameter("removeLabelIds", "UNREAD");
                request.Method = Method.Post;
                ApiUtils.SendRequest(urlOfUserEmail, request);
            }
        }

        private static List<string> GetAllEmailIds()
        {
            RestRequest request = new(UrlsAndEndpoints.GetValue<string>("MessagesEndpoint"));
            RestResponse response = ApiUtils.SendRequest(urlOfUserEmail, request);
            List<string> ids = new List<string>();
            EmailListModel myEmails = JsonConvert.DeserializeObject<EmailListModel>(response.Content!.ToString())!;
            foreach (Message message in myEmails.Messages!)
            {
                ids.Add(message.Id!);
            };
            return ids;
        }

        private static string? GetUnreadEmailId()
        {
            RestRequest request = new(UrlsAndEndpoints.GetValue<string>("MessagesEndpoint"));
            request.AddParameter("labelIds", "UNREAD");
            request.Method= Method.Get;
            RestResponse response = ApiUtils.SendRequest(urlOfUserEmail, request);
            try
            {
                EmailListModel myEmails = JsonConvert.DeserializeObject<EmailListModel>(response.Content!.ToString())!;
                return myEmails.Messages![0].Id;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

    }
}
