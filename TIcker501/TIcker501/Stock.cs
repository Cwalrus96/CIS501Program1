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
    class Stock
    {
        public string ticker;
        public string companyName;
        public double price;

        public Stock(string ticker, string companyName, double price)
        {
            this.ticker = ticker;
            this.companyName = companyName;
            this.price = price;
        }

        public override string ToString()
        {
            return "Ticker:" + this.ticker + " Company Name:" + this.companyName + " price: $" + price;
        }

    }
}
