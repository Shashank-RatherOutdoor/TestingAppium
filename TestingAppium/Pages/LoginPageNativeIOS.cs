using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Threading;

namespace TestingAppium.Pages
{
    public class LoginPageNativeIOS : LoginPageNativeCommon
    {
        private By SecretPassword => By.ClassName("XCUIElementTypeStaticText");

        public override bool IsPasswordDisplayed()
        {
            ScrollIOS(SwipeDirection.Down, SecretPassword);
            return IsElementAppears(SecretPassword);
        }
    }
}
