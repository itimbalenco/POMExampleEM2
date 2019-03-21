using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using POMExampleEM.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMExampleEM
{
    public class TestClass
    {

        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void TestBBCPresense()
        {
            EMLoginPage eMLoginPage = new EMLoginPage(driver);
            EMMainPage eMMainPage = new EMMainPage(driver);
            eMLoginPage.goToPage();
            eMLoginPage.usernameTextfield.SendKeys("qct133.emintuser@inttest.com");
            eMLoginPage.passwordTextfield.SendKeys("Welcome1");
            eMLoginPage.signInBtn.Click();
            eMMainPage.isPageLoaded();
            IWebElement popUpButton = driver.FindElement(By.XPath("//div[@class='spacing']//a[@class='button ok']"));
            if (popUpButton.Displayed)
                popUpButton.Click();
            IWebElement group = eMMainPage.getAvailableGroups()[1];
            group.Click();
            List<IWebElement> elementList = eMMainPage.getUsersInGroupByGroupName("TestGroup1");
            eMMainPage.rightClick(elementList[1]);

            Assert.IsTrue(eMMainPage.bbcCard.Displayed);
            eMMainPage.rightClick(elementList[1]);
            IWebElement group2 = eMMainPage.getAvailableGroups()[2];
            group2.Click();
            Thread.Sleep(2000);
            List<IWebElement> elementList2 = eMMainPage.getUsersInGroupByGroupIndex(2);
            eMMainPage.rightClick(elementList2[1]);
            Thread.Sleep(10000);
            Assert.IsTrue(eMMainPage.bbcCard.Displayed);
        }

        [Test]
        public void testName()
        {

        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

    }
}
