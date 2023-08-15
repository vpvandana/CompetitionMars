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
       
        private static IWebElement removeIcon => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[1]/tr/td[6]/span[2]/i"));
        private static IWebElement updateIcon => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[6]/span[1]/i"));
        private static IWebElement updateButton => driver.FindElement(By.XPath("//input[@value='Update']"));

        public void SendKeysToInputField(Education education)
        {
            universityNameTextbox.Clear();
            universityNameTextbox.SendKeys(education.CollegeName);

            countryDropdown.SendKeys(education.Country); 
            titleDropdown.SendKeys(education.Title);

            degreeTextbox.Clear();
            degreeTextbox.SendKeys(education.Degree);
            yearOfGraduationDropdown.SendKeys(education.Year);
        }

        public void ClearExistingEntries()
        {
            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 15);
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead//following-sibling::tbody/tr"));
            if (rows.Count > 0)
            {
                foreach (IWebElement row in rows)
                {
                    IWebElement deleteIcon = row.FindElement(By.XPath("./td[6]/span[2]/i"));
                    deleteIcon.Click();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
                }
            }
        }
        public void AddEducation(Education education)
        {
            
            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 7);
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addNewButton.Click();
            SendKeysToInputField(education);
            addButton.Click();
           
           driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
                     
        }

        public string GetAddedEducation(Education education)
        {
            Thread.Sleep(1000);
            string result = "";
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead//following-sibling::tbody[last()]/tr"));

            foreach (IWebElement row in rows)
            {
                IWebElement collegeNameElement = row.FindElement(By.XPath("./td[2]"));
                IWebElement countryElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement titleElement = row.FindElement(By.XPath("./td[3]"));
                string addedCollegeName = collegeNameElement.Text;
                string addedCountry = countryElement.Text;
                string addedTitle = titleElement.Text;

                if (addedCollegeName.Equals(education.CollegeName) && addedCountry.Equals(education.Country) && addedTitle.Equals(education.Title))
                {
                    result = addedCollegeName;
                    break;
                }
                else
                {
                    result = "Not Added";
                }

            }
            return result;
        }

        public string GetAddedMessage()
        {
            IWebElement actualMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualMessage.Text;
        }

        public void UpdateEducation(Education education)
        {
            educationTab.Click();

            updateIcon.Click();
            SendKeysToInputField(education);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            updateButton.Click();
            
        }

        public string GetUpdatedEducation(Education education)
        {

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            string result = "";

            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead//following-sibling::tbody[last()]/tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement countryElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement titleElement = row.FindElement(By.XPath("./td[3]"));
                IWebElement degreeElement = row.FindElement(By.XPath("./td[4]"));
                string updatedCountry = countryElement.Text;
                string updatedTitle = titleElement.Text;
                string updatedDegree = degreeElement.Text;

                if ((updatedCountry == education.Country) && (updatedDegree == education.Degree) && (updatedTitle == education.Title))
                {
                    result = updatedDegree;
                    break;
                }

            }

            return result;

        }

        public void DeleteEducation(Education education)
        {
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
           
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead//following-sibling::tbody[last()]/tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement collegeNameElement = row.FindElement(By.XPath("./td[2]"));
                IWebElement countryElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement titleElement = row.FindElement(By.XPath("./td[3]"));
                IWebElement degreeElement = row.FindElement(By.XPath("./td[4]"));
                IWebElement yearElement = row.FindElement(By.XPath("./td[5]"));

                string collegeNameDelete = collegeNameElement.Text;
                string countryDelete = countryElement.Text;
                string titleDelete = titleElement.Text;
                string degreetodelete = degreeElement.Text;
                string yearDelete = yearElement.Text;

               if(degreetodelete.Equals(education.Degree) && yearDelete.Equals(education.Year) && titleDelete.Equals(education.Title))
               {

                     IWebElement deleteIcon = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[6]/span[2]/i"));
                     deleteIcon.Click();  
                     driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
               }
               // driver.Navigate().Refresh(); 
            } 
        }

        public string GetDeleteEducation(Education education)
        {
            //educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            
            string result = "";
           
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead//following-sibling::tbody[last()]/tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement countryElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement titleElement = row.FindElement(By.XPath("./td[3]"));
                IWebElement degreeElement = row.FindElement(By.XPath("./td[4]"));
                string deletedCountry = countryElement.Text;
                string deletedTitle = titleElement.Text;
                string deletedDegree = degreeElement.Text;

                if ((deletedCountry != education.Country) && (deletedDegree != education.Degree) && (deletedTitle != education.Title))
                    {
                        result = "Deleted";  
                        break;
                    }
                   
            }

            return result;
        }
        public string GetDeletedMessage()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            IWebElement actualMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualMessage.Text;
        }

        public void AddEmptyEducationField(Education education)
        {
            

            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 7);
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addNewButton.Click();
            SendKeysToInputField(education);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addButton.Click();
          
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

        }

        public string GetEmptyFieldErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualErrorMessage.Text;
        }

        public void AddSameDegreeSameYear(Education education)
        {


            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 7);
            educationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addNewButton.Click();
            SendKeysToInputField(education); 
            addButton.Click();
   
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
