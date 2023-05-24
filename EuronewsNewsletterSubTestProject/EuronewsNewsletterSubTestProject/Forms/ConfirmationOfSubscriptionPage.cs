using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace EuronewsNewsletterSubTestProject
{
    public  class ConfirmationOfSubscriptionPage : Form
    {
        private IButton backToSiteButton => ElementFactory.GetButton(By.XPath("//*[contains(text(),'Back to the site')]"), "Back to site button");

        public ConfirmationOfSubscriptionPage() : base(By.XPath("//*[contains(text(), 'Your subscription has been successfully confirmed.')]"), "Confirmation of subscription Page") { }

        public void ClickBackToSiteButton()
        {
            backToSiteButton.ClickAndWait();
        }

    }
}
