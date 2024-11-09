using System;
using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CSProject.Tests
{
    [TestFixture]
    class SanityTest
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Size = new Size(1920, 1080);
            driver.Manage().Window.Position = new Point(620, 0);
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            Login("standard_user", "secret_sauce");
        }

        [Test]
        public void Test01_CountItems()
        {
            SelectAllItems();
            VerifyNumberOfItemsCart("6");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public void Login(string username, string password)
        {
            driver.FindElement(By.Id("user-name")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("login-button")).Click();
        }

        public void SelectAllItems()
        {
            var items = driver.FindElements(By.ClassName("inventory_item_name"));
            int numItems = items.Count;
            for (int i = 0; i < numItems; i++)
            {
                items[i].Click();
                driver.FindElement(By.CssSelector("button[class='btn btn_primary btn_small btn_inventory")).Click();
                driver.FindElement(By.Id("back-to-products")).Click();
                items = driver.FindElements(By.ClassName("inventory_item_name"));
            }
        }

        public void VerifyNumberOfItemsCart(String expected)
        {
            Assert.That(driver.FindElement(By.ClassName("shopping_cart_badge")).Text.Equals(expected));
        }
    }
}
