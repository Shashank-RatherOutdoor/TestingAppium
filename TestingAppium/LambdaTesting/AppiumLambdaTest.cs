using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace TestingAppium.LambdaTesting
{
    [TestFixture]
    public class AppiumTests
    {
        private IOSDriver driver;
        private string username = "schannegowdaratheroutdoors";
        private string accessKey = "hVBsO3iCWqUaiNpK677h5s4Zii5GEnByYGBjvLUQ9YI7w3nVWF";

        [SetUp]
        public void SetUp()
        {
            // Generate the Appium options using the option generator
            AppiumOptions options = GenerateOptions();
            options.AddAdditionalAppiumOption("user", username);
            options.AddAdditionalAppiumOption("accessKey", accessKey);

            // Initialize the remote WebDriver using LambdaTest Hub URL
            driver = new IOSDriver(new Uri($"https://{username}:{accessKey}@mobile-hub.lambdatest.com/wd/hub"), options);
        }

        [TearDown]
        public void TearDown()
        {
            // Quit the driver and end the session
            driver.Quit();
        }

        [Test]
        public void LoginTest()
        {
            // Login method
            driver.Navigate().GoToUrl("https://www.lews.com");

            Login(driver, "schannegowda@ratheroutdoors.com", "Testing1!");

            // Your test code here
            // For example, interacting with the app after login
            Console.WriteLine("Title: " + driver.Title);

            // Perform other interactions or assertions as needed
            //Assert.AreEqual("Expected Title", driver.Title);
        }

        static AppiumOptions GenerateOptions()
        {
            // Create a new AppiumOptions object
            AppiumOptions options = new AppiumOptions();

            // Create a dictionary to hold the LambdaTest options
            Dictionary<string, object> ltOptions = new Dictionary<string, object>
            {
                { "w3c", true },
                { "platformName", "ios" },
                { "deviceName", "iPhone 16 Pro Max" },
                { "platformVersion", "18" },
                { "network", true },
                { "timezone", "Kolkata" },
                { "video", true },
                { "geoLocation", "IN" },
                { "isRealMobile", true },
                { "console", true }
            };

            // Add the LambdaTest options to the AppiumOptions object
            options.AddAdditionalAppiumOption("lt:options", ltOptions);

            return options;
        }

        static void Login(IOSDriver driver, string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Locate the login UI and click it
            IWebElement loginUI = driver.FindElement(By.CssSelector("div.HeaderColumns a.header__navigation__login"));
            loginUI.Click();

            // Wait until the email input field is visible
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("email_input_field_id")));

            // Locate the email input field and enter the email
            IWebElement emailField = driver.FindElement(By.Id("email_input_field_id"));
            emailField.SendKeys(email);

            // Locate the password input field and enter the password
            IWebElement passwordField = driver.FindElement(By.Id("password_input_field_id"));
            passwordField.SendKeys(password);

            // Locate the login button and click it
            IWebElement loginButton = driver.FindElement(By.Id("login_button_id"));
            loginButton.Click();

            // Optionally, wait until an element on the new page is visible after login
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("some_element_on_new_page")));
        }
    }
}