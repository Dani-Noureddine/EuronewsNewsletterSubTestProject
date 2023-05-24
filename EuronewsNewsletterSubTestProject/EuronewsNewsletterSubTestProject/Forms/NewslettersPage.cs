using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace EuronewsNewsletterSubTestProject
{
    public class NewslettersPage : Form
    {
        public static int newsletterId;
        private IButton selectNewsletterButton => ElementFactory.GetButton(By.XPath(GetRandomNewsletterXpath()), "Select Newsletter Button");
        private IButton newsletterPreviewButton => ElementFactory.GetButton(By.XPath($"//*[contains(@class, 'bg-white') and contains(@class, 'shadow-lg')][{newsletterId}]//a"), "Newsletter preview Button");

        public NewslettersPage() : base(By.XPath("//*[contains(@class, 'text-secondary') and contains(text(), 'Our newsletters')]"), "Newsletters Page") { }

        public void SelectRandomNewsletter()
        {
            selectNewsletterButton.Click();
        }

        public void ClickPreview()
        {
            newsletterPreviewButton.Click();
        }

        private static string GetRandomNewsletterXpath()
        {
            Random rand = new Random();
            int index = rand.Next(1, 8);
            newsletterId = index;
            return $"//*[contains(@class, 'bg-white') and contains(@class, 'shadow-lg')][{index}]//*[contains(text(),'Select this newsletter')]";
        }
    }
}