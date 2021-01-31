using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YandexPage
{
    public class YandexPageObj
    {

        public ChromeDriver driver;
        private readonly By _searchFeild = By.XPath(
           "//input[contains(@class,'input__control input__input mini-suggest__input')]");
        private readonly By _findBtn = By.XPath("//button[contains(text(),'Найти')]");
        private readonly By _dispetcherLink = By.XPath("//b[contains (text(),'avtodispetcher.ru')]");
        private readonly string url = "https://yandex.ru/";
        public string Url { get => driver.Url; }
        public YandexPageObj(ChromeDriver webDriver)
        {
            driver = webDriver;
        }

        public void Open()
        {
            driver.Navigate().GoToUrl(url);
        }
        public void sendKeysToSearchField(string Text)
        {
            driver.FindElement(_searchFeild).SendKeys(Text);
        }
        public void clickSearchButton()
        {
            driver.FindElement(_findBtn).Click();
        }
        public AvtodispetcherPage clickOnSite()
        {
            driver.FindElement(_dispetcherLink).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            return new AvtodispetcherPage(driver);
        }
      
    }
}
