using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Runtime.CompilerServices;

namespace CompetitionMars.Utilities.ExtentReport
{
    public class BaseReport
    {
       // protected ExtentHtmlReporter htmlReporter;
        protected ExtentReports extent;
        protected ExtentTest test;



        [OneTimeSetUp]
        public void SetupReport()

        {
            
            string reportPath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\Utilities\\ExtentReport\\BaseReport.cs";
                // string reportFile = DateTime.Now.ToString().Replace("\\", "/");
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
           
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
                

        }

        [TearDown]

        [OneTimeTearDown]

                public void TearDownReport() 
        {
            extent.Flush();
        }

    }

}
