using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PageObjects;
using System;
using System.Linq;
using System.Threading;
using YandexPage;

namespace YandexTests
{
    public class AvtoDispetcherTests
    {
        static ChromeDriver driver = new ChromeDriver(OptionSettings());
        static YandexPageObj yp;
        static AvtodispetcherPage ap;

        private static ChromeOptions OptionSettings()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("disable-popup-blocking");
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            return options;
        }
        private static void RemovePopup()
        {
            driver.FindElementByClassName("ezmob-footer-close").Click();
        }
        [TestFixture]
        public class Fixture1
        {
            [Test, Order(1)]
            public void TestUrl() 
            {
                yp = new YandexPageObj(driver);
                yp.Open();//Открываем yandex
                yp.SendKeysToSearchField("расчет расстояний между городами");
                yp.ClickSearchButton();
                ap = yp.СlickOnSite();//Открываем сайт avtodispatcher нажатием на ссылку
                Assert.AreEqual("https://www.avtodispetcher.ru/distance/", ap.Url);
            }

            [Test,Order(2)]
            public void TestPriceDistanseFirst()
            {
                RemovePopup();
                ap.EnterData();
                ap.ClickOnCalculateBtn();
                Assert.IsTrue(ap.IsPriceEqual("3726") && ap.IsDistanceEqual("897"));
            }
            [Test, Order(3)]
            public void TestPriceDistanseSecond()
            {
                RemovePopup();
                ap.AddThrowCity("Великий Новгород");
                Thread.Sleep(30000);
                ap.ClickOnCalculateBtn();
                Assert.IsTrue(ap.IsPriceEqual("4002") && ap.IsDistanceEqual("966"));

            }
            [OneTimeTearDown]
            public void close_Browser()
            {
                driver.Quit();
            }

        }
        
    }
}