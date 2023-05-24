using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace EuronewsNewsletterSubTestProject
{
    public class EmailForm: Form
    {
        private static ISettingsFile TestData => new JsonSettingsFile("testData.json");

        private ITextBox enterEmailBox => ElementFactory.GetTextBox(By.XPath("//*[@class='w-full' and contains(@placeholder,'Enter your email')]"), "Enter your Email Textbox");
        private IButton submitButton => ElementFactory.GetButton(By.XPath("//*[contains(@class, 'btn-primary') and contains(@type, 'submit') and contains(@value, 'Submit')]"), "Submit Email Button");

        public EmailForm():base(By.XPath("//*[@class='w-full' and contains(@placeholder,'Enter your email')]"), "Email Form") { }

        public void TypeInEmail()
        {
            enterEmailBox.SendKeys(TestData.GetValue<string>("Email"));
        }

        public void ClickSubmit()
        {
            submitButton.Click();
        }
    }
}
