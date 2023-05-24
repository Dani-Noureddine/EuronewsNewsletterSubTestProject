using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;
using System.Text;

namespace EuronewsNewsletterSubTestProject
{
    public static class GmailUtils
    {
        public static string GetLinkFromConfirmationEmail(RestResponse response)
        {
            Email myEmail = JsonConvert.DeserializeObject<Email>(response.Content!.ToString())!;
            string base64 = myEmail.Payload!.Parts![0].Body!.Data!.ToString();
            base64 = base64.Replace('-', '+').Replace('_', '/');
            byte[] s = Convert.FromBase64String(base64);
            string html = Encoding.UTF8.GetString(s);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var link = doc.DocumentNode.SelectSingleNode("//a");
            return link.Attributes["href"].Value;
        }

        public static bool ConfirmationEmailReceived()
        {
            RestResponse? response = GmailApiUtils.GetLatestMail();
            if (response == null) return false;
            else return true;
        }
    }
}
