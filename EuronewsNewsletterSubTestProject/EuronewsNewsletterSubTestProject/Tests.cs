using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using EuronewsNewsletterSubTestProject.Forms;
using RestSharp;

namespace EuronewsNewsletterSubTestProject
{
    public class Tests
    {
        Browser browser = AqualityServices.Browser;
        private static ISettingsFile ConfigData => new JsonSettingsFile("config.json");
        
        [SetUp]
        public void Setup()
        {
            browser.Maximize();
            browser.GoTo(ConfigData.GetValue<string>("Url"));
            GmailApiUtils.MakeAllEmailsRead();
        }

        [TearDown]
        public void Teardown()
        {
            browser.Quit();
        }

        [Test]
        public void GmailEuronewsTest()
        {
            MainPageForm mainPageForm = new MainPageForm();
            Assert.True(mainPageForm.State.IsDisplayed, "Main Page did not open");
            mainPageForm.AcceptCookies();

            mainPageForm.ClickOnNewsletters();
            NewslettersPage newslettersPage = new NewslettersPage();
            Assert.True(newslettersPage.State.IsDisplayed, "Newsletters Page did not open");

            newslettersPage.SelectRandomNewsletter();
            EmailForm emailForm = new EmailForm();
            Assert.True(emailForm.State.IsDisplayed, "Email form did not open");

            emailForm.TypeInEmail();
            emailForm.ClickSubmit();
            AqualityServices.ConditionalWait.WaitForTrue(()=>GmailUtils.ConfirmationEmailReceived(), message:"Email for confirmation was not received");

            RestResponse? response = GmailApiUtils.GetLatestMail();
            string confirmationUrl = GmailUtils.GetLinkFromConfirmationEmail(response!);

            browser.GoTo(confirmationUrl);
            ConfirmationOfSubscriptionPage confirmationOfSubscriptionPage = new ConfirmationOfSubscriptionPage();
            Assert.True(confirmationOfSubscriptionPage.State.IsDisplayed, "Confirmation of Subscription Page did not open");

            confirmationOfSubscriptionPage.ClickBackToSiteButton();
            Assert.True(mainPageForm.State.IsDisplayed, "Main Page did not open");

            mainPageForm.ClickOnNewsletters();
            newslettersPage.ClickPreview();
            PreviewForm previewForm = new PreviewForm();
            Assert.True(previewForm.State.IsDisplayed, "Preview form did not open");

            string unsubLink = previewForm.GetUnsubscribeLink();
            browser.GoTo(unsubLink);
            UnsubPage unsubPage = new UnsubPage();
            Assert.True(unsubPage.State.IsExist, "Unsubscription Page did not open");

            GmailApiUtils.MakeAllEmailsRead();

            unsubPage.EnterEmail();
            unsubPage.SubmitUnsubscription();
            Assert.True(unsubPage.YouAreUnsubscribedMessageIsDisplayed(), "You are unsubscribed message was not displayed");

            Thread.Sleep(3000);
            Assert.False(GmailUtils.ConfirmationEmailReceived(), "Email about unsubscription has been received");

        }

    }
}