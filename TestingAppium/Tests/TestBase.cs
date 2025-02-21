using TestingAppium.Pages;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using System;
using System.IO;
using System.Threading;
using TestingAppium.Utilities;

namespace TestingAppium.Tests
{
    public class TestBase
    {
        AppiumLocalService appiumLocalService;
        PlatformCapabilities platformCapabilities = new PlatformCapabilities();
        AppiumOptions appiumOptions = new AppiumOptions();
        AppiumServiceBuilder appiumServiceBuilder = new AppiumServiceBuilder();
        // Report report = new Report();
        ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        private readonly string url = Startup.ReadFromAppSettings("AppUrl");

        protected LoginPage loginPage;
        protected HomePage homePage;

        //BrowserStack Credentials
        private readonly static string username = "<your_browserstack_username>";
        private readonly static string accessKey = "<your_browserstack_accesskey>";
        private readonly Uri uri = new Uri($"https://{username}:{accessKey}@hub-cloud.browserstack.com/wd/hub");

        private enum PlatformType
        {
            Android,
            iOS,
            WebAndroid,
            WebIOS
        }

        private AppiumDriver InitializeDriver(PlatformType platformType)
        {
            appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.NewCommandTimeout, 300);
            appiumOptions.AddAdditionalAppiumOption("browserstack.user", username);
            appiumOptions.AddAdditionalAppiumOption("browserstack.key", accessKey);
            switch (platformType)
            {
                case PlatformType.Android:
                    appiumOptions = platformCapabilities.InitNativeAndroidCapabilities();
                    BasePage.driver.Value = new AndroidDriver(uri, appiumOptions);
                    loginPage = new LoginPageNativeAndroid();
                    homePage = new HomePageNative();
                    break;
                case PlatformType.iOS:
                    appiumOptions = platformCapabilities.InitNativeIOSCapabilities();
                    BasePage.driver.Value = new IOSDriver(uri, appiumOptions);
                    loginPage = new LoginPageNativeIOS();
                    homePage = new HomePageNative();
                    break;
                case PlatformType.WebAndroid:
                    appiumOptions = platformCapabilities.InitWebAndroidCapabilities();
                    BasePage.driver.Value = new AndroidDriver(uri, appiumOptions);
                    loginPage = new LoginPageWeb();
                    homePage = new HomePageWeb();
                    NavigateToWebApp();
                    break;
                case PlatformType.WebIOS:
                    appiumOptions = platformCapabilities.InitWebIOSCapabilities();
                    BasePage.driver.Value = new IOSDriver(uri, appiumOptions);
                    loginPage = new LoginPageWeb();
                    homePage = new HomePageWeb();
                    NavigateToWebApp();
                    break;
                default:
                    throw new Exception("No platform selected!");
            }
            return BasePage.GetDriver();
        }


        private void StartAppiumServer()
        {
            string appiumJSPath = Environment.ExpandEnvironmentVariables($"%SystemDrive%/Users/%USERNAME%/AppData/Local/Programs/Appium/resources/app/node_modules/appium/build/lib/main.js");
            appiumServiceBuilder
                .WithAppiumJS(new FileInfo(appiumJSPath))
                .UsingDriverExecutable(new FileInfo(@"C:\Program Files\nodejs\node.exe"));
            appiumLocalService = appiumServiceBuilder.Build();
            if (!appiumLocalService.IsRunning)
                appiumLocalService.Start();
        }

        private void CloseAppiumServer()
        {
            if (appiumLocalService.IsRunning)
                appiumLocalService.Dispose();
        }

        private void SetUpDriver()
        {
            Enum.TryParse(Startup.ReadFromAppSettings("PlatformType"), out PlatformType platformType);
            BasePage.driver.Value = InitializeDriver(platformType);
        }

        private void CloseDriver() => BasePage.GetDriver()?.Quit();

        private void AttachScreenshotToTheReport()
        {
            try
            {
                if (BasePage.GetDriver() != null)
                {
                    if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                    {
                        var screenshot = BasePage.GetDriver().GetScreenshot();
                        string screenshotPath = $"{TestContext.CurrentContext.Test.MethodName}.png";
                        screenshot.SaveAsFile(screenshotPath);
                        //report.SaveScreenshotToReport(screenshotPath);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Snapshot was not taken" + e.Message);
            }
        }

        private void SetLandscapeOrientation()
        {
            BasePage.GetDriver().Orientation = ScreenOrientation.Landscape;
            //Report.test.Info($"Browser has switched to landscape orientation");
        }

        private void NavigateToWebApp()
        {
            SetLandscapeOrientation();
            BasePage.GetDriver().Navigate().GoToUrl(url);
            //Report.test.Info($"Browser navigated to {url}");
        }

        private IConfiguration BuildTestDataFile() => configurationBuilder.AddJsonFile(@"Utils\TestsData.json").Build();

        protected string GetData(string value) => BuildTestDataFile().GetSection(value).Value;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            StartAppiumServer();
            //report.StartReport();
        }


        [OneTimeTearDown]
        public void OneTimeTearDown() => CloseAppiumServer();


        [SetUp]
        public void SetUp()
        {
            //report.CreateTest();
            SetUpDriver();
        }


        [TearDown]
        public void TearDown()
        {
            AttachScreenshotToTheReport();
            // report.LogTestStatus();
            CloseDriver();
        }
    }
}