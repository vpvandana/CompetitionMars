using CompetitionMars.DataModel;
using CompetitionMars.Pages;
using CompetitionMars.Utilities;
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
    public class EducationTests : CommonDriver
    {
        private LoginPage loginPageObject = new LoginPage();
        private EducationPage educationPageObject = new EducationPage();
        private JsonHelper jsonHelperObject = new JsonHelper();
        private List<Education> testData = new List<Education>();

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

        [Test]
        public void TestAddEducationWithTestData()
        {
            EducationPage educationPageObject = new EducationPage();
            
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\addEducationTestData.json";
            
            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);
            
            Console.WriteLine(testData.ToString());

            foreach(var education in testData) 
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

                educationPageObject.AddEducation(collegeName,country,title,degree,year);

                string addedTitle = educationPageObject.GetAddedEducation();

                if(education.Title == addedTitle)
                {
                    Assert.AreEqual(education.Title, addedTitle, "Acual and expected education do not match");
                }
               /* else
                {
                    Console.WriteLine("Error in data");
                }*/
            }

           /* Assert.AreEqual(testData.Count, addedEducationDetails.Count,"No.of added education entries do not match");

            for(int i = 0; i < testData.Count; i++)
            {
                Assert.AreEqual(testData[i].CollegeName, addedEducationDetails[i].CollegeName, "College name does not match" + (i + 1));
                Assert.AreEqual(testData[i].Country, addedEducationDetails[i].Country,"Country does not match" +(i + 1));
                Assert.AreEqual(testData[i].Title, addedEducationDetails[i].Title,"title does not not match"+(i + 1));
                Assert.AreEqual(testData[i].Degree, addedEducationDetails[i].Degree,"Degree does not match"+(i + 1));
                Assert.AreEqual(testData[i].Year, addedEducationDetails[i].Year,"year does not match"+(i + 1));
            }*/
          //  EducationPage educationPageObject = new EducationPage();
           
         
           /* foreach(var education in testData) 
            {
                    
            
                educationPageObject.AddEducation(education);
                string addedEducation = educationPageObject.GetEducation(education);
                if(addedEducation == education.CollegeName) 
                {
                    Assert.AreEqual(education.CollegeName, addedEducation,"Actual college name and expected name are not equal");
                }
                else
                {
                    Console.WriteLine("Not added");
                }
               List<List<string>> tabledata = educationPageObject.GetEducationTableData();

                bool foundEducation = false;
                foreach(var row in tabledata)
                {
                    if(row.Contains(education.CollegeName) && row.Contains(education.Country) && row.Contains(education.Title) && row.Contains(education.Degree) && row.Contains(education.Year))
                    { 
                        foundEducation = true;
                        break;
                    }
                }

                Assert.IsTrue(foundEducation, "Added education details not found in table"); */

            }
        [Test]
        public void TestUpdateEducation()
        {
            EducationPage educationPageObject = new EducationPage();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\updateEducationTestData.json";
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

                // educationPageObject.UpdateEducation(CollegeName,Country, Title, Degree, Year);

                List<Education> updatedEducation = educationPageObject.GetUpdatedEducation();

                //updatedEducation.Add(education);
                Assert.AreEqual(testData.Count, updatedEducation.Count, "Number of education entries mismatch.");

                for (int i = 0; i < testData.Count; i++)
                {
                    Assert.AreEqual(testData[i].Country, updatedEducation[i].Country, "country mismatch for entry " + i);
                    Assert.AreEqual(testData[i].CollegeName, updatedEducation[i].CollegeName, "college name mismatch for entry " + i);
                    Assert.AreEqual(testData[i].Title, updatedEducation[i].Title, "title mismatch for entry " + i);
                    Assert.AreEqual(testData[i].Degree, updatedEducation[i].Degree, "Degree mismatch for entry " + i);
                    Assert.AreEqual(testData[i].Year, updatedEducation[i].Year, "Year mismatch for entry " + i);
                }

                //Assert.AreEqual(testData, updatedEducation, "Actual and expected education do not match");
            }

        }
        [Test]
        public void DeleteEducationTest()
        {
            EducationPage educationPageObject = new EducationPage();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\updateEducationTestData.json";
            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

        }

        [TearDown]
    public void TearDownActions()
        {
            driver.Quit();
        }
    }

    
}
