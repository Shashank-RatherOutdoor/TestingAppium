using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Threading;

namespace TestingAppium.Pages
{
    public class HomePageNative : HomePage
    {
        protected override By ProductItem => MobileBy.AccessibilityId("test-Item title");
    }
}
