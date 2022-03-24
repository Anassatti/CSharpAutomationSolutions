using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using OpenQA.Selenium.Interactions;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Chrome;
using SeleniumAutomationProject1.Utilities;
using SeleniumAutomationProject1.PageObjects;

namespace SeleniumAutomationProject1
{
    [Parallelizable(ParallelScope.Children)]
    public class Tests: Base
    {      
        [Test]
         
        public void Test()
        {
            LoginPage loginpage = new LoginPage(getDriver());
            //Explicit wait
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.Value.FindElement(By.XPath("//input[@type='submit']")),"Login"));
            //Add email
            loginpage.Login("anasssattis@gmail.com", "Amro@2015");

            //Verify the login
            IWebElement email = driver.Value.FindElement(By.XPath("//span[contains(text(),'Anas Satti')]"));
            // TestContext.Progress.WriteLine(driver.Title);
            Assert.AreEqual(email.Text, "Anas Satti");
            TestContext.Progress.WriteLine(email.Text);
        }
        [Test, Category("Regression")] // Execute select groups of test such as regression, smoke from commond line(C:\Users\anass\source\repos\SeleniumAutomationProject1)
        
        public void Dropdwon()
        {
         IWebElement dropdown= driver.Value.FindElement(By.CssSelector("select .form-control"));
            SelectElement select = new SelectElement(dropdown);
            select.SelectByValue("stud");
        }
        [Test]
        public void RadioButton()
        {
            IList <IWebElement> radio = driver.Value.FindElements(By.CssSelector("input[type='radio']"));
            // radio[1].Click();
           foreach(IWebElement radioButton in radio)
            {
                if(radio[1].GetAttribute("value").Equals("user"))
                {
                    radioButton.Click();
                }    
            }
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("usertype")));
            driver.Value.FindElement(By.Id("okayBtn"));
            //Check if the radio button selected
          Boolean check= driver.Value.FindElement(By.Id("usertype")).Selected;
            //check the result of selected radio button
            Assert.That(check, Is.True);
        }
        [Test, TestCaseSource("AddTestDataConfig"), Category("Smoke")]
       
        // [TestCase("rahulshettyacademy", "learning")]

        public void ecommerceWebsite(string username, string password, string[] productsList)
        {
            LoginPage loginpage = new LoginPage(getDriver());
           
            //List of products
           // string[] productsList = { "iphone X", "Blackberry" };
            //Actual products
            string[] actualproducts = new string[2];
            //Login
            ProductsPage products= loginpage.Login(username, password);
            //Implicitwait
            products.waitForVisible();
            IList<IWebElement> productlist = products.getProducts();
            foreach (IWebElement product in productlist)
            {

                if (productsList.Contains(product.FindElement(products.getProductTitle()).Text))
                {
                    product.FindElement(products.getCard()).Click();
                }               

            }
            CheckOutPage checkOut=products.getcheckOut();
            IList<IWebElement> checkoutcrads = checkOut.getcard();
            for (int i=0; i< checkoutcrads.Count;i++)
            {
                actualproducts[i]= checkoutcrads[i].Text;
            }
            Assert.AreEqual(actualproducts, productsList);
            checkOut.getCheckOutButton();
            ConfirmationPage confirmpage = new ConfirmationPage(driver.Value);
            confirmpage.getCountry();
            confirmpage.waitForCountryVisible();
            confirmpage.CountryChoice();
            confirmpage.termsCheckbox();
            confirmpage.purchase();
            confirmpage.verification();
        }

        [Test]
     
        public void SortWebTable()
        {
            ArrayList vegtableList = new ArrayList();
            SelectElement dropdown = new SelectElement(driver.Value.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");
            //Store all vegies in the array list

            IList<IWebElement> vegetables = driver.Value.FindElements(By.XPath("//tr/td[1]"));
            foreach(IWebElement veg in vegetables)
            {
                vegtableList.Add(veg.Text);

            }
            //Sort out arraylist
            vegtableList.Sort();
            foreach(string element in vegtableList)
            {
                TestContext.Progress.WriteLine(element);
            }
            ArrayList vegtableList2 = new ArrayList();
            //
            driver.Value.FindElement(By.CssSelector("th[aria-label *='fruit name']")).Click();
            IList<IWebElement> Sortvegetables = driver.Value.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veg in Sortvegetables)
            {
                vegtableList2.Add(veg.Text);

            }

            //verify arraylist 1&2 equals
            Assert.AreEqual(vegtableList, vegtableList2);
        }

        [Test]
    
        public void popUpHandling()
        {

            //Add name
            string name = "Anas";
            driver.Value.FindElement(By.XPath("//input[@id='name']")).SendKeys(name);
            //Click on confirmation
            driver.Value.FindElement(By.CssSelector("input[id='confirmbtn']")).Click();
            //Implicit wait
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            //Handle popup window
          string alerttext= driver.Value.SwitchTo().Alert().Text;
            driver.Value.SwitchTo().Alert().Accept();
            //Verify if the input matches
            StringAssert.Contains(name, alerttext);

        }
        [Test]
      
        public void Autosuggestion()
        {
            driver.Value.FindElement(By.XPath("//input[@id='autocomplete']")).SendKeys("Ind");
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            //select all elements
          IList<IWebElement> options= driver.Value.FindElements(By.CssSelector(".ui-menu-item div"));
            foreach(IWebElement option in options)
            {
               if( option.Text.Equals("India"))
                {
                    option.Click();
                }
            }
            //Grab value during runtime
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.Id("autocomplete")).GetAttribute("value"));
        }
        [Test]
   
        public void actions()
        {
            //Mouse hover
            /* Actions act = new Actions(driver);
             act.MoveToElement(driver.FindElement(By.XPath("//a[@class='dropdown-toggle']"))).Perform();

             act.MoveToElement(driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a"))).Click().Perform();*/

            //Drag & Drop 
            Actions act = new Actions(driver.Value);
            act.DragAndDrop(driver.Value.FindElement(By.Id("draggable")), driver.Value.FindElement(By.Id("droppable"))).Perform();
            
        }
        [Test]
    
        public void Frame()
        {
            //Scrol USING JAVA script
            IWebElement scrol = driver.Value.FindElement(By.Id("courses-iframe"));
           IJavaScriptExecutor js= (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", scrol);

            driver.Value.SwitchTo().Frame("courses-iframe");
            driver.Value.FindElement(By.XPath("/html/body/app-root/div/header/div[2]/div/div/div[2]/nav/div[2]/ul/li[3]/a")).Click();
          TestContext.Progress.WriteLine(driver.Value.FindElement(By.CssSelector("h1")).Text);
            driver.Value.SwitchTo().DefaultContent();
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.CssSelector("h1")).Text);

        }
        [Test]
      
        public void Childwindow()
        {
            string email= "mentor@rahulshettyacademy.com";
            string parentWindowHandle = driver.Value.CurrentWindowHandle;
            driver.Value.FindElement(By.CssSelector(".blinkingText")).Click();
            string Childwindowname = driver.Value.WindowHandles[1];
            driver.Value.SwitchTo().Window(Childwindowname);
          
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.CssSelector(".red")).Text);
            string text = driver.Value.FindElement(By.CssSelector(".red")).Text;
          string[] splittedtext=  text.Split("at");
          string[] trimestring= splittedtext[1].Trim().Split(" ");
            Assert.AreEqual(email, trimestring[0]);
            driver.Value.SwitchTo().Window(parentWindowHandle);
            //Add email
            driver.Value.FindElement(By.Id("username")).SendKeys(trimestring[0]);
          
        }

        //Testcases Source for many test data
        public static IEnumerable<TestCaseData>  AddTestDataConfig()
        {
          yield return  new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData("rahulshettyacademy", "learning");
            yield return new TestCaseData("rahulshettyacademy", "learning");
        }

      
    }
}