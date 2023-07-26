using CompetitionMars.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionMars.Pages
{
    public class LoginPage : CommonDriver

    {
        private static IWebElement signInButton => driver.FindElement(By.XPath("//*[@id=\"home\"]/div/div/div[1]/div/a"));
        private static IWebElement emailTextbox => driver.FindElement(By.Name("email"));
        private static IWebElement passwordTextbox => driver.FindElement(By.Name("password"));
        private static IWebElement loginButton => driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div[4]"));

        public void LoginSteps(string email, string password)
        {
            //Launch the Application
            driver.Navigate().GoToUrl("http://localhost:5000/");
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);


            //Click on Sign In button

            signInButton.Click();
            Wait.WaitToBeClickable(driver, "XPath", "//*[@id=\"home\"]/div/div/div[1]/div/a", 7);

            //Enter valid Username and Password

           emailTextbox.SendKeys(email);
            passwordTextbox.SendKeys(password);

            //Sign In using Login Button

            loginButton.Click();
          
            Wait.WaitToBeClickable(driver, "XPath", "/html/body/div[2]/div/div/div[1]/div/div[4]", 7);
        }

    }
}
