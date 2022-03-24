using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomationProject1.PageObjects
{
    [Parallelizable(ParallelScope.Self)]
    public  class ConfirmationPage
    {

        private IWebDriver driver;
        public ConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement choiceCountry;
        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement Country;
        [FindsBy(How = How.XPath, Using = "/html/body/app-root/app-shop/div/app-checkout/div[1]/div[2]/label")]
        private IWebElement termCheck;
        [FindsBy(How = How.XPath, Using = "//input[@type='submit']")]
        private IWebElement purchaseSubmit;
        [FindsBy(How = How.XPath, Using = ".alert-success")]
        private IWebElement confirmationMessage;
        public void getCountry()
        {
            choiceCountry.SendKeys("Ind");
        }

        public void waitForCountryVisible()
        {
            //Implicitwait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
        }

        public void CountryChoice()
        {
            Country.Click();
        }
        public void termsCheckbox()
        {
            termCheck.Click();
        }
        public void purchase()
        {
            purchaseSubmit.Click();
        }
        public void verification()
        {
            string confirmation = confirmationMessage.Text;
            StringAssert.Contains("Success", confirmation);
        }
    }
}
