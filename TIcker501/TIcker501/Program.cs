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
    enum MarketVolatility
    {
        HIGH, MID, LOW
    }
    class Program
    {
        /*This function will be called when the user first opens up a console, and it will retrieve
         * data from the database (file) in order to populate the user accounts, stocks, and portfolios
         */
        public static Dictionary<string, Stock> RetrieveData(string fileLocation)
        {
            Dictionary<string, Stock> stockIndex = new Dictionary<string, Stock>();
            string line;
            string countString; 
            int count; 
            System.IO.StreamReader file = new System.IO.StreamReader(fileLocation);
            countString = file.ReadLine();
            count = Int32.Parse(countString);
            for(int i = 0; i < count; i ++)
            {
                line = file.ReadLine();
                string[] words = line.Split('-');
                Stock s = new Ticker501.Stock(words[0], words[1], double.Parse(words[2].Substring(1, words[2].Length - 1)));
                stockIndex.Add(words[0], s);
            }
            return stockIndex;
        }

        static void Main(string[] args)
        {
            Dictionary<string, Stock> stockIndex = RetrieveData("C:/CIS501GithubFiles/Ticker501/TIcker501/TIcker501/Ticker.txt");
            foreach(string s in stockIndex.Keys)
            {
                Console.WriteLine(stockIndex[s]);
            }
            Console.ReadLine();
        }
    }
}
