using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo_Practicing.pages
{
    internal class CheckOutPage
    {
        IWebDriver Driver;

        public CheckOutPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this.Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".inventory_item_name")]
        private IList<IWebElement> boughtProduct;

        [FindsBy(How = How.CssSelector, Using = "#checkout")]
        private IWebElement checkOutButton;


        public void CheckProducts()
        {
            foreach (var item in boughtProduct)
            {
                if (item.Text == "Sauce Labs Fleece Jacket")
                {
                    Console.WriteLine("Produto " + item.Text + " Adicionado com sucesso!");
                }else if(item.Text == "Sauce Labs Backpack")
                {
                    Console.WriteLine("Produto " + item.Text + " Adicionado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Produto não foi encontrado na lista de compras!");
                }
            }

            checkOutButton.Click();
            Thread.Sleep(3000);
        }

        

    }
}
