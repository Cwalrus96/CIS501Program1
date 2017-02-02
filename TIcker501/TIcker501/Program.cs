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
         * data from the database (file) in order to populate the stocks
         */
        public static Dictionary<string, Stock> RetrieveStockData(string fileLocation)
        {
            Dictionary<string, Stock> stockIndex = new Dictionary<string, Stock>();
            string line;
            string countString;
            int count;
            System.IO.StreamReader file = new System.IO.StreamReader(fileLocation);
            countString = file.ReadLine();
            count = Int32.Parse(countString);
            for (int i = 0; i < count; i++)
            {
                line = file.ReadLine();
                string[] words = line.Split('-');
                Stock s = new Ticker501.Stock(words[0], words[1], double.Parse(words[2].Substring(1, words[2].Length - 1)));
                stockIndex.Add(words[0], s);
            }
            return stockIndex;
        }

        //This function will be called after retrieving all stocks, and will be used to retrieve all 
        //Portfolio information from the database (text files)
        public static Dictionary<string, Portfolio> RetrievePortfolioData(string fileLocation, Dictionary<string, Stock> s)
        {
            Dictionary<string, Portfolio> portfolios = new Dictionary<string, Portfolio>();
            string line;
            string countString;
            int count;
            System.IO.StreamReader file = new System.IO.StreamReader(fileLocation);
            countString = file.ReadLine();
            count = Int32.Parse(countString);
            for (int i = 0; i < count; i++)
            {
                line = file.ReadLine();
                string[] words = line.Split(':');
                Portfolio p = new Portfolio(words[0]);
                string[] subWords = words[1].Split(';');
                for (int j = 0; j < subWords.Length; j++)
                {
                    string[] subSubWords = subWords[j].Split('-');
                    if (s.ContainsKey(subSubWords[0]))
                    {
                        p.AddStock(s[subSubWords[0]], Int32.Parse(subSubWords[1]));
                    }
                }
                portfolios.Add(p.name, p);

            }
            return portfolios;
        }

        //This function will be called after calling the portfolio retrieval function, and will be used to 
        //retrieve all account data from the database (text files)
        public static Dictionary<string, Account> RetrieveAccountData(string fileLocation, Dictionary<string, Portfolio> portfolios)
        {
            Dictionary<string, Account> users = new Dictionary<string, Account>();
            string line;
            string countString;
            int count;
            System.IO.StreamReader file = new System.IO.StreamReader(fileLocation);
            countString = file.ReadLine();
            count = Int32.Parse(countString);
            for (int i = 0; i < count; i++)
            {
                line = file.ReadLine();
                string[] words = line.Split(':');
                string username = words[0];
                double funds = double.Parse(words[1].Substring(1));
                string[] subWords = words[2].Split('-');
                Account a = new Ticker501.Account(username, funds);
                for (int j = 0; j < subWords.Length; j++)
                {
                    if (portfolios.ContainsKey(subWords[j]))
                    {
                        a.addPortfolio(subWords[j], portfolios[subWords[j]]);
                    }
                }
                users.Add(username, a);
            }
            return users;
        }

        //This function will be called at the beginning of the console application in order to "Log in" 
        //to a user, and will also be called whenever the user "logs out"
        public static Account LogInAccount(Dictionary<string, Account> accounts)
        {
            string input = "2";
            Console.WriteLine("Welcome to the Rempton Electronic Banking System");
            while ((Int32.Parse(input) != 1) && (Int32.Parse(input) != 0))
            {
                Console.WriteLine("Are you a new or returning user?");
                Console.WriteLine("New User (1)");
                Console.WriteLine("Returning User (0)");
                input = Console.ReadLine();

            }
            if (Int32.Parse(input) == 1)

            {
                Console.WriteLine("Choose a UserName");
                input = Console.ReadLine();
                Console.WriteLine("Creating a new user...");
                Account user = new Account(input);
                Console.WriteLine("New User Created! Welcome, " + input + "!");
                return user;
            }
            else
            {
                Console.WriteLine("Type your Username");
                input = Console.ReadLine();
                while (!accounts.ContainsKey(input))
                {
                    Console.WriteLine("That Username does not exist. Type your username, or type 0 to exit");
                    input = Console.ReadLine();
                    int number;
                    if ((Int32.TryParse(input, out number) == false) && (number == 0))
                    {
                        Console.WriteLine("Exitting....");
                        return null;
                    }
                }
            }
            Console.WriteLine("Welcome back, " + input + '!');
            return accounts[input];
        }

        

        static void Main(string[] args)
        {
            Dictionary<string, Stock> stockIndex = RetrieveStockData("Ticker.txt");
            Dictionary<string, Portfolio> portfolios = RetrievePortfolioData("Portfolios.txt", stockIndex);
            Dictionary<string, Account> users = RetrieveAccountData("Accounts.txt", portfolios);
            Account currentUser = null;
            bool exit = false;
            bool loggedIn = false;
            string input;
            while (exit == false)
            {
                if (loggedIn == false)
                {
                    currentUser = LogInAccount(users);
                    if (currentUser == null) return;
                    loggedIn = true;
                }
                int number;
                //Print menu options
                Console.WriteLine("Well, We made it this far!");
                Console.WriteLine("Please Select an action!");
                Console.WriteLine("Buy/Sell Stocks (1)");
                Console.WriteLine("Transfer Funds (2)");
                Console.WriteLine("View Accounts/Portfolios (3) ");
                Console.WriteLine("Create/Delete Portfolios (4) ");
                Console.WriteLine("Log out (5)");

                input = Console.ReadLine();
                while ((Int32.TryParse(input, out number) == false) && (number > 0) && (number < 6))
                {
                    Console.WriteLine("Please Select a valid Input");
                    input = Console.ReadLine();
                }

                if (number == 1)
                {
                    Console.WriteLine("    Would you like to buy(1) or sell (2)?");
                    input = Console.ReadLine();
                    while ((Int32.TryParse(input, out number) == false) && (!(number == 0) && !(number == 1)))
                    {
                        Console.WriteLine("Please Select a valid Input");
                        input = Console.ReadLine();
                    }
                    if(input == 1)
                    {
                        Console.WriteLine("What kind of stock would you like to buy? Type the stock ticker to select a stock, or type 1 to view stocks");
                        Account.buyStocks();
                    }
                }
                else if (number == 2)
                {

                }
                else if (number == 3)
                {

                }
                else if (number == 4)
                {

                }
                else
                {

                }


            };

        }
    }
}