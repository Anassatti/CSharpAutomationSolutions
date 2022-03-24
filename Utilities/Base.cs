using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading;
using AventStack.ExtentReports.Reporter;
using System.IO;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace SeleniumAutomationProject1.Utilities
{
    public class Base
    {
       public ExtentReports extent;
        ExtentTest test;
        //Get browser options from terminal
        string browserName; 
        //Report file
        [OneTimeSetUp]
        public void SetupReport()
        {

            //Generate file path
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string reportPath = projectDirectory + "//index.html";
            var htmlReport = new ExtentHtmlReporter(reportPath);
             extent = new ExtentReports();
            extent.AttachReporter(htmlReport);
            extent.AddSystemInfo("HostName","Local Host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Anas Satti");

        }
       // public IWebDriver driver;
       // This driver for parall execution
       public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        [SetUp]
        public void Setup()
        {

           test= extent.CreateTest(TestContext.CurrentContext.Test.Name);
             browserName = TestContext.Parameters["browserName"];
            if(browserName==null)
            { 
             browserName = ConfigurationManager.AppSettings["browser"];
            }
            intiBrowser(browserName);
            //Hit the URL
            //Driver.value is part of parall driver execution, in the sequencial execution no need to value
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            //Implicitwait
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Manage().Window.Maximize();
           

        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public void intiBrowser(string browserName)
        {
            switch(browserName)
            {
                case "Chrome": 
                     new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                   
                    break;
                case "Firefox":
                     new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
                case "IE":
                     new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;

            }
        


        }


        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void CloseBrowser()
        {
            DateTime time = DateTime.Now;
            string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            var status=  TestContext.CurrentContext.Result.Outcome.Status;
            var logTrace = TestContext.CurrentContext.Result.StackTrace;
            if (status==TestStatus.Failed)
            {
                test.Fail("Test failed", captureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail,"Test Fail"+ logTrace);
            }else if(status == TestStatus.Passed)
            {

            }
            extent.Flush();
            driver.Value.Quit();

           
        }
        public MediaEntityModelProvider captureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();

        }
    }
}
