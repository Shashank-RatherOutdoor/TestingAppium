using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;

namespace AppiumCSharp.Utils
{
    public class PlatformCapabilities
    {
        AppiumOptions appiumOptions = new AppiumOptions();

        public AppiumOptions InitNativeAndroidCapabilities()
        {
            lock (appiumOptions)
            {
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.App, Startup.ReadFromAppSettings("App"));
                appiumOptions.AddAdditionalAppiumOption("appWaitActivity", Startup.ReadFromAppSettings("AppActivity"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, Startup.ReadFromAppSettings("DeviceName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, Startup.ReadFromAppSettings("OSVersion"));
            }            
            return appiumOptions;
        }

        public AppiumOptions InitNativeIOSCapabilities()
        {
            lock (appiumOptions)
            {
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, Startup.ReadFromAppSettings("PlatformName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.AutomationName, Startup.ReadFromAppSettings("AutomationName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, Startup.ReadFromAppSettings("PlatformVersion"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, Startup.ReadFromAppSettings("DeviceName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.App, Startup.ReadFromAppSettings("App"));
            }               
            return appiumOptions;
        }

        public AppiumOptions InitWebAndroidCapabilities()
        {
            lock (appiumOptions)
            {
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.AutomationName, Startup.ReadFromAppSettings("AutomationName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.BrowserName, Startup.ReadFromAppSettings("BrowserName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, Startup.ReadFromAppSettings("DeviceName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, Startup.ReadFromAppSettings("OSVersion"));
                //appiumOptions.AddAdditionalAppiumOption(ChromeOptions.Capability, JObject.Parse("{'w3c':false}")); //Required because of a bug in Appium C# client                   
            }                
            return appiumOptions;
        }

        public AppiumOptions InitWebIOSCapabilities()
        {
            lock (appiumOptions)
            {
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.AutomationName, Startup.ReadFromAppSettings("AutomationName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.BrowserName, Startup.ReadFromAppSettings("BrowserName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, Startup.ReadFromAppSettings("PlatformName"));
                appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, Startup.ReadFromAppSettings("DeviceName"));
            }                
            return appiumOptions;
        }
    }
}
