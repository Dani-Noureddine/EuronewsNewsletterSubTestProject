using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace EuronewsNewsletterSubTestProject
{
    public class MainPageForm : Form
    {
        private IButton newslettersButton => ElementFactory.GetButton(By.XPath("//*[@class='c-internal-links__text' and @href='/newsletters']"), "Newsletters button");
        private IButton acceptCookiesButton => ElementFactory.GetButton(By.XPath("//*[@id='didomi-notice-agree-button']"), "Accept Cookies button");

        public MainPageForm() : base(By.XPath("//*[@class='o-site-header__logo' and @aria-label='Euronews Logo']"), "Main Page") { }

        public void ClickOnNewsletters()
        {
            newslettersButton.Click();
        }

        public void AcceptCookies()
        {
            acceptCookiesButton.WaitAndClick();
        }
    }
}