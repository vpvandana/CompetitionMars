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


        public void AddCertification(string Certificate, string From, string CertificationYear)
        {

            Wait.WaitToBeClickable(driver, "XPath", "//*[@data-tab='third']", 15);
            certificationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addNewButton.Click();

            certificateTextbox.SendKeys(Certificate);
            certificateFromTextbox.SendKeys(From);
            certificateYearDropdown.SendKeys(CertificationYear);
            
            addButton.Click();
            Console.WriteLine("Certificate added");
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        public string GetAddedCertification()
        {
            return addedCertificate.Text;
        }

        public void UpdateCertification(string Certificate, string From, string CertificationYear)
        {
            certificationTab.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            //find the row in table

            //IWebElement row = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody[1]/tr"));

            // if (row != null) 

            // {
            updateIcon.Click();
            certificateTextbox.Clear();
            certificateTextbox.SendKeys(Certificate);

            certificateFromTextbox.Clear();
            certificateFromTextbox.SendKeys(From);

            certificateYearDropdown.SendKeys(CertificationYear);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            updateButton.Click();
            //Thread.Sleep(3000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 

            driver.Navigate().Refresh();


            // }

            // else 
            // {
            //  Console.WriteLine("Not found");
            //}
        }

        public string GetUpdatedCertification()
        {
            IWebElement updatedCertificate = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody/tr/td[1]"));
            return updatedCertificate.Text;

        }

        public void DeleteCertification(string Certificate)
        {
           
            certificationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
            JsonHelper jsonHelperObject = new JsonHelper();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\DeleteCertificateTestData.json";
            
            List<Certification> certificationTestData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);

            Console.WriteLine(certificationTestData.ToString());

            foreach (var certificate in certificationTestData)
            {
                string certificateName = certificate.Certificate;
                //  Console.WriteLine(certificateName);
                IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead//following-sibling::tbody/tr"));
                foreach (IWebElement row in rows) 
                {
                    IWebElement certificateNameElement = row.FindElement(By.XPath("./td[1]"));
                    string certificateToDelete = certificateNameElement.Text;

                    if(certificateToDelete == certificate.Certificate) 
                    {
                        IWebElement deleteIcon = driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/form/div[5]/div[1]/div[2]/div/table/tbody[1]/tr/td[4]/span[2]/i"));
                        deleteIcon.Click();
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
                       driver.Navigate().Refresh();
                      
                    }
                }

            }          

        }

        public string GetDeletedCertification()
        {
            JsonHelper jsonHelperObject = new JsonHelper();
            string jsonFilePath = "C:\\internship notes\\CompetitionMars\\CompetitionMars\\CompetitionMars\\CompetitionMars\\TestData\\DeleteCertificateTestData.json";
            
            List<Certification> certificationTestData = jsonHelperObject.ReadCertificateTestDataFromJson(jsonFilePath);
            // List<Certification> certificationTestData = JsonConvert.DeserializeObject<List<Certification>>(jsonData);
            string result = "";

            foreach(var certificate in certificationTestData)
            {
                string certificateName = certificate.Certificate;
                string certificateFrom = certificate.From;

                IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//th[text()='Certificate']//ancestor::thead//following-sibling::tbody/tr"));

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
                    else
                    {
                        result = "Not Deleted";
                    }
                    

                }
            }
           
            return result;
            //  if (country.Equals(Country, StringComparison.OrdinalIgnoreCase) && collegeName.Equals(CollegeName, StringComparison.OrdinalIgnoreCase) && title.Equals(Title, StringComparison.OrdinalIgnoreCase) && degree.Equals(Degree, StringComparison.OrdinalIgnoreCase) && year.Equals(Year, StringComparison.OrdinalIgnoreCase))

        }

        public void AddEmptyCertificationField(string Certificate, string From, string CertificationYear)
        {
            certificationTab.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);

            addNewButton.Click();

            certificateTextbox.SendKeys(Certificate);
            certificateFromTextbox.SendKeys(From);
            certificateYearDropdown.SendKeys(CertificationYear);

            addButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
        }

        public string GetEmptyFieldErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualErrorMessage.Text;
        }

        public void AddSameCertificationSameYear(string Certificate,string From, string CertificationYear)
        {
            certificationTab.Click();
            addNewButton.Click();

            certificateTextbox.SendKeys(Certificate);
            certificateFromTextbox.SendKeys(From);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
            certificateYearDropdown.SendKeys(CertificationYear);

            addButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
        }

        public string GetSameCertificationSameYearErrorMessage()
        {
            IWebElement actualErrorMessage = driver.FindElement(By.XPath("//div[@class='ns-box-inner']"));
            return actualErrorMessage.Text;
        }
        public void AddSameCertificationDifferentYear(string Certificate,string From,string CertificationYear)
        {
            certificationTab.Click();
            addNewButton.Click();

            certificateTextbox.SendKeys(Certificate);   
            certificateFromTextbox.SendKeys(From);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(9);
            certificateYearDropdown.SendKeys(CertificationYear);

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
