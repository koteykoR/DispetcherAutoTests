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
        static ChromeDriver driver = new ChromeDriver(OptionSettrings());
        static YandexPageObj yp;
        static AvtodispetcherPage ap;

        private static ChromeOptions OptionSettrings()
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
            [Test]
            public void aTestUrl() //Добавлены буквы для соблюдения порядка тестирования. Order почему-то не работает
            {
                yp = new YandexPageObj(driver);
                yp.Open();//Открываем yandex
                yp.sendKeysToSearchField("расчет расстояний между городами");
                yp.clickSearchButton();
                ap = yp.clickOnSite();//Получаем страницу avtodispatcher нажатием на 
                Assert.AreEqual("https://www.avtodispetcher.ru/distance/", ap.Url);
            }

            [Test]
            public void bTestPriceDistanseFirst()
            {
                RemovePopup();
                ap.enterData();
                ap.clickOnCalculateBtn();
                Assert.IsTrue(ap.isPriceEqual("3726") && ap.isDistanceEqual("897"));
            }
            [Test]
            public void cTestPriceDistanseSecond()
            {
                RemovePopup();
                ap.addThrowCity("Великий Новгород");
                Thread.Sleep(30000);
                ap.clickOnCalculateBtn();
                Assert.IsTrue(ap.isPriceEqual("4002") && ap.isDistanceEqual("966"));

            }
            [OneTimeTearDown]
            public void close_Browser()
            {
                driver.Quit();
            }

        }
        
    }
}