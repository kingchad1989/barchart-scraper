namespace BarchartScraper
{
    using System;
    using System.Collections.ObjectModel;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;
    using WebDriverManager;
    using WebDriverManager.DriverConfigs.Impl;

    public class WebPage
    {
        private const string PageUrl = "https://www.barchart.com/options/unusual-activity/stocks";
        private static readonly By TableDiv = By.ClassName("bc-table-scrollable");

        public static ReadOnlyCollection<Cookie> GetCookies()
        {
            IWebDriver driver = null;
            try
            {
                driver = GetChromeDriver();
                driver.Navigate().GoToUrl(PageUrl);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.ElementIsVisible(TableDiv));
                var cookies = driver.Manage().Cookies.AllCookies;
                return cookies;
            }
            finally
            {
                driver?.Quit();
            }
        }

        private static IWebDriver GetChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();
            options.AddArgument("headless");
            return new ChromeDriver(options);
        }
    }
}