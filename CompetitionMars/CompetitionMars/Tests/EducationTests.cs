using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionMars.DataModel;
using CompetitionMars.Pages;
using CompetitionMars.Utilities;
using CompetitionMars.Utilities.ExtentReport;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionMars.Tests
{
    [TestFixture]
    public class EducationTests : CommonDriver
    {
        private LoginPage loginPageObject = new LoginPage();
        private EducationPage educationPageObject = new EducationPage();
        private JsonHelper jsonHelperObject = new JsonHelper();
        private List<Education> testData = new List<Education>();
        private ExtentReports extent;
        private ExtentTest test;

        [OneTimeSetUp]
        public void SetupReport()
        {
            string reportPath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\Utilities\\ExtentReport\\BaseReport.cs";
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }



        [SetUp]
        public void SetUpActions()
        {
            driver = new ChromeDriver();
            loginPageObject = new LoginPage();
            educationPageObject = new EducationPage();
            jsonHelperObject = new JsonHelper();

            loginPageObject.LoginSteps();
            
          //  string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\addEducationTestData.json";
           // testData = jsonHelperObject.ReadTestDataAddEducationFromJson(jsonFilePath);


        }

        [Test, Order(1)]
        public void TestAddEducationWithTestData()
        {
            EducationPage educationPageObject = new EducationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\AddEducationTestData.json";

            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());

            foreach (var education in testData)
            {
                string collegeName = education.CollegeName;
                Console.WriteLine(collegeName);

                string country = education.Country;
                Console.WriteLine(country);

                string degree = education.Degree;
                Console.WriteLine(degree);

                string title = education.Title;
                Console.WriteLine(title);

                string year = education.Year;
                Console.WriteLine(year);

                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                string screenshotPath = CaptureScreenshot(driver, "AddEducation");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                educationPageObject.AddEducation(collegeName, country, title, degree, year);

                string addedTitle = educationPageObject.GetAddedEducation();

                if (education.Title == addedTitle)
                {
                    Assert.AreEqual(education.Title, addedTitle, "Acual and expected education do not match");
                    test.Pass("Education added successfully");
                }
            }
        }

        
        [Test,Order(2)]
        public void TestUpdateEducation()
        {
            EducationPage educationPageObject = new EducationPage();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\UpdateEducationTestData.json";
            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());
            foreach (var education in testData)
            {
                string collegeName = education.CollegeName;
                Console.WriteLine(collegeName);

                string country = education.Country;
                Console.WriteLine(country);

                string degree = education.Degree;
                Console.WriteLine(degree);

                string title = education.Title;
                Console.WriteLine(title);

                string year = education.Year;
                Console.WriteLine(year);

                educationPageObject.UpdateEducation(collegeName, country, title, degree, year);
                string updatedEducation = educationPageObject.GetUpdatedEducation();

                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                string screenshotPath = CaptureScreenshot(driver, "UpdateEducation");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());


                if (updatedEducation == education.Degree)
                {
                    Assert.AreEqual(education.Degree, updatedEducation,"Actual and updated education do not match");
                    test.Pass("Education updated successfully");
                }
            }
            

        }

        [Test,Order(3)]
        public void DeleteEducationTest()
        {
            EducationPage educationPageObject = new EducationPage();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\DeleteEducationTestData.json";
            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());
            foreach (var education in testData)
            {
                string collegeName = education.CollegeName;
                string country = education.Country;
                string degree = education.Degree;
                string title = education.Title;
                string year = education.Year;
               
                educationPageObject.DeleteEducation(degree);
                string expectedMessage = educationPageObject.GetDeleteEducation();

                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                string screenshotPath = CaptureScreenshot(driver, "DeleteEducation");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());


                // string actualMessage = educationPageObject.GetDeleteMessage();
                // string expectedDeleteMessage = "Education entry successfully removed";
                //  Assert.AreEqual(actualMessage, expectedDeleteMessage, "Actual and expected message do not match");
                Assert.AreEqual("Deleted", expectedMessage, "Message mismatch.Education not deleted");
                test.Pass("Education deleted");
            }
            
        }

        [Test,Order(4)]
        public void AddEmptyEducationTest() 
        {
            EducationPage educationPageObject = new EducationPage();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\AddEmptyEducationTestData.json";
            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());
            foreach (var education in testData)
            {
                string collegeName = education.CollegeName;
                Console.WriteLine(collegeName);

                string country = education.Country;
                Console.WriteLine(country);

                string degree = education.Degree;
                Console.WriteLine(degree);

                string title = education.Title;
                Console.WriteLine(title);

                string year = education.Year;
                Console.WriteLine(year);

                educationPageObject.AddEmptyEducationField(collegeName, country, title, degree, year);

                string actualerrorMessage = educationPageObject.GetEmptyFieldErrorMessage();
                string expectedMessage = "Please enter all the fields";

                Assert.AreEqual(expectedMessage, actualerrorMessage, "Expected and actual message do not match");
                test.Pass("User not able to add empty fields");
            }
        }

        [Test,Order(5)]
        public void AddSameDegreeSameYearTest()
        {
            EducationPage educationPageObject = new EducationPage();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\SameDegreeSameYear.json";
            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());
            foreach (var education in testData)
            {
                string collegeName = education.CollegeName;
                Console.WriteLine(collegeName);

                string country = education.Country;
                Console.WriteLine(country);

                string degree = education.Degree;
                Console.WriteLine(degree);

                string title = education.Title;
                Console.WriteLine(title);

                string year = education.Year;
                Console.WriteLine(year);

                educationPageObject.AddSameDegreeSameYear(collegeName, country, title, degree, year);

                string actualerrorMessage = educationPageObject.GetSameEducationDetailsErrorMessage();
                string expectedMessage = "This information is already exist.";

              Assert.AreEqual(expectedMessage, actualerrorMessage, "Expected and actual message do not match");
              
            }
        }

        [Test,Order(6)]
        public void AddSameDegreeDifferentYearTest()
        {
            EducationPage educationPageObject = new EducationPage();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\SameDegreeDifferentYear.json";
            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());
            foreach (var education in testData)
            {
                string collegeName = education.CollegeName;
                Console.WriteLine(collegeName);

                string country = education.Country;
                Console.WriteLine(country);

                string degree = education.Degree;
                Console.WriteLine(degree);

                string title = education.Title;
                Console.WriteLine(title);

                string year = education.Year;
                Console.WriteLine(year);

                educationPageObject.AddEducation(collegeName, country, title, degree, year);

                string actualerrorMessage = educationPageObject.GetSameDegreeDifferentYearErrorMessage();
                string expectedMessage = "Duplicated data";

                Assert.AreEqual(expectedMessage, actualerrorMessage, "Expected and actual message do not match");
                
            }
        }
        [Test,Order(7)]
        public void UpdateEducationNoChangeTest()
        {
            EducationPage educationPageObject = new EducationPage();
            educationPageObject.UpdateEducationNoChange();

            string actualerrorMessage = educationPageObject.GetUpdateNoChangeErrorMessage();
            string expectedMessage = "This information is already exist.";

            Assert.AreEqual(expectedMessage, actualerrorMessage, "Expected and actual message do not match");
            test.Pass("Test is passed");

        }

        private string CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            string screenshotPath = Path.Combine(@"C:\internship notes\CompetitionMars\CompetitionMars\CompetitionMars\CompetitionMars\CompetitionScreenshot\", $"{screenshotName}_{DateTime.Now:yyyyMMddHHmmss}.png");
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            return screenshotPath;
        }


        [TearDown]
        public void TearDownActions()
        {
            driver.Quit();
        }

        [OneTimeTearDown]
        public void ExtentTeardown()
        {
            extent.Flush();
        }

    }


}
