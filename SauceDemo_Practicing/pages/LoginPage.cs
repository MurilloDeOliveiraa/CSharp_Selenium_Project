using OpenQA.Selenium;
using SauceDemo_Practicing.hooks;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo_Practicing.pages
{
    internal class LoginPage
    {
        private IWebDriver Driver;               

        public LoginPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this.Driver, this);
        }

        [FindsBy(How = How.Id, Using = "user-name")]
        private IWebElement usernameTextBox;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordTextBox;

        [FindsBy(How = How.Id, Using = "login-button")]
        private IWebElement loginButton;

        public void ExecuteLogin(String username, String password)
        {
            usernameTextBox.SendKeys(username);
            passwordTextBox.SendKeys(password);
            loginButton.Click();
        }
                              
        
        


    }
}
