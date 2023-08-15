using CompetitionMars.DataModel;
using CompetitionMars.Utilities;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionMars.Pages
{
    public class CertificationPage : CommonDriver
    {
        private static IWebElement addNewButton => driver.FindElement(By.XPath("//th[text()='Certificate']//parent::tr//child::div"));
        private static IWebElement certificateTextbox => driver.FindElement(By.Name("certificationName"));
        private static IWebElement certificateFromTextbox => driver.FindElement(By.Name("certificationFrom"));
        private static IWebElement certificateYearDropdown => driver.FindElement(By.Name("certificationYear"));
        private static IWebElement addButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        private static IWebElement updateIcon => driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[5]/div[1]/div[2]/div[1]/table[1]/tbody[last()]/tr[1]/td[4]/span[1]/i[1]"));
        private static IWebElement updateButton => driver.FindElement(By.XPath("//input[@value='Update']"));
        private static IWebElement removeIcon => driver.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/section[2]/div[1]/div[1]/div[1]/div[3]/form[1]/div[5]/div[1]/div[2]/div[1]/table[1]/tbody/tr[1]/td[4]/span[2]/i[1]"));
        private static IWebElement cancelButton => driver.FindElement(By.XPath("//input[@value='Cancel']"));
        private static IWebElement certificationTab => driver.FindElement(By.XPath("//a[@data-tab='fourth']"));
        private static IWebElement addedCertificate => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[last()]/tr/td[1]"));

        public void SendKeysToInputField(Certification certificate)
        {
            certificateTextbox.Clear();
            certificateTextbox.SendKeys(certificate.Certificate);

            certificateFromTextbox.Clear();
            certificateFromTextbox.SendKeys(certificate.From);
            certificateYearDropdown.SendKeys(certificate.CertificationYear);

        }
        public void ClearExistingEntries() 
        {
            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='fourth']", 15);
            certificationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead//following-sibling::tbody/tr"));
            if (rows.Count > 0)
            {
                foreach (IWebElement row in rows)
                {
                    IWebElement deleteIcon = row.FindElement(By.XPath("./td[4]/span[2]/i"));
                    deleteIcon.Click();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                }
            }
        }
        public void AddCertification(Certification certificate)
        {

            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='fourth']", 15);
            certificationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
          
            addNewButton.Click();

            SendKeysToInputField(certificate);
            
            addButton.Click();
            
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
          
        }

        public string GetAddedCertification(Certification certificate)
        {
            Thread.Sleep(1000);          
            string result = "";
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead//following-sibling::tbody[last()]/tr"));
            
            foreach (IWebElement row in rows) 
            {
                IWebElement certificateNameElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement certificateFromElement = row.FindElement(By.XPath("./td[2]"));
                string addedCertificateName = certificateNameElement.Text;
                string addedCertificateFrom = certificateFromElement.Text;

                if(addedCertificateName.Equals(certificate.Certificate) && addedCertificateFrom.Equals(certificate.From))
                {
                    result = addedCertificateName;
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
       
        public void UpdateCertification(Certification certificate)
        {
            certificationTab.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            updateIcon.Click();

            SendKeysToInputField(certificate);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            updateButton.Click();
           
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 

            driver.Navigate().Refresh();

        }

        public string GetUpdatedCertification(Certification certificate)
        {
            // IWebElement updatedCertificate = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody/tr/td[1]"));
            //return updatedCertificate.Text;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            string result = "";
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead//following-sibling::tbody/tr"));

            foreach (IWebElement row in rows)
            {
                IWebElement certificateNameElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement certificateFromElement = row.FindElement(By.XPath("./td[2]"));

                string updatedCertificateName = certificateNameElement.Text;
                string updatedCertificateFrom = certificateFromElement.Text;

                if ((updatedCertificateName == certificate.Certificate) && (updatedCertificateFrom == certificate.From))
                {
                    result = updatedCertificateName;
                    break;
                }

            }

            return result;

        }

    
        public void DeleteCertification(Certification certificate)
        {
           
            certificationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
           
            Wait.WaitToExist(driver, "XPath", "//th[text()='Certificate']//ancestor::thead//following-sibling::tbody[last()]/tr", 7);

            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead//following-sibling::tbody/tr"));
            foreach (IWebElement row in rows) 
            {
                 driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
                 IWebElement certificateNameElement = row.FindElement(By.XPath("./td[1]"));
                 string certificateToDelete = certificateNameElement.Text;

                    if(certificateToDelete == certificate.Certificate) 
                    {
                        IWebElement deleteIcon = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[last()]/tr/td[4]/span[2]/i"));
                        deleteIcon.Click();
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                      
                      
                    }
            }          

        }

        public string GetDeletedCertification(Certification certificate)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            string result = "";
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead//following-sibling::tbody[last()]/tr"));

            foreach (IWebElement row in rows)
            {
                IWebElement certificateNameElement = row.FindElement(By.XPath("./td[1]"));
                IWebElement certificateFromElement = row.FindElement(By.XPath("./td[2]"));

                string deletedCertificateName = certificateNameElement.Text;
                string deletedCertificateFrom = certificateFromElement.Text;

                if ((deletedCertificateName != certificate.Certificate) && (deletedCertificateFrom != certificate.From))
                {
                    result = "Deleted";
                    break;
                }
              
            }
                
            return result;
        }

        public string GetDeletedMessage()
        {
            IWebElement deleteMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return deleteMessage.Text;
        }

        public void AddEmptyCertificationField(Certification certificate)
        {
            certificationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addNewButton.Click();
            SendKeysToInputField(certificate);
            addButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
        }

        public string GetEmptyFieldErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualErrorMessage.Text;
        }

        public void AddSameCertificationSameYear(Certification certificate)
        {
            certificationTab.Click();
            
            addNewButton.Click();
            SendKeysToInputField(certificate);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
            addButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
        }

        public string GetSameCertificationSameYearErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualErrorMessage.Text;
        }
        public void AddSameCertificationDifferentYear(Certification certificate)
        {
            certificationTab.Click();
            addNewButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
            SendKeysToInputField(certificate);

            addButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

        }
        public string GetSameCertificationDifferentYearErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box ns-growl ns-effect-jelly ns-type-error ns-show']"));
            return actualErrorMessage.Text;
        }

        public void UpdateCertificateNoChange()
        {
            certificationTab.Click();

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
