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
        [TestFixture]
        public class Fixture1
        {
            [Test]
            public void aTestUrl() //��������� ����� ��� ���������� ������� ������������. Order ������-�� �� ��������
            {
                yp = new YandexPageObj(driver);
                yp.Open();//��������� yandex
                yp.sendKeysToSearchField("������ ���������� ����� ��������");
                yp.clickSearchButton();
                ap = yp.clickOnSite();//�������� �������� avtodispatcher �������� �� 
                Assert.AreEqual("https://www.avtodispetcher.ru/distance/", ap.Url);
            }

            [Test]
            public void bTestPriceDistanseFirst()
            {
                
                ap.enterData();
                ap.clickOnCalculateBtn();
                Assert.IsTrue(ap.isPriceEqual("3726") && ap.isDistanceEqual("897"));
            }
            [Test]
            public void cTestPriceDistanseSecond()
            {
                Thread.Sleep(50);//����� ������ �� ������� � �� �������� ����������
                ap.addThrowCity("������� ��������");
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