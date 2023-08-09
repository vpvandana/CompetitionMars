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
            certificationPageObject = new CertificationPage();
            jsonHelperObject = new JsonHelper();

            loginPageObject.LoginSteps();

        }

        [Test]
        public void TestAddCertificationWithTestData()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\AddCertificateTestData.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());

            foreach (var certificate in testData)
            {
                string certificateName = certificate.Certificate;
                Console.WriteLine(certificateName);

                string certificateFrom = certificate.From;
                Console.WriteLine(certificateFrom);

                string certificateYear = certificate.CertificationYear;
                Console.WriteLine(certificateYear);

                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                string screenshotPath = CaptureScreenshot(driver, "AddCertification");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());

                certificationPageObject.AddCertification(certificateName, certificateFrom, certificateYear);

                string addedCertificate = certificationPageObject.GetAddedCertification();

                if (certificateName == addedCertificate)
                {
                   // Assert.AreEqual(certificate.Certificate, addedCertificate, "Acual and expected certification do not match");
                    test.Pass("Add certification test passed");
                   // break;
                }
               // else { test.Fail("Test Failed"); }
               
            }
        }
        [Test]

        public void TestUpdateCertification()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\UpdateCertificateTestData.json";
            string jsonData = File.ReadAllText(jsonFilePath);

            List<Certification> certificationTestData = JsonConvert.DeserializeObject<List<Certification>>(jsonData);
            Console.WriteLine(certificationTestData.ToString());

            foreach (var certificate in certificationTestData)
            {
                string certificateName = certificate.Certificate;
                Console.WriteLine(certificateName);

                string certificateFrom = certificate.From;
                Console.WriteLine(certificateFrom);

                string certificateYear = certificate.CertificationYear;
                Console.WriteLine(certificateYear);

                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                string screenshotPath = CaptureScreenshot(driver, "UpdateCertification");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());


                certificationPageObject.UpdateCertification(certificateName, certificateFrom, certificateYear);

                string updatedCertificate =  certificationPageObject.GetUpdatedCertification();   

                if (certificate.Certificate == updatedCertificate) 
                {
                    //Assert.AreEqual(updatedCertificate, certificate.Certificate, "Actual and update certificate do not match");
                    test.Log(Status.Pass, "Certificate updated successfully");
                }
              
                //Assert.AreEqual(testData, updatedEducation, "Actual and expected education do not match");
            }
        }

        [Test]
        public void DeleteCertificationTest()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\DeleteCertificateTestData.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

            foreach (var certificate in testData)
            {
                string certificateName = certificate.Certificate;
               
                string certificateFrom = certificate.From;
                //Console.WriteLine(certificateFrom);
                string certificateYear = certificate.CertificationYear;

                test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
                string screenshotPath = CaptureScreenshot(driver, "DeleteCertification");
                test.Log(Status.Info, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());


                certificationPageObject.DeleteCertification(certificateName);
                
                string deleteCertificateResult = certificationPageObject.GetDeletedCertification();

                Assert.AreEqual("Deleted", deleteCertificateResult, "Actual and expected message do not match. Certificate not deleted");
                test.Pass("Certificate Deleted");
            }
        }

        [Test]
        public void AddEmptyFieldTest()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\AddEmptyCertificateTestData.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());

            foreach (var certificate in testData)
            {
                string certificateName = certificate.Certificate;
                Console.WriteLine(certificateName);

                string certificateFrom = certificate.From;
                Console.WriteLine(certificateFrom);

                string certificateYear = certificate.CertificationYear;
                Console.WriteLine(certificateYear);

                certificationPageObject.AddEmptyCertificationField(certificateName,certificateFrom, certificateYear);

                string actualErrorMessage = "Please enter Certification Name, Certification From and Certification Year";

                string expectedErrorMessage = certificationPageObject.GetEmptyFieldErrorMessage();

                Assert.AreEqual(expectedErrorMessage, actualErrorMessage,"Actual and expected messages do not match");
            }
        }
        [Test]
        public void SameCertificateSameYearTest()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\SameCertificateSameYearTestData.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());

            foreach (var certificate in testData)
            {
                string certificateName = certificate.Certificate;
                Console.WriteLine(certificateName);

                string certificateFrom = certificate.From;
                Console.WriteLine(certificateFrom);

                string certificateYear = certificate.CertificationYear;
                Console.WriteLine(certificateYear);

                certificationPageObject.AddSameCertificationSameYear(certificateName, certificateFrom, certificateYear);

                string actualErrorMessage = "This information is already exist.";

                string expectedErrorMessage = certificationPageObject.GetSameCertificationSameYearErrorMessage();

                Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "Actual and expected messages do not match");
            }
        }
        [Test]
        public void SameCertificateDifferentYearTest()
        {
            CertificationPage certificationPageObject = new CertificationPage();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\SameCertificateDifferentYear.json";

            List<Certification> testData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());

            foreach (var certificate in testData)
            {
                string certificateName = certificate.Certificate;
                Console.WriteLine(certificateName);

                string certificateFrom = certificate.From;
                Console.WriteLine(certificateFrom);

                string certificateYear = certificate.CertificationYear;
                Console.WriteLine(certificateYear);

                certificationPageObject.AddSameCertificationDifferentYear(certificateName, certificateFrom, certificateYear);

                string actualErrorMessage = "Duplicated data";

                string expectedErrorMessage = certificationPageObject.GetSameCertificationDifferentYearErrorMessage();

                Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "Actual and expected messages do not match");
            }
        }

        [Test]
        public void UpdateNoChangeTest()
        {
            CertificationPage certificatePageObject = new CertificationPage();
            certificatePageObject.UpdateCertificateNoChange();

            string actualerrorMessage = certificatePageObject.GetUpdateNoChangeErrorMessage();
            string expectedMessage = "This information is already exist.";

            Assert.AreEqual(expectedMessage, actualerrorMessage, "Expected and actual message do not match");
        }



        [TearDown]
            public void TearDownActions()
            {
                driver.Quit();
                extent.Flush();
            }
        private string CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            string screenshotPath = Path.Combine(@"C:\internship notes\CompetitionMars\CompetitionMars\CompetitionMars\CompetitionMars\CompetitionScreenshot\", $"{screenshotName}_{DateTime.Now:yyyyMMddHHmmss}.png");
            screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
            return screenshotPath;
        }
        [OneTimeTearDown]
        public void ExtentTeardown()
        {
            extent.Flush();
        }

    }
}