using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomationProject1.PageObjects
{
    [Parallelizable(ParallelScope.Self)]
    public class LoginPage
    {

        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            // To provide driver to all FindsBy page factory
            PageFactory.InitElements(driver, this);
        }



        //PageFactory
        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement email;
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement password;
        [FindsBy(How = How.XPath, Using = "//input[@type='submit']")]
        private IWebElement loginButton;


        public ProductsPage Login(string name, string pass)
        {
            username.SendKeys(name);
            password.SendKeys(pass);
            loginButton.Click();
            return new ProductsPage(driver);

        }
    }
}
