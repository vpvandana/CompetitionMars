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
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionMars.Tests
{
    [TestFixture]
    public class CertificationTests : CommonDriver
    {
        private LoginPage loginPageObject = new LoginPage();
        private CertificationPage certificationPageObject = new CertificationPage();
        private JsonHelper jsonHelperObject = new JsonHelper();
        private List<Certification> testData = new List<Certification>();
        

        [Test,Order(1)]
        public void TestAddCertificationWithTestData()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\AddCertificateTestData.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());

            foreach (var certificate in testData)
            {
                string certificateName = certificate.Certificate;
                
                string screenshotPath = CaptureScreenshot(driver, "AddCertification");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                certificationPageObject.ClearExistingEntries();
                certificationPageObject.AddCertification(certificate);
                string addedCertificate = certificationPageObject.GetAddedCertification(certificate);

                string expectedSuccessMessage = certificateName + " has been added to your certification";
                string actualMessage = certificationPageObject.GetAddedMessage();
                Assert.AreEqual(expectedSuccessMessage, actualMessage, "Actual and expected message do not match");

                if (certificateName == addedCertificate)
                {

                    Assert.AreEqual(certificate.Certificate, addedCertificate, "Acual and expected certification do not match");
                    test.Pass("Add certification test passed");
                  
                }
               else { test.Fail("Test Failed"); }
               
            }
        }
        [Test,Order(2)]

        public void TestUpdateCertification()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\UpdateCertificateTestData.json";
            string jsonData = File.ReadAllText(jsonFilePath);

            List<Certification> certificationTestData = JsonConvert.DeserializeObject<List<Certification>>(jsonData);
            Console.WriteLine(certificationTestData.ToString());

            foreach (var certificate in certificationTestData)
            {
               
                certificationPageObject.UpdateCertification(certificate);

                string screenshotPath = CaptureScreenshot(driver, "GetUpdatedCertification");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());


                string updatedCertificate =  certificationPageObject.GetUpdatedCertification(certificate);   

                if (certificate.Certificate == updatedCertificate) 
                {
                    Assert.AreEqual(updatedCertificate, certificate.Certificate, "Actual and update certificate do not match");
                    test.Log(Status.Pass, "Certificate updated successfully");
                }
             
            }
        }

        [Test,Order(3)]
        public void DeleteCertificationTest()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\DeleteCertificateTestData.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);
            foreach(var certificate in testData)
            {
                certificationPageObject.AddCertification(certificate);
            }
           
            foreach (var certificate in testData)
            {
                string certificationName = certificate.Certificate;

                certificationPageObject.DeleteCertification(certificate);
                
                string screenshotPath = CaptureScreenshot(driver, "DeleteCertification");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
               
                //verifying deleted message
                string expectedMessage = certificationName + " has been deleted from your certification";
                string actualMessage = certificationPageObject.GetDeletedMessage();
                Assert.AreEqual(expectedMessage, actualMessage, "Actual and expected message do not match");

                //Verifying if record is deleted
                string deleteCertificateResult = certificationPageObject.GetDeletedCertification(certificate);
                Assert.AreEqual("Deleted", deleteCertificateResult, "Actual and expected message do not match. Certificate not deleted");
                test.Pass("Certificate Deleted");
            }
        }

        [Test,Order(4)]
        public void AddEmptyFieldTest()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\AddEmptyCertificateTestData.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());

            foreach (var certificate in testData)
            {
                string screenshotPath = CaptureScreenshot(driver, "AddEmptyCertificationField");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                certificationPageObject.AddEmptyCertificationField(certificate);

                string actualErrorMessage = "Please enter Certification Name, Certification From and Certification Year";

                string expectedErrorMessage = certificationPageObject.GetEmptyFieldErrorMessage();

                Assert.AreEqual(expectedErrorMessage, actualErrorMessage,"Actual and expected messages do not match");
                test.Pass("Test Pass. Error message"+actualErrorMessage+"displayed");
            }
        }
        [Test,Order(5)]
        public void SameCertificateSameYearTest()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\SameCertificateSameYearTestData.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

         
            foreach (var certificate in testData)
            {
                
                string screenshotPath = CaptureScreenshot(driver, "AddSameCertificationSameYear");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                certificationPageObject.AddSameCertificationSameYear(certificate);

                string actualErrorMessage = "This information is already exist.";

                string expectedErrorMessage = certificationPageObject.GetSameCertificationSameYearErrorMessage();

                Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "Actual and expected messages do not match");
                test.Pass("Test Pass. Error message displayed and user is not able to add");
            }
        }
        [Test,Order(6)]
        public void SameCertificateDifferentYearTest()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\SameCertificateDifferentYear.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());

            foreach (var certificate in testData)
            {
               
                string screenshotPath = CaptureScreenshot(driver, "AddSameCertificationDifferentYear");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                certificationPageObject.AddSameCertificationDifferentYear(certificate);

                string actualErrorMessage = "Duplicated data";

                string expectedErrorMessage = certificationPageObject.GetSameCertificationDifferentYearErrorMessage();

                Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "Actual and expected messages do not match");
                test.Pass("Test Pass.Error message displayed and user is not able to add.");
            }
        }

        [Test,Order(7)]
        public void UpdateNoChangeTest()
        {
            CertificationPage certificatePageObject = new CertificationPage();

            
            string screenshotPath = CaptureScreenshot(driver, "UpdateCertificateNoChange");
            test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

            certificatePageObject.UpdateCertificateNoChange();

            string actualerrorMessage = certificatePageObject.GetUpdateNoChangeErrorMessage();
            string expectedMessage = "This information is already exist.";

            Assert.AreEqual(expectedMessage, actualerrorMessage, "Expected and actual message do not match");
            test.Pass("Test Pass");
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