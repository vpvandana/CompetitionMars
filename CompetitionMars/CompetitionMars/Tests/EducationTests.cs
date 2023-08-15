using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CompetitionMars.DataModel;
using CompetitionMars.Pages;
using CompetitionMars.Utilities;
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
       
        [Test, Order(1)]
        public void TestAddEducationWithTestData()
        {
            EducationPage educationPageObject = new EducationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\AddEducationTestData.json";

            List<Education> educationtestData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

            foreach (var education in educationtestData)
            {
                string screenshotPath = CaptureScreenshot(driver, "AddEducation");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                educationPageObject.ClearExistingEntries();
                educationPageObject.AddEducation(education);

                string addedTitle = educationPageObject.GetAddedEducation(education);

                string expectedSuccessMessage =  "Education has been added";
                string actualMessage = educationPageObject.GetAddedMessage();
                Assert.AreEqual(expectedSuccessMessage, actualMessage, "Actual and expected message do not match");


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
                
                educationPageObject.UpdateEducation(education);
                string updatedEducation = educationPageObject.GetUpdatedEducation(education);

                
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
            foreach(var education in testData)
            {
                educationPageObject.AddEducation(education);
            }

            foreach (var education in testData)
            {
                
                educationPageObject.DeleteEducation(education);

                string screenshotPath = CaptureScreenshot(driver, "DeleteEducation");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                string expectedSuccessMessage = "Education entry successfully removed";
                string actualMessage = educationPageObject.GetDeletedMessage();
                Assert.AreEqual(expectedSuccessMessage, actualMessage, "Actual and expected message do not match");

                string expectedMessage = educationPageObject.GetDeleteEducation(education);

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

           
            foreach (var education in testData)
            {
              
                educationPageObject.AddEmptyEducationField(education);

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

            foreach (var education in testData)
            {
                
                educationPageObject.AddSameDegreeSameYear(education);

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
                educationPageObject.AddEducation(education);

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

    }


}
