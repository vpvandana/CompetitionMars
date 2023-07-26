using CompetitionMars.Pages;
using CompetitionMars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace CompetitionMars.Tests
{
    [TestFixture]
    public class LoginTests : CommonDriver
    {
        
        private LoginPage loginPageObject = new LoginPage();
        private JsonHelper jsonHelperObject = new JsonHelper();
        private List<LoginTestModel> testData = new List<LoginTestModel>();

        [SetUp] 
        public void SetUpActions() 
        {
            driver = new ChromeDriver();
            loginPageObject= new LoginPage();
            jsonHelperObject = new JsonHelper();

            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\loginTestData.json";
            testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);
        }
        [Test]
        public void TestLoginWithTestData()
        {
            foreach(var data in testData)
            {
                string email = data.Email;
                string password = data.Password;

                loginPageObject.LoginSteps(email, password);
                //Wait.WaitToExist(driver, "XPath", "//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span", 9);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                IWebElement userProfile = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span"));
               // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
               // Wait.WaitToExist(driver, "XPath", "//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span", 9);
               // Thread.Sleep(5000);
                string actualUsername = userProfile.Text;

                Assert.AreEqual(actualUsername, "Hi Van", "Actual and expected username do not match");

                
            }
        }
        [TearDown]
        public void TearDownActions() 
        {
            driver.Quit();
        }
    }
}
