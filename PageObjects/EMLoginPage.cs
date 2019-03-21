using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMExampleEM.PageObjects
{
    class EMLoginPage
    {
        private IWebDriver driver;

        public EMLoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "email")]
        public IWebElement usernameTextfield;

        [FindsBy(How = How.XPath, Using = "//input[@id='password']")]
        public IWebElement passwordTextfield;

        [FindsBy(How = How.XPath, Using = "//button[@id='login']")]
        public IWebElement signInBtn;

        [FindsBy(How = How.XPath, Using = "//div[@class='forgot-password']/a")]
        public IWebElement forgotPasswordLnk;

        public void goToPage()
        {
            driver.Navigate().GoToUrl("https://collab.reutest.com/em-beta/login");
        }

        

    }
}
