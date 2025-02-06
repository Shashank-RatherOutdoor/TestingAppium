using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestingAppium.LambdaTesting
{
    [TestFixture]
    public class AppiumTests
    {
        private AppiumDriver driver;
        public readonly int _customTimeoutInSeconds;
        private string username = "schannegowdaratheroutdoors";
        private string accessKey = "hVBsO3iCWqUaiNpK677h5s4Zii5GEnByYGBjvLUQ9YI7w3nVWF";
        public static string platform = "iOS";  //android //iOS
        public static string deviceName = "iPhone 16 Pro Max"; //iPhone 16 Pro Max//Galaxy S25//Pixel 9 Pro XL
        public static string platformVersion = "18"; //18 //15

        [SetUp]
        public void SetUp()
        {
            // Generate the Appium options using the option generator
            AppiumOptions options = GenerateOptions();
            options.AddAdditionalAppiumOption("user", username);
            options.AddAdditionalAppiumOption("accessKey", accessKey);

            // Initialize the remote WebDriver using LambdaTest Hub URL
            if(platform == "iOS")
            {
                driver = new IOSDriver(new Uri($"https://{username}:{accessKey}@mobile-hub.lambdatest.com/wd/hub"), options);

            }
            else
            {
                driver = new AndroidDriver(new Uri($"https://{username}:{accessKey}@mobile-hub.lambdatest.com/wd/hub"), options, TimeSpan.FromSeconds(600));

            }
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
            driver.Manage().Cookies.AddCookie(new Cookie("MCPopupClosed", "yes"));
            driver.Manage().Cookies.AddCookie(new Cookie("OptanonAlertBoxClosed", ""));



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
                { "platformName",  platform},
                { "deviceName", deviceName},
                { "platformVersion", platformVersion},
                { "network", true },
                { "timezone", "Kolkata" },
                { "video", true },
                { "geoLocation", "IN" },
                { "isRealMobile", true },
                { "console", true },
                {"unicodeKeyboard", true }
            };

            // Add the LambdaTest options to the AppiumOptions object
            options.AddAdditionalAppiumOption("lt:options", ltOptions);

            return options;
        }

        static void Login(AppiumDriver driver, string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Locate the login UI and click it
            IWebElement body = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("body")));
            //IWebElement body = driver.FindElement(By.TagName("body"));
            driver.Manage().Cookies.AddCookie(new Cookie("MCPopupClosed", "yes"));
            driver.Manage().Cookies.AddCookie(new Cookie("OptanonAlertBoxClosed", ""));

            IWebElement loginUI = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div.HeaderColumns a.header__navigation__login")));
            Actions actions = new Actions(driver);
            actions.Click(loginUI).Build().Perform();
            //var jsExecutor = (IJavaScriptExecutor)driver;
            //jsExecutor.ExecuteScript("arguments[0].click();", loginUI);

            //loginUI.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(body));
            // Wait until the email input field is visible
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("email_input_field_id")));

            // Locate the email input field and enter the email
            IWebElement emailField = driver.FindElement(By.Id("Email"));
            emailField.SendKeys(email);

            // Locate the password input field and enter the password
            IWebElement passwordField = driver.FindElement(By.Id("Password"));
            passwordField.SendKeys(password);
           //passwordField.SendKeys(Keys.Enter);
            //if (platform == "android")
            //{
            //    driver.HideKeyboard();
            //}

            // Locate the login button and click it
            body = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.TagName("body")));
            IWebElement loginButton = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("form.login__form button[type='submit']")));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(loginButton));
            loginButton.Click();

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(body));
     }

    }
}