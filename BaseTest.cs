using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectSauceLab
{
    class BaseTest
    {
       public AppiumDriver<AppiumWebElement> Drivers;
        [SetUp]
        public void Setup()
        {
            Drivers = DriversSetup.Instance.InitDrivers();
        }

        [TearDown]
        public void Teardown()
        {
            if (Drivers == null) return;

            var isTestPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            ((IJavaScriptExecutor)Drivers).ExecuteScript("sauce:job-result=" + (isTestPassed ? "passed" : "failed"));
            Drivers.Quit();
        }

    }
}
