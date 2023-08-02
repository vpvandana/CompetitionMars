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
        private static IWebElement removeIcon => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[6]/span[2]/i"));
        private static IWebElement updateIcon => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[last()]/tr/td[6]/span[1]/i"));
        private static IWebElement updateButton => driver.FindElement(By.XPath("//input[@value='Update']"));

        public void AddEducation(string CollegeName, string Country, string Title, string Degree, string Year)
        {
            /* JsonReader jsonReaderObject = new JsonReader();
             jsonReaderObject.GetTestData();
             addNewButton.Click();

             universityNameTextbox.SendKeys(GetTestData(collegeName));*/
            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 7);
            educationTab.Click();
            
            
            
           driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            //XPath of table
  
               
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

        public string GetAddedEducation()
        {
            return addedTitle.Text;     
        }

    public void UpdateEducation(string CollegeName, string Country, string Title, string Degree, string Year)
        {
            educationTab.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            //find the row in table

            //IWebElement row = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[1]/tr"));

           // if (row != null) 
            
           // {
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
                Thread.Sleep(3000);
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15); 


           // }

           // else 
           // {
              //  Console.WriteLine("Not found");
            //}
        }

        public List<Education> GetUpdatedEducation()
        {
            List<Education> educationData = new List<Education>();
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead//following-sibling::tbody[last()]/tr"));
           // string result = "";
            foreach (IWebElement row in rows)
            {
                IWebElement countryElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement collegeNameElement = row.FindElement(By.XPath("./td[2]"));
                IWebElement titleElement = row.FindElement(By.XPath("./td[3]"));
                IWebElement degreeElement = row.FindElement(By.XPath("./td[4]"));
                IWebElement yearElement = row.FindElement(By.XPath("./td[5]"));

                string country = countryElement.Text;
                string collegeName = collegeNameElement.Text;
                string title = titleElement.Text;
                string degree = degreeElement.Text;
                string year = yearElement.Text;
                //  if (country.Equals(Country, StringComparison.OrdinalIgnoreCase) && collegeName.Equals(CollegeName, StringComparison.OrdinalIgnoreCase) && title.Equals(Title, StringComparison.OrdinalIgnoreCase) && degree.Equals(Degree, StringComparison.OrdinalIgnoreCase) && year.Equals(Year, StringComparison.OrdinalIgnoreCase))
                educationData.Add(new Education
                {
                    Country = country,
                    CollegeName = collegeName,
                    Title = title,
                    Degree = degree,
                    Year = year
                });
            }

            return educationData; 
            
        }

        public void DeleteEducation(string Degree, string Title)
        {
            IWebElement educationRow = GetDeleteEducation(Degree, Title);
            if (educationRow != null) 
            {
                IWebElement deleteIcon = educationRow.FindElement(By.XPath("//tbody[1]/tr[1]/td[6]/span[2]/i[1]"));
                deleteIcon.Click();
            }
        }

        public IWebElement  GetDeleteEducation(string Degree, string Title)
        {
            ReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Country']//ancestor::thead//following-sibling::tbody[last()]/tr"));
            foreach (IWebElement row in rows)
            {
                IWebElement collegeNameElement = row.FindElement(By.XPath(".\td[2]"));
                IWebElement countryElement = row.FindElement(By.XPath(".\td[1]"));
                IWebElement titleElement = row.FindElement(By.XPath(".\td[3]"));
                IWebElement degreeElement = row.FindElement(By.XPath(".\td[4]"));
                IWebElement yearElement = row.FindElement(By.XPath(".\td[5]"));
                string collegeName = collegeNameElement.Text;
                string country = countryElement.Text;
                string title = titleElement.Text;
                string degree = degreeElement.Text;
                string year = yearElement.Text;
               
                

                if (degree.Equals(Degree, StringComparison.OrdinalIgnoreCase) && title.Equals(Title, StringComparison.OrdinalIgnoreCase))
                {
                    return row;
                    
                }
            }
            return null;
        }
    }
}
