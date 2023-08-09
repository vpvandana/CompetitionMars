using CompetitionMars.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CompetitionMars.DataModel;
using System.Reflection.Emit;
using System.Collections.ObjectModel;

namespace CompetitionMars.Pages
{
    public class EducationPage : CommonDriver
    {
        private static IWebElement addNewButton => driver.FindElement(By.XPath("//*[text()='Country']//parent::tr//child::th[6]//div"));
        private static IWebElement universityNameTextbox => driver.FindElement(By.Name("instituteName"));
        private static IWebElement countryDropdown => driver.FindElement(By.Name("country"));
        private static IWebElement titleDropdown => driver.FindElement(By.Name("title"));
        private static IWebElement degreeTextbox => driver.FindElement(By.Name("degree"));
        private static IWebElement yearOfGraduationDropdown => driver.FindElement(By.Name("yearOfGraduation"));
        private static IWebElement addButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        private static IWebElement cancelBButton => driver.FindElement(By.XPath("//input[@value='Cancel']"));
        private static IWebElement educationTab => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[1]/a[3]"));
        
        private static IWebElement addedTitle = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[3][last()]"));
        //private static IWebElement addedDegree = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[4][last()]"));
        //private static IWebElement addedYear = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[5][last()]"));
        private static IWebElement removeIcon => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[1]/tr/td[6]/span[2]/i"));
        private static IWebElement updateIcon => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[1]/i"));
        private static IWebElement updateButton => driver.FindElement(By.XPath("//input[@value='Update']"));

        public void AddEducation(string CollegeName, string Country, string Title, string Degree, string Year)
        {
            
            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 7);
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addNewButton.Click();

            universityNameTextbox.SendKeys(CollegeName);
            countryDropdown.SendKeys(Country);
            titleDropdown.SendKeys(Title);
            degreeTextbox.SendKeys(Degree);
            yearOfGraduationDropdown.SendKeys(Year);

           addButton.Click();
                //  Thread.Sleep(2000)
          driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
                     
        }

        public string GetAddedEducation()
        {
            return addedTitle.Text;     
        }

    public void UpdateEducation(string CollegeName, string Country, string Title, string Degree, string Year)
        {
            educationTab.Click();

            updateIcon.Click();
            universityNameTextbox.Clear();
            universityNameTextbox.SendKeys(CollegeName);

                
            countryDropdown.SendKeys(Country);
            titleDropdown.SendKeys(Title);
            degreeTextbox.Clear();
            degreeTextbox.SendKeys(Degree);
            yearOfGraduationDropdown.SendKeys(Year);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            updateButton.Click();
            
        }

        public string GetUpdatedEducation()
        {
           
            IWebElement updatedDegree = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[4]"));
            return updatedDegree.Text;
            
        }

        public void DeleteEducation(string Degree)
        {
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
            JsonHelper jsonHelperObject = new JsonHelper();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\DeleteEducationTestData.json";
            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

            Console.WriteLine(testData.ToString());

            foreach (var education in testData)
            {
                string degree = education.Degree;
                //Console.WriteLine(degree);

                IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead//following-sibling::tbody[last()]/tr"));
                foreach (IWebElement row in rows)
                {
                    IWebElement degreeElement = row.FindElement(By.XPath("./td[4]"));
                    string degreetodelete = degreeElement.Text;

                    Console.WriteLine(degreetodelete);
                    if(degreetodelete == education.Degree)
                    {

                        IWebElement deleteIcon = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[6]/span[2]/i"));
                        deleteIcon.Click();
                       // Thread.Sleep(3000);
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                    }
                 
                }
            }

          driver.Navigate().Refresh();    
        }

        public string GetDeleteEducation()
        {
            JsonHelper jsonHelperObject = new JsonHelper();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\DeleteEducationTestData.json";
            List<Education> testData = jsonHelperObject.ReadTestDataFromJson(jsonFilePath);

            string result = "";
            Console.WriteLine(testData.ToString());
            foreach (var education in testData)
            {
                string degree = education.Degree;
                string title = education.Title;
               

                IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead//following-sibling::tbody[last()]/tr"));
                foreach (IWebElement row in rows)
                {

                    IWebElement titleElement = row.FindElement(By.XPath("./td[3]"));
                    IWebElement degreeElement = row.FindElement(By.XPath("./td[4]"));

                    string deletedTitle = titleElement.Text;
                    string deletedDegree = degreeElement.Text;

                    if ((deletedDegree != education.Degree) && (deletedTitle != education.Title))
                    {
                        result = "Deleted";
                        
                        break;
                    }
                    else
                    {
                        result = "Not deleted";
                    }
                }

            }
            Console.WriteLine("Education Deleted");
            return result;
        }
        public string GetDeleteMessage()
        {
            IWebElement actualMessage = driver.FindElement(By.XPath("//div[text()='Education entry successfully removed']"));
            return actualMessage.Text;
        }

        public void AddEmptyEducationField(string CollegeName, string Country, string Title, string Degree, string Year)
        {
            

            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 7);
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addNewButton.Click();

            universityNameTextbox.SendKeys(CollegeName);
            countryDropdown.SendKeys(Country);
            titleDropdown.SendKeys(Title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            degreeTextbox.SendKeys(Degree);
            yearOfGraduationDropdown.SendKeys(Year);

            addButton.Click();
            //  Thread.Sleep(2000);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

        }

        public string GetEmptyFieldErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualErrorMessage.Text;
        }

        public void AddSameDegreeSameYear(string CollegeName, string Country, string Title, string Degree, string Year)
        {


            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 7);
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addNewButton.Click();

            universityNameTextbox.SendKeys(CollegeName);
            countryDropdown.SendKeys(Country);
            titleDropdown.SendKeys(Title);
            degreeTextbox.SendKeys(Degree);
            yearOfGraduationDropdown.SendKeys(Year);

            addButton.Click();
            //  Thread.Sleep(2000);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        public string GetSameEducationDetailsErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualErrorMessage.Text;
        }

        public string GetSameDegreeDifferentYearErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualErrorMessage.Text;
        }

        public void UpdateEducationNoChange()
        {
            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 7);
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            updateIcon.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
            updateButton.Click();

        }

        public string GetUpdateNoChangeErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualErrorMessage.Text;
        }
       
    }
}
