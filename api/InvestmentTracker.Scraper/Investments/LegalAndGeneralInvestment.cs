using OpenQA.Selenium;
using System;
using InvestmentTracker.Domain.Prices;
using System.Collections.Generic;
using InvestmentTracker.Domain.Investments;

namespace InvestmentTracker.Scraper.Investments
{
    public class LegalAndGeneralInvestment : Investment
    {
        public LegalAndGeneralInvestment(InvestmentSettings settings)
            : base(settings)
        {
        }

        protected override IEnumerable<Price> GetPrices(IWebDriver driver, DateTime date)
        {
            var rows = driver.FindElements(By.CssSelector("table tbody tr"));
            foreach (var row in rows)
            {
                Price price = GetPrice(row, date);
                if (price != null)
                {
                    yield return price;
                }
            }
        }

        protected override void Login(IWebDriver driver)
        {
            driver.FindElement(By.Id("username")).SendKeys(Username);
            driver.FindElement(By.Id("password")).SendKeys(Password);

            driver.FindElement(By.Id("loginForm")).Submit();
        }

        protected override void Navigate(IWebDriver driver, DateTime date)
        {
            driver.Url = Url.Replace("{date}", date.ToString("yyyyMMdd"));
        }

        private IWebElement FindElement(IWebElement parent, By by)
        {
            try
            {
                return parent.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        private string GetInnerText(IWebElement element)
        {
            if (element == null)
            {
                return null;
            }

            // .Text returns empty string for hidden elements
            return element.GetAttribute("textContent");
        }

        private Price GetPrice(IWebElement row, DateTime date)
        {
            try
            {
                IWebElement fundCell = FindElement(row, By.CssSelector("td:first-child"));
                string fund = GetInnerText(fundCell);
                if (string.IsNullOrWhiteSpace(fund))
                {
                    return null;
                }

                IWebElement valueCell = FindElement(row, By.ClassName("gridColumnPrice"));
                string valueString = GetInnerText(valueCell);
                if (string.IsNullOrWhiteSpace(valueString))
                {
                    return null;
                }

                valueString = valueString.Replace(",", "").Replace("p", "");

                if (!double.TryParse(valueString, out double value))
                {
                    return null;
                }

                return new Price(date, fund, value);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
    }
}