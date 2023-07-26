using CompetitionMars.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

     public void AddEducation(string collegeName, string country, string title, string degree, string year)
        {
           /* JsonReader jsonReaderObject = new JsonReader();
            jsonReaderObject.GetTestData();
            addNewButton.Click();

            universityNameTextbox.SendKeys(GetTestData(collegeName));*/

            addNewButton.Click();
            
            universityNameTextbox.SendKeys(collegeName);
            countryDropdown.SendKeys(country);
            titleDropdown.SendKeys(title);
            degreeTextbox.SendKeys(degree);
            yearOfGraduationDropdown.SendKeys(year);

            addButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);


        }
    }
}
