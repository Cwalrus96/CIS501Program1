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
        public string name;

        //This constructor will take a name, and return a portfolio with an empty list of stocks.
        public Portfolio(string name)
        {
            this.name = name;
            this.stocks = new Dictionary<string, Stock>();
        }

        //This constructor will take a dictionary of stocks as an argument, 
        //and will create a portfolio containing those stocks
        public Portfolio(Dictionary<string, Stock> stocks)
        {
            this.stocks = stocks; 
        }

        //This function will allow the user to not only specify a stock to add, but also the amount of that stock to add
        //and will add that amount of the stock to the portfolio.
        public void AddStock(Stock s, int amount)
        {

        }

        public override string ToString()
        {
            return stocks.ToString();
        }

    }
}
