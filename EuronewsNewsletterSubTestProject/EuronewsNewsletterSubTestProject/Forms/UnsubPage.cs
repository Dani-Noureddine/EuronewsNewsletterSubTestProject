using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace EuronewsNewsletterSubTestProject.Forms
{
    public class UnsubPage: Form
    {
        private static ISettingsFile TestData => new JsonSettingsFile("testData.json");
        private ITextBox enterEmailBox => ElementFactory.GetTextBox(By.XPath("//*[@id='email']"), "Enter your Email Textbox");
        private IButton confirmUnsubscriptionButton => ElementFactory.GetButton(By.XPath("//*[@type='submit']"), "Confirm Unsubscription Button");
        private ILabel youAreUnsubscribedText => ElementFactory.GetLabel(By.XPath("//*[contains(text(),'You are unsubscribed.')]"), "You are unsubscribed Label");
      
        public UnsubPage() : base(By.XPath("//title[contains(text(),'Newsletter unsubscription')]"), "Unsubscription Page") { }

        public void EnterEmail()
        {
            enterEmailBox.ClearAndType(TestData.GetValue<string>("Email"));
        }

        public void SubmitUnsubscription()
        {
            confirmUnsubscriptionButton.Click();
        }

        public bool YouAreUnsubscribedMessageIsDisplayed()
        {
           return youAreUnsubscribedText.State.IsDisplayed;
        }

    }
}
