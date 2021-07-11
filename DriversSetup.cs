using Newtonsoft.Json;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectSauceLab
{
   public sealed class DriversSetup
    {

        public Capabilities mobileCapabilities { get;  set; }
        public AppiumDriver<AppiumWebElement> Drivers;
        public static string HubUrlPart => "ondemand.us-west-1.saucelabs.com/wd/hub";
        public string SauceUser => Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);

        public string SauceAccessKey =>
            Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);

        public string Url => $"https://{SauceUser}:{SauceAccessKey}@{HubUrlPart}";
        private static DriversSetup instance = null;
        DriversSetup()
        {

        }
        public static DriversSetup Instance
         {  
        get  
        {  
            if (instance == null)  
            {  
                instance = new DriversSetup();
    }  
            return instance;  
        }  
    } 

        public AppiumDriver<AppiumWebElement> InitDrivers ()
        {
            var filepath = Path.Combine(System.IO.Directory.GetCurrentDirectory().Replace("/bin/Debug/net5.0", ""), "Configuration.json");
            var file = File.ReadAllText(filepath);
            mobileCapabilities = JsonConvert.DeserializeObject<Capabilities>(file);

            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("deviceName", mobileCapabilities.desiredCapabilities.DeviceName);
            capabilities.AddAdditionalCapability("platformVersion", mobileCapabilities.desiredCapabilities.PlatformVersion);
            capabilities.AddAdditionalCapability("platformName", mobileCapabilities.desiredCapabilities.PlatformName);
        //    capabilities.AddAdditionalCapability("browserName", "");
            capabilities.AddAdditionalCapability("appiumVersion", mobileCapabilities.desiredCapabilities.AppiumVersion);
          
            // capabilities.AddAdditionalCapability("app", "https://github.com/saucelabs/sample-app-mobile/releases/download/2.5.0/Android.SauceLabs.Mobile.Sample.app.2.5.0.apk");
            capabilities.AddAdditionalCapability("app", mobileCapabilities.desiredCapabilities.App);
 
           // string Url = $"https://{mobileCapabilities.sauceLabKeys.SauceUser}:{mobileCapabilities.sauceLabKeys.SauceAccessKey}@{mobileCapabilities.sauceLabKeys.hubUrlPort}";
            if (mobileCapabilities.desiredCapabilities.PlatformName.Equals("Android", StringComparison.OrdinalIgnoreCase))
            {
                capabilities.AddAdditionalCapability("appActivity", mobileCapabilities.desiredCapabilities.AppActivity);
                capabilities.AddAdditionalCapability("appPackage", mobileCapabilities.desiredCapabilities.AppPackage);
                Drivers = new AndroidDriver<AppiumWebElement>(new Uri(Url), capabilities, TimeSpan.FromSeconds(1000));
            }
            else
            {
                Drivers = new IOSDriver<AppiumWebElement>(new Uri(Url), capabilities, TimeSpan.FromSeconds(1000));

            }
            return Drivers;
        }

    }
}
