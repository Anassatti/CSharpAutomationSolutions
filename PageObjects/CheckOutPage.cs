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
    [NUnit.Framework.Parallelizable(ParallelScope.Self)]
    public class CheckOutPage
    {
        public CheckOutPage()
        {

        }

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> checkOutCards;


        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        private IWebElement checkOutButton;

        public IList<IWebElement> getcard()
        {
            return checkOutCards;
        }
        public void getCheckOutButton()
        {
            checkOutButton.Click();
        }
    }
}
