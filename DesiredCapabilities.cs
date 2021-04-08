using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectSauceLab
{
   public  class DesiredCapabilities
    {
        public   string App { get; set; }
        public   string PlatformName { get; set; }
        public   string PlatformVersion { get; set; }
        public   string DeviceName { get; set; }
        public   string NewCommandTimeout { get; set; }
        public   string BrowserName { get; set; }
        public   string AutomationName { get; set; }
        public   string AppiumVersion { get; set; }
        public  string AppPackage { get; set; }
        public  string AppActivity { get; set; }
        public   string Udid { get; set; }
             
    }

    public class SauceLabKeys
    {
        public string SauceUser { get; set; }
        public string SauceAccessKey { get; set; }
        public string hubUrlPort { get; set; }
    }
    public class Capabilities
    {
        public  DesiredCapabilities desiredCapabilities { get; set; }
        public SauceLabKeys sauceLabKeys { get; set; }

    }
}
