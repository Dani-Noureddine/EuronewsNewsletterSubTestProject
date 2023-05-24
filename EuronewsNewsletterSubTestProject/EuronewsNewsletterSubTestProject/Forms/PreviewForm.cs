using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Diagnostics;

namespace EuronewsNewsletterSubTestProject
{
    public class PreviewForm : Form
    {
        Browser browser = AqualityServices.Browser;
        private static ISettingsFile TestData => new JsonSettingsFile("testData.json");

        private static int indexOfNewsletter = NewslettersPage.newsletterId;
        private static string newsletterId = TestData.GetValue<string>($"{indexOfNewsletter}");
        private static int indexOfFrame;
        private WebElement iframe => (WebElement)browser.Driver.FindElement(By.XPath($"(//*[@class='iframe-preview'])[{indexOfFrame}]"));

        public PreviewForm() : base(By.XPath($"//*[@id ='{newsletterId}']"), "Preview Form"){}
        
        public string GetUnsubscribeLink()
        {
            indexOfFrame = GetFrameIndex();
            browser.Driver.SwitchTo().Frame(iframe);
            WebElement unsubElement = (WebElement)browser.Driver.FindElement(By.XPath("//a[contains(text(),'unsubscribe by clicking here')]"));
            return unsubElement.GetAttribute("href");
        }

        private static int GetFrameIndex()
        {
            if(indexOfNewsletter>=3 && indexOfNewsletter <= 7)
            {
                return indexOfNewsletter-2;
            }
            else
            {
                return indexOfNewsletter + 5;
            }
        }

    }
}
