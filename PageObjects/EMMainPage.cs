using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMExampleEM.PageObjects
{
    class EMMainPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public EMMainPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='current-user']/span[@class='username']")]
        public IWebElement usernameLbl;

        [FindsBy(How = How.XPath, Using = "//div[@class='baseball-card']")]
        public IWebElement bbcCard;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'list roster-groups')]")]
        public IWebElement groupsList;

        [FindsBy(How = How.XPath, Using = "//div[@class='current-user']/span[contains(@class, 'current-user-presence')]")]
        public IWebElement presenceIndicatorLbl;

        [FindsBy(How = How.XPath, Using = "//textarea[@placeholder='Type Here']")]
        public IWebElement sendMessageTxtFld;

        public void goToPage()
        {
            driver.Navigate().GoToUrl("https://collab.reutest.com/em-beta");
        }

        public List<IWebElement> getAvailableGroups()
        {
            List<IWebElement> list = groupsList.FindElements(By.XPath("//ul[@class='list-items']/div[contains(@class, 'list-item group-item droppable shown')]")).ToList();
            return list;
        }

        public List<IWebElement> getUsersInGroupByGroupName(String groupName)
        {            
            string xPath = $"//div[@data-id='{groupName}']//ul[@class='list-items']/div[contains(@class, 'list-item shown')]";
            List<IWebElement> list = driver.FindElements(By.XPath(xPath)).ToList();            
            return list;
        }

        public List<IWebElement> getUsersInGroupByGroupIndex(int groupIndex)
        {
            string data_id = getAvailableGroups()[groupIndex].GetAttribute("data-id");
            string xPath = $"//div[@data-id='{data_id}']//ul[@class='list-items']/div[contains(@class, 'list-item shown')]";
            List<IWebElement> list = driver.FindElements(By.XPath(xPath)).ToList();
            return list;
        }

        public IWebElement getGroupByName(string groupName)
        {
            for (int i = 0; i < getAvailableGroups().Count(); i++)
            {
                if (getAvailableGroups()[i].GetAttribute("id") == groupName)
                    return getAvailableGroups()[i];
            }
            return null;
        }

        public bool IsElementPresent(IWebElement element)
        {
            return element.Displayed;
        }

        public void rightClick(IWebElement elementToRightClick)
        {
            var action = new OpenQA.Selenium.Interactions.Actions(this.driver);
            action.ContextClick(elementToRightClick);
            action.Perform();
        }

        public bool isPageLoaded()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='current-user']/span[contains(@class, 'current-user-presence')]")));
            return this.IsElementPresent(this.presenceIndicatorLbl);
        }

    }
}
