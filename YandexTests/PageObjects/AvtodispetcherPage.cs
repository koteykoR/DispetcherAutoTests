using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace PageObjects
{
    public class AvtodispetcherPage
    {
        private static IWebDriver driver;
        public  string Url { get => driver.Url; }
        #region Xpaths
        private readonly By _fromFeild = By.Name("from");
        private readonly By _toFeild = By.Name("to");
        private readonly By _fp = By.XPath("//input[contains(@name,'fp')]");
        private readonly By _fc = By.XPath("//input[contains(@name,'fc')]");
        private readonly By _calculateBtn = By.XPath("//input[contains(@value,'Рассчитать')]");
        private readonly By _distance = By.XPath("//span[contains( @id,'totalDistance')]");
        private readonly By _price = By.XPath(@"//a[contains(text(),'руб')]");
        private readonly By _changeRouteBtn = By.XPath(@"//span[contains(text(),'Настроить')]");
        private readonly By _throwCityField = By.Name("v");
        private readonly By _fuelAndPrice = By.XPath("//form[contains(@onsubmit,'return fuelFormSubmitHandler(this);')]");
        #endregion 
        public string Distance { get=>driver.FindElement(_distance).Text;}
        public string Price { get => getPrice(); }
        public AvtodispetcherPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }
        public void Open()
        {
            driver.Navigate().GoToUrl("https://www.avtodispetcher.ru/distance/");
        }
        public  void EnterData()
        {
            EnterFrom();
            EnterTo();
            EnterFc();
            EnterFp();
        }
        private void EnterFrom()
        {
            driver.FindElement(_fromFeild).SendKeys("Тула");
        }
        private void EnterTo()
        {
            driver.FindElement(_toFeild).SendKeys("Санкт-Петербург");

        }
        private void EnterFc()
        {
            var fc = driver.FindElement(_fc);
            fc.Clear();
            fc.SendKeys("9");
        }
        private void EnterFp()
        {
            var fc = driver.FindElement(_fp);
            fc.Clear();
            fc.SendKeys("46");
        }
        public void ClickOnCalculateBtn()
        {
            driver.FindElement(_calculateBtn).SendKeys(Keys.Enter);
        }
        public string getPrice() //Спрятали суммарную стоимость топлива,пришлось получать так(Пофиксить)
        {
            var str = driver.FindElement(_fuelAndPrice).Text;
            var from = str.LastIndexOf("= ")+ "= ".Length;
            var to = str.LastIndexOf(" руб");
            var s = str.Substring(from, to - from);
            return s;
        }
        public bool isPriceEqual(string price)
        {
            if (price == Price)
                return true;
            else return false;
        }
        public bool isDistanceEqual(string distance)
        {
            if (distance == Distance)
                return true;
            else return false;
        }
        public void addThrowCity(string city)
        {
            driver.FindElement(_changeRouteBtn).Click();
            driver.FindElement(_throwCityField).SendKeys(city);

        }
    }

}
