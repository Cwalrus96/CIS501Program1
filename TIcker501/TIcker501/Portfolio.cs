//Caleb Compton
//CIS 501 Personal Programming Assignment 
//2017-01-28
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticker501
{
    class Portfolio
    {
        public Dictionary<string, Stock> stocks;
        public Dictionary<string, int> amounts;
        public Dictionary<string, double> startingPrices;
        public string name;

        //This constructor will take a name, and return a portfolio with an empty list of stocks.
        public Portfolio(string name)
        {
            this.name = name;
            this.stocks = new Dictionary<string, Stock>();
            this.amounts = new Dictionary<string, int>();
            this.startingPrices = new Dictionary<string, double>();
        }

        //This constructor will take a dictionary of stocks as an argument, 
        //and will create a portfolio containing those stocks
        public Portfolio(Dictionary<string, Stock> stocks)
        {
            this.stocks = stocks;
        }

        public void ViewPortfolio()
        {
            Console.WriteLine("Portfolio Name: " + name);
            Console.WriteLine("Total Number of stocks: " + this.stocks.Count);
            Console.WriteLine("Stock Information:");
            int totalNumber = 0;
            double totalValue = 0.0;
            foreach (string s in this.stocks.Keys)
            {
                totalNumber += this.amounts[s];
                totalValue += (this.stocks[s].price * this.amounts[s]);
            }
            double totalValueChange = 0;
            foreach (String s in this.stocks.Keys)
            {
                Console.WriteLine(this.stocks[s]);
                Console.WriteLine("Amount: " + this.amounts[s] + " \n Total Value of these stocks $" +
                    (this.amounts[s] * this.stocks[s].price) + " \n Percent of total shares: " + (((double)this.amounts[s]) / ((double)totalNumber)) * 100 +
                    "% \n Percent of total Value of Portfolio: " + (((((double)this.amounts[s]) * ((double)this.stocks[s].price)) / ((double)totalValue)) * 100) + "% \n" +
                    "Gain/Loss of this stock: $" + ((this.stocks[s].price - this.startingPrices[s]) * this.amounts[s]));
                totalValueChange += ((this.stocks[s].price - this.startingPrices[s]) * this.amounts[s]);
            }
            Console.WriteLine("Total Gains/Losses = $" + totalValueChange);
        }

        //This function will allow the username to not only specify a stock to add, but also the amount of that stock to add
        //and will add that amount of the stock to the portfolio.
        public void AddStock(Stock s, int amount, double startingPrice)
        {
            if (this.stocks.ContainsKey(s.ticker))
            {
                this.amounts[s.ticker] += amount;
            }
            else
            {
                this.stocks.Add(s.ticker, s);
                this.amounts.Add(s.ticker, amount);
                this.startingPrices.Add(s.ticker, startingPrice);
            }
        }

        public override string ToString()
        {
            return "Portfolio:" + name;
        }

    }
}
