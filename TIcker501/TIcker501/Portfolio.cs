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

        //This constructor will take no arguments, and will return an empty portfolio.
        public Portfolio()
        {
            this.stocks = new Dictionary<string, Stock>();
        }

        //This constructor will take a dictionary of stocks as an argument, 
        //and will create a portfolio containing those stocks
        public Portfolio(Dictionary<string, Stock> stocks)
        {
            this.stocks = stocks; 
        }

        public override string ToString()
        {
            return stocks.ToString();
        }

    }
}
