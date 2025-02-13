using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SeleniumAndroidTests
{
    [TestFixture]
    public class AndroidTests
    {
        private AndroidDriver driver;

        [SetUp]
        public void Setup()
        {
            // Set the capabilities
            var capabilities = new AppiumOptions();
            capabilities.PlatformName = "Android";
            capabilities.BrowserName = "Chrome";
            capabilities.DeviceName = "Pixel 9 API 35"; // Use the device name found using adb //Pixel 9 API 35 //23043RP34I
            capabilities.AutomationName = "UiAutomator2";
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.Udid, "emulator-5554"); // Replace YOUR_DEVICE_UDID with your device's UDID //emulator-5554 //a7b426b7
            capabilities.AddAdditionalAppiumOption("chromedriverExecutable", "C:\\ChromeDriver\\chromedriver_124.0.6367.219.exe"); // Replace with the actual path to your Chromedriver executable
            capabilities.AddAdditionalAppiumOption("androidPackage", "com.android.chrome"); // Add the androidPackage option
            capabilities.AcceptInsecureCertificates = true; // Set accept insecure certificates

            // Add Chrome options for incognito mode
            var chromeOptions = new Dictionary<string, object>
            {
                ["args"] = new List<string> { "--incognito" }
            };
            capabilities.AddAdditionalAppiumOption("goog:chromeOptions", chromeOptions);

            // Define the Appium server URI
            var serverUri = new Uri("http://192.168.29.212:4723/");

            // Initialize the driver
            driver = new AndroidDriver(serverUri, capabilities);
        }

        [Test]
        public void LaunchWebAppTest()
        {
            // Launch the web app
            driver.Navigate().GoToUrl("https://www.lews.com/"); // Replace with your web app URL
            var title = driver.Title;
            // Add your test steps here
            var element = driver.FindElement(By.XPath("//a[contains(text(),'Feel the Difference')]"));
            Assert.That(title.Contains("Feel the Difference"), "Web app did not launch successfully!");
        }

        [TearDown]
        public void TearDown()
        {
            // Quit the driver
            driver.Quit();
        }
    }
}