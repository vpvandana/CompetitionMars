using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using CompetitionMars.Pages;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;

namespace CompetitionMars.Utilities
{
    public class CommonDriver
    {
        public static IWebDriver driver;
        protected ExtentReports extent;
        protected ExtentTest test;

        [OneTimeSetUp]
        public void SetupReport()

        {

            string reportPath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\Utilities\\report.html";
            // string reportFile = DateTime.Now.ToString().Replace("\\", "/");
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

        }


        [SetUp]
        public void Initialize()

        {
            //Lauch Chrome browser
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            //Login page object initialization and definition
            LoginPage loginpage = new LoginPage();
            loginpage.LoginSteps();

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);


        }


        [TearDown]
        public void close()
        {
            driver.Quit();


        }

        [OneTimeTearDown]

        public void TearDownReport()
        {
            extent.Flush();
        }


    }
}

