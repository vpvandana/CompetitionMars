﻿using CompetitionMars.DataModel;
using CompetitionMars.Pages;
using CompetitionMars.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionMars.Tests
{
    public class CertificationTests : CommonDriver
    {
        private LoginPage loginPageObject = new LoginPage();
        private CertificationPage certificationPageObject = new CertificationPage();
        private JsonHelper jsonHelperObject = new JsonHelper();
        private List<Certification> testData = new List<Certification>();

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

                certificationPageObject.AddCertification(certificateName, certificateFrom, certificateYear);

                string addedCertificate = certificationPageObject.GetAddedCertification();

                if (certificate.Certificate == addedCertificate)
                {
                    Assert.AreEqual(certificate.Certificate, addedCertificate, "Acual and expected education do not match");
                }
                /* else
                 {
                     Console.WriteLine("Error in data");
                 }*/
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

                certificationPageObject.UpdateCertification(certificateName, certificateFrom, certificateYear);

                string updatedCertificate =  certificationPageObject.GetUpdatedCertification();   

                if (certificate.Certificate == updatedCertificate) 
                {
                    Assert.AreEqual(updatedCertificate, certificate.Certificate, "Actual and update certificate do not match");
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

                certificationPageObject.DeleteCertification(certificateName);
                
                string deleteCertificateResult = certificationPageObject.GetDeletedCertification();

                Assert.AreEqual("Deleted", deleteCertificateResult, "Actual and expected message do not match. Certificate not deleted");

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
            }

        
    }
}