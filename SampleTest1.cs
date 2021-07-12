using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectSauceLab
{
    class SampleTest1 : BaseTest
    {

        [Test]
        [Category("Sanity")]
        public void Test1()
        {
            var emailInput = Drivers.FindElementByAccessibilityId("test-Username");
            emailInput.SendKeys("standard_user");
            Assert.AreEqual(emailInput.Text, "standard_user");
            var passwordInput = Drivers.FindElementByAccessibilityId("test-Password");
            passwordInput.SendKeys("secret_sauce");
            //   Assert.AreEqual(passwordInput.Text, "secret_sauce");
            var loginBtn = Drivers.FindElementByAccessibilityId("test-LOGIN");
            loginBtn.Click();
            //var cart = Drivers.FindElementByName("test-Cart");
            //Assert.IsTrue(cart.Displayed);
        }

        [Test]
        [Category("Regression")]
        public void Test2()
        {
            var entryFiled = Drivers.FindElementByAccessibilityId("test-Username");
            entryFiled.SendKeys("1");
            Assert.AreEqual(entryFiled.Text, "1");
            //var btnElement = Drivers.FindElementByAccessibilityId("changeBtn");
            //btnElement.Click();
            //var label1 = Drivers.FindElementByAccessibilityId("CoffeeName");
            //Assert.AreEqual(label1.Text, "Pizza");
        }
    }
}
