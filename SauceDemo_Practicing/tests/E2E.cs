using MongoDB.Driver.Core.Authentication;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo_Practicing.hooks;
using SauceDemo_Practicing.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace SauceDemo_Practicing.tests
{

    internal class E2E : Base
    {

        [Test, TestCaseSource("credentialsSource")]
        //[Parallelizable(ParallelScope.All)]
        //[TestCase("credentialsSource"), Category("Regression")]
        //[TestCaseSource("credentialsSource")]
        public void E2E_Test(string username, string password)
        {
            // Elements Instanciated
            LoginPage login = new LoginPage(getDriver());
            ProductsPage products = new ProductsPage(getDriver());
            CheckOutPage check = new CheckOutPage(getDriver());
            //Automation Steps
            login.ExecuteLogin(username, password);
            products.WaitPageElement();
            products.SortProductsByPrice();
            products.AdditensToCart();
            products.GoToCheckOutPage();
            check.CheckProducts();

        }

        public static IEnumerable<TestCaseData> credentialsSource()
        {
            yield return new TestCaseData(JsonReader.getDataJson("valid_credentials.username"), JsonReader.getDataJson("valid_credentials.password"));
            yield return new TestCaseData(JsonReader.getDataJson("invalid_credentials.username"), JsonReader.getDataJson("invalid_credentials.password"));
            yield return new TestCaseData(JsonReader.getDataJson("without_credentials.username"), JsonReader.getDataJson("without_credentials.password"));
        }


    }
}
