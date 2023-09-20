using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders.JsonEncoders;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SauceDemo_Practicing.hooks
{
    
    internal class Base
    {
        public ExtentReports extent;
        public ExtentTest test;
        //private ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        private WebDriver driver;

        public IWebDriver getDriver()
        {
            return driver;
        }

        [OneTimeSetUp]
        public void PathToReport()
        {
            // tudo isso é só para criar o caminho onde o arquivo vai ser salvo
            //string workingDirectory = Environment.CurrentDirectory;
            string workingDirectory2 = "C:/Users/Murillo/source/repos/SauceDemo_Practicing/SauceDemo_Practicing/HtmlReport";
            String reportPath = workingDirectory2 + "//index.html";
            
            //inicializo um relatório html e passo o caminho para salvar
            var htmlReport = new ExtentHtmlReporter(reportPath);

            extent = new ExtentReports();
            extent.AttachReporter(htmlReport);
            extent.AddSystemInfo("Host Name", "Notebook Murillo");
            extent.AddSystemInfo("Enviroment", "QA");
            extent.AddSystemInfo("Username", "Murillo");
        }

        [SetUp]
        public void BrowserSetup()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            String browserName = ConfigurationManager.AppSettings["browser"];
            ConfigBrowser(browserName);
            String enviroment = ConfigurationManager.AppSettings["prod_url"];
            UrlTest(enviroment);            
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";

            if (status == TestStatus.Failed)
            {
                test.Fail("Test Failed", CaptureScreenShot(driver, fileName));
                test.Log(Status.Fail, "TEST FAILED WITH LOGTRACE : " + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {

            }
            extent.Flush();
            
            driver.Close();
            driver.Quit();
           
        }

        public MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, String screenShotName)
        {
            // casting the driver
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();


        }


        public void ConfigBrowser(String browser)
        {           
            switch (browser)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;
            }
            driver.Manage().Window.Maximize();
        }

        public void UrlTest(String site)
        {
            driver.Navigate().GoToUrl(site);
        }          
    }
}
