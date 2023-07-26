using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using CompetitionMars.Pages;

namespace CompetitionMars.Utilities
{
    public class CommonDriver
    {
        public static IWebDriver driver;
        
        public void Initialize()

        {
            //Lauch Chrome browser
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            //Login page object initialization and definition
           // LoginPage loginpage = new LoginPage();
            //loginpage.LoginSteps(driver);

            
        }
        public void close()
        {
            driver.Close();

        }
    }
}

