using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CompetitionMars.Utilities
{
    public class Wait
    {
        public static void WaitToBeClickable(IWebDriver driver, string locatorType, string locatorValue, int time)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));

            if(locatorType == "Id")
            {
                wait.Until((SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id(locatorValue))));

            }
            if (locatorType == "Xpath")
            {
                wait.Until((SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(locatorValue))));

            }
            if (locatorType == "CssSelector")
            {
                wait.Until((SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector(locatorValue))));

            }

        }
        public static void WaitToExist(IWebDriver driver, string locatorType, string locatorValue, int time)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));

            if (locatorType == "Id")
            {
                wait.Until((SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id(locatorValue))));

            }
            if (locatorType == "Xpath")
            {
                wait.Until((SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(locatorValue))));

            }
            if (locatorType == "CssSelector")
            {
                wait.Until((SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(locatorValue))));

            }
           
        } 
        
      
    }
}
