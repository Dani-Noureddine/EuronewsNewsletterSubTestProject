# Euronews Newsletter Subscription Test Project

In this project I created an automated test that utilizes the Gmail API to test euronews website's newsletter subscription feature. 

What it does:
----------------------
The webdriver navigates to the euronews website, then to the newsletter form and selects a random newsletter to subscribe to. Upon subscribing a test email is entered.
A confirmation email is then sent by euronews to the given test email confirming subscription. Then the test retrieves the confirmation email from gmail through the api, parses it, and finds the unsubscribe option and unsubscribes to that newsletter.

Setting up the test:
----------------------
For the test to work properly, a test email needs to be specified in the testData.json file, as well as a valid access token to the gmail API in the credetials.json folder. Both files are found in the Resources folder. 
(brower on which the test is run can be specified in the settings.json file as by Aquality framework requires that)

Structure:
----------------------
This project uses Page Object Model design to provide a clean and efficient way to organize methods and utilities for each action and page navigated through by the webdriver. Each Form is defined by it's name and XPath, and it has methods interacting with it's webelements. 

A base test class containing the SetUp and TearDown method was not created here as there is only 1 test in this project.

This project was made using NUnit, Selenium, and Aquality framework.
