using System;
using System.Collections.Generic;
using InvestmentTracker.Domain.Investments;
using InvestmentTracker.Domain.Prices;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;

namespace InvestmentTracker.Scraper.Investments
{
    public abstract class Investment : IInvestment
    {
        protected Investment(InvestmentSettings settings)
        {
            Password = settings.Password;
            Url = settings.Url;
            Username = settings.Username;
        }

        protected bool LoggedIn { get; private set; }

        protected string Password { get; }

        protected string Url { get; }

        protected string Username { get; }

        public IReadOnlyCollection<Price> GetPrices(DateTime from, DateTime? to)
        {
            using (IWebDriver driver = new PhantomJSDriver())
            {
                List<Price> prices = new List<Price>();

                DateTime date = from;

                while (date <= (to ?? from))
                {
                    Navigate(driver, date);

                    if (!LoggedIn)
                    {
                        Login(driver);
                        LoggedIn = true;
                    }

                    IEnumerable<Price> datePrices = GetPrices(driver, date);
                    prices.AddRange(datePrices);

                    date = date.AddDays(1);
                }

                return prices;
            }
        }

        protected abstract IEnumerable<Price> GetPrices(IWebDriver driver, DateTime date);

        protected abstract void Login(IWebDriver driver);

        protected abstract void Navigate(IWebDriver driver, DateTime date);
    }
}