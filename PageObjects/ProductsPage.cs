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
    public class ProductsPage
    {
        private IWebDriver driver;
        By cardTitle = By.CssSelector(".card-title a");
        By addToCard = By.CssSelector(".card-footer button");
        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }




        [FindsBy(How = How.CssSelector, Using = "app-card")]
        private IList<IWebElement> products;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkOut;

        public void waitForVisible()
        {
            //Implicitwait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }

        public IList<IWebElement> getProducts()
        {

            return products;
        }

        public By getProductTitle()
        {

            return cardTitle;
        }
        public By getCard()
        {

            return addToCard;
        }
        public CheckOutPage getcheckOut()
        {

            checkOut.Click();
            return new CheckOutPage();
        }


    }
}
