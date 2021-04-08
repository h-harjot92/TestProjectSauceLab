using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectSauceLab
{
    public class Tests
    {
        DesiredCapabilities desiredCapabilities;
         AndroidDriver<AppiumWebElement> diver;
        //IOSDriver<IOSElement> diver;
        public string Url => "https://harjot:105340bb-f56c-4da6-be7d-549a548347d8@ondemand.us-west-1.saucelabs.com:443/wd/hub";
        [SetUp]
        public  void Setup()
        {
              UploadAndSetUp();
            //iOSCapabilities();
        }

        public void UploadAndSetUp()
        {
            var capabilities = new AppiumOptions();
            //We can run on any version of the platform as long as it's the correct device
            //Make sure to pick an Android or iOS device based on your app
            capabilities.AddAdditionalCapability("deviceName", "Google Pixel 3 GoogleAPI Emulator");
            //  if (!string.IsNullOrEmpty(_androidVersion))
            capabilities.AddAdditionalCapability("platformVersion", "10.0");
            capabilities.AddAdditionalCapability("platformName", "Android");
         //   capabilities.AddAdditionalCapability("newCommandTimeout", 180);
            capabilities.AddAdditionalCapability("browserName", "");
            capabilities.AddAdditionalCapability("appiumVersion", "1.20.2");
            capabilities.AddAdditionalCapability("appActivity", "crc64ad68f59cd763e462.MainActivity");
                //"SplashActivity");
            capabilities.AddAdditionalCapability("appPackage", "com.companyname.app1");
                //"com.swaglabsmobileapp");
            // capabilities.AddAdditionalCapability("app",
            //    "storage:6465f3a1-776d-4213-9bef-f4f37b89849a");
            // capabilities.AddAdditionalCapability("app", "https://github.com/saucelabs/sample-app-mobile/releases/download/2.5.0/Android.SauceLabs.Mobile.Sample.app.2.5.0.apk");
            capabilities.AddAdditionalCapability("app", "https://github.com/h-harjot92/AndroidApkForAutomation/raw/main/SampleAppAutomation.apk");
            diver = new AndroidDriver<AppiumWebElement>(new Uri("https://harjot:105340bb-f56c-4da6-be7d-549a548347d8@ondemand.us-west-1.saucelabs.com:443/wd/hub"), capabilities, TimeSpan.FromSeconds(1000));
        }

        public void iOSCapabilities()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("deviceName", "iPhone 11 Pro Simulator");
            capabilities.AddAdditionalCapability("platformVersion", "14.3");
            capabilities.AddAdditionalCapability("platformName", "iOS");
            capabilities.AddAdditionalCapability("newCommandTimeout", 180);
            capabilities.AddAdditionalCapability("browserName", "");
            capabilities.AddAdditionalCapability("appiumVersion", "1.20.1");
            // capabilities.AddAdditionalCapability("bundleId", "SwagLabsMobileApp.app");
            //  capabilities.AddAdditionalCapability("app",
            // "storage:6ebde158-5585-4f26-8979-9775dfbdb6c4");
            // capabilities.AddAdditionalCapability("app", "storage:filename=iOS.Simulator.SauceLabs.Mobile.Sample.app.2.7.1.zip");
            capabilities.AddAdditionalCapability("app", "storage:filename=iOS.Simulator.SauceLabs.Mobile.Sample.app.2.7.1.zip");

          var  diver = new IOSDriver<IOSElement>(new Uri("https://harjot:105340bb-f56c-4da6-be7d-549a548347d8@ondemand.us-west-1.saucelabs.com:443/wd/hub"), capabilities, TimeSpan.FromSeconds(1000));

        }

        public async Task<bool> UplaodApk()
        {
          
               var filePath = Path.Combine(@"H:\Harjot_data\", @"Android.SauceLabs.Mobile.Sample.app.2.2.1.apk");

          
                var path = Directory.GetCurrentDirectory();
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.us-west-1.saucelabs.com/v1/storage/upload"))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("harjot:105340bb-f56c-4da6-be7d-549a548347d8"));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                    var multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(new ByteArrayContent(File.ReadAllBytes(filePath)), "payload", Path.GetFileName(filePath));
                    multipartContent.Add(new StringContent(File.ReadAllText(filePath)), "name");
                    request.Content = multipartContent;
                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }

        [TearDown]
        public void Teardown()
        {
            if (diver == null) return;

            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            ((IJavaScriptExecutor)diver).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
            diver.Quit();
        }

        [Test]
        public void Test1()
        {
           var emailInput = diver.FindElementByAccessibilityId("test-Username");
            emailInput.SendKeys("standard_user");
            Assert.AreEqual(emailInput.Text, "standard_user");
            var passwordInput = diver.FindElementByAccessibilityId("test-Password");
            passwordInput.SendKeys("secret_sauce");
         //   Assert.AreEqual(passwordInput.Text, "secret_sauce");
            var loginBtn = diver.FindElementByAccessibilityId("test-LOGIN");
            loginBtn.Click();
            var cart = diver.FindElementByName("test-Cart");
            Assert.IsTrue(cart.Displayed);

        }
    }
}