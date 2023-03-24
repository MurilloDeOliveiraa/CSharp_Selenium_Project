using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SauceDemo_Practicing.hooks;
using SeleniumExtras.PageObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo_Practicing.pages
{
    internal class ProductsPage
    {
        private IWebDriver Driver;

        public ProductsPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this.Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".product_sort_container")]
        private IWebElement dropDown;

        [FindsBy(How = How.XPath, Using = "//div[@class='inventory_item'][1]/div/div/button")]
        private IWebElement FirstItem;

        [FindsBy(How = How.XPath, Using = "//div[@class='inventory_item'][2]/div/div/button")]
        private IWebElement SecondItem;

        [FindsBy(How = How.CssSelector, Using = ".shopping_cart_link")]
        private IWebElement CartButton;


        public void WaitPageElement()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".title")));
        }

        public void SortProductsByPrice()
        {
            SelectElement s = new SelectElement(dropDown);
            s.SelectByValue("hilo");

            String expectedPrice = "$49.99";
            String currentPrice = Driver.FindElement(By.XPath("//div[@class='inventory_item'][1]/div/div/div[@class='inventory_item_price']")).Text;

            Assert.AreEqual(expectedPrice, currentPrice);



            Actions act = new Actions(Driver);
            IWebElement lastItemPrice = Driver.FindElement(By.XPath("//div[@class='inventory_item'][6]/div/div/div[@class='inventory_item_price']"));
            act.ScrollToElement(lastItemPrice).Perform();

            String expectedPrice2 = "$7.99";
            String currentPrice2 = Driver.FindElement(By.XPath("//div[@class='inventory_item'][6]/div/div/div[@class='inventory_item_price']")).Text;

            Assert.AreEqual(expectedPrice2, currentPrice2);
            Thread.Sleep(3000);

            IWebElement titleText = Driver.FindElement(By.CssSelector(".title"));
            act.ScrollToElement(titleText).Perform();
        }

        public void AdditensToCart()
        {
            FirstItem.Click();
            SecondItem.Click();
            Thread.Sleep(2000);
        }

        public void GoToCheckOutPage()
        {
            CartButton.Click();
            Thread.Sleep(2000);

        }


    }
}
