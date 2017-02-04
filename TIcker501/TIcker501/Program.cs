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
        //Initialize important variables 
        public static Dictionary<string, Stock> stockIndex;
        public static Dictionary<string, Portfolio> portfolios;
        public static Dictionary<string, Account> users;
        public static Account currentUser;
        public static bool exit;
        public static bool loggedIn;
        public static string input;
        public static int menu;
        public static int number;

        /*This function will be called when the username first opens up a console, and it will retrieve
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
        //to a username, and will also be called whenever the username "logs out"
        public static void LogInAccount()
        {
            string input = "-1";
            Console.WriteLine("Welcome to the Rempton Electronic Banking System");
            while ((Int32.Parse(input) != 1) && (Int32.Parse(input) != 2))
            {
                Console.WriteLine("Are you a new or returning username?");
                Console.WriteLine("New User (1)");
                Console.WriteLine("Returning User (2)");
                input = Console.ReadLine();

            }
            if (Int32.Parse(input) == 1)

            {
                Console.WriteLine("Choose a Username");
                input = Console.ReadLine();
                Console.WriteLine("Creating a new username...");
                Account user = new Account(input);
                Console.WriteLine("New User Created! Welcome, " + input + "!");
                Program.users.Add(user.username, user);
                currentUser = user;
            }
            else
            {
                Console.WriteLine("Type your Username");
                input = Console.ReadLine();
                while (!users.ContainsKey(input))
                {
                    Console.WriteLine("That Username does not exist. Type your username, or type 0 to exit");
                    input = Console.ReadLine();
                    int number;
                    if ((Int32.TryParse(input, out number) == true) && (number == 0))
                    {
                        Console.WriteLine("Exitting....");
                    }
                }
            }
            Console.WriteLine("Welcome back, " + input + '!');
            currentUser = users[input];
        }

        //This function will be callled to ask the username if they want to buy stocks, in what portfolio, and how much. 
        static void AskToBuy()
        {
            Console.WriteLine("What kind of stock would you like to buy? Type the stock ticker to select a stock, or type 1 to view stocks");
            //This while loop will make sure username enters a valid stock ticker
            while (stockIndex.ContainsKey(input) == false)
            {
                input = Console.ReadLine();
                if ((Int32.TryParse(input, out number) == true) && (number == 1))
                {
                    foreach (string s in stockIndex.Keys)
                    {
                        Console.WriteLine(stockIndex[s]);
                    }
                }
                if (stockIndex.ContainsKey(input))
                {
                    string stockName = input;
                    //This while loop will make sure the username enters a valid amount that they can afford
                    while (Int32.TryParse(input, out number) == false)
                    {
                        Console.WriteLine("How much of this stock would you like to buy? Please enter a whole number, or type 0 to exit");
                        input = Console.ReadLine();
                        Int32.TryParse(input, out number);
                        //User entered '0' - exit to main menu
                        if (number == 0)
                        {
                            menu = 7;
                        }
                        else
                        {
                            Console.WriteLine("Which portfolio do you want to put these stocks in?");
                            Console.WriteLine("Your current Portfolios are: ");

                            foreach (string s in currentUser.portfolios.Keys)
                            {
                                Console.WriteLine(currentUser.portfolios[s]);
                            }

                            while (currentUser.portfolios.ContainsKey(input) == false)
                            {
                                Console.WriteLine("Please type the name of the Portfolio you wish to add the stocks to");
                                input = Console.ReadLine();
                            }
                            if (currentUser.BuyStock(currentUser.portfolios[input], stockIndex[stockName], number))
                            {
                                Console.WriteLine("Transaction Completed! purchased " + number + " shares of " + stockIndex[stockName].companyName + " stock.");
                                Console.WriteLine("New Account Balance = " + currentUser.funds + ".");
                                Console.WriteLine("Enter to return to the Stock Menu");
                                Console.ReadLine();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                    break;
                }
                break;
            }
        }

        //This function will be called to ask the username if they want to sell stocks, from what portfolio, and how much. 
        static void AskToSell()
        {
            //Decide Portfolio to sell from
            Console.WriteLine("Which portfolio do you want to sell from?");
            Console.WriteLine("Your current Portfolios are: ");

            foreach (string s in currentUser.portfolios.Keys)
            {
                Console.WriteLine(currentUser.portfolios[s]);
            }

            while (currentUser.portfolios.ContainsKey(input) == false)
            {
                Console.WriteLine("Please type the name of the Portfolio you wish to sell from");
                input = Console.ReadLine();
            }
            string portfolioName = input;
            Console.WriteLine("Which stock would you like to sell? Type the name of a stock to sell it, 1 to see a list of stocks in this portfolio, or 0 to exit");
            //Wait until username enters a valid input or 0
            number = -1;
            while ((currentUser.portfolios[portfolioName].stocks.ContainsKey(input) == false))
            {
                input = Console.ReadLine();
                //check if username enters 1 - if so, list all stocks
                if (Int32.TryParse(input, out number) == true && number == 1)
                {
                    foreach (string s in currentUser.portfolios[portfolioName].stocks.Keys)
                    {
                        Console.WriteLine(currentUser.portfolios[portfolioName].stocks[s] + "(" + currentUser.portfolios[portfolioName].amounts[s] + ")");
                    }

                }
                //check if username entered 0 - if so, break and exit to menu
                else if (number == 0)
                {
                    break;
                }

            }
            string stockToSell = input;
            bool sold = false;
            Console.WriteLine("How much of this stock would you like to sell? Enter a number, or 0 to exit");
            while (Int32.TryParse(input, out number) == false || sold == false)
            {
                input = Console.ReadLine();
                bool sell = Int32.TryParse(input, out number);
                //if username types 0, exit 
                if (number == 0)
                {
                    break;
                }
                else if (sell == true)
                {
                    sold = currentUser.SellStock(currentUser.portfolios[portfolioName], currentUser.portfolios[portfolioName].stocks[stockToSell], number);
                }
            }
        }

        //This function will take a market volatility level (High, Mid, or low) and will randomly adjust all stocks based on that market volatility
        public static void RunSimulator(MarketVolatility m)
        {
            int max;
            int min;
            Random r = new Random(); 
            if(m == MarketVolatility.HIGH)
            {
                max = 15;
                min = 3; 
            }
            else if(m == MarketVolatility.MID)
            {
                max = 8;
                min = 2;
            }
            else
            {
                max = 4;
                min = 1; 
            }
            foreach(string s in Program.stockIndex.Keys)
            {
                double increment = ((double) r.Next(min, max) / 100) + 1;
                if(r.Next(1,10) > 5)
                {
                    increment *= -1;
                }
                Program.stockIndex[s].price *= increment;
            }
            Console.WriteLine("Finished running simulator");

        }




        static void Main(string[] args)
        {
            //Initialize important variables 
            stockIndex = RetrieveStockData("Ticker.txt");
            portfolios = RetrievePortfolioData("Portfolios.txt", stockIndex);
            users = RetrieveAccountData("Accounts.txt", portfolios);
            currentUser = null;
            exit = false;
            loggedIn = false;
            menu = 7;

            //This will be the main loop - will keep looping until the username chooses to exit
            while (exit == false)
            {

                //This while loop will be called if menu = zero. That means this will run at the beginning of the program, and if a username exits another menu
                while (menu == 7)
                {
                    //This will check if the username is logged in. User cannot interact with system if not logged in
                    if (loggedIn == false)
                    {
                        LogInAccount();
                        if (currentUser == null) return;
                        loggedIn = true;
                    }
                    //Print menu options
                    Console.WriteLine("Please Select an action!");
                    Console.WriteLine("Buy/Sell Stocks (1)");
                    Console.WriteLine("Transfer Funds (2)");
                    Console.WriteLine("View Accounts/Portfolios (3) ");
                    Console.WriteLine("Create/Delete Portfolios (4) ");
                    Console.WriteLine("Run Simulator (5)");
                    Console.WriteLine("Log out (6)");
                    Console.WriteLine("Exit Program (0)");
                    //Wait for username Input
                    input = Console.ReadLine();
                    //This loop will wait and make sure that the username is entering valid input
                    while ((Int32.TryParse(input, out number) == false) && (number > 0) && (number < 8))
                    {
                        Console.WriteLine("Please Select a valid Input");
                        input = Console.ReadLine();
                    }
                    //This updates the menu number, which allows the system to enter another while loop with another function
                    menu = number;
                }

                //This while loop will be entered when the username wants to buy or sell a stock. 
                while (menu == 1)
                {   //Cannot add stocks if username does not have any portfolios
                    if (currentUser.portfolios.Count == 0)
                    {
                        Console.WriteLine("Cannot add stocks without first creating a portfolio!");
                        menu = 7;
                        break;
                    }
                    //First, we need to decide if we want to buy or sell stocks, or exit the system
                    Console.WriteLine("Would you like to buy(1), sell (2), or exit(0)?");
                    input = Console.ReadLine();
                    //Making sure username entered valid input
                    while ((Int32.TryParse(input, out number) == false) || (!(number < 3) && !(number > -1)))
                    {
                        Console.WriteLine("Please Select a valid Input");
                        input = Console.ReadLine();
                    }

                    //If username selected '1' they want to buy stocks
                    if (number == 1)
                    {
                        AskToBuy();
                    }
                    //If username selected '2' they want to sell stocks
                    else if (number == 2)
                    {
                        AskToSell();
                    }
                    //If username selected '0' they want to exit back to the main menu
                    else if (number == 0)
                    {
                        Console.WriteLine("Returning to main menu....");
                        menu = 7;
                    }
                }

                //This while loop will be called if the username wants to withdraw or deposit funds into their account
                while (menu == 2)
                {
                    Console.WriteLine("Would you like to Deposit Funds (1), Withdraw Funds (2), or Exit (0)? ");
                    //Wait to make sure the username selected a valid input
                    input = Console.ReadLine();
                    number = -1;

                    while (Int32.TryParse(input, out number) == false || ((number > 2) || (number < 0)))
                    {
                        Console.WriteLine("Please Enter a valid Input");
                        input = Console.ReadLine();
                    }
                    //If the username selects 1, ask how much funds they would like to deposit
                    if (number == 1)
                    {
                        Console.WriteLine("How much would you like to deposit?");
                        input = Console.ReadLine();
                        double amount;
                        while (double.TryParse(input, out amount) == false)
                        {
                            Console.WriteLine("Please Enter a valid amount");
                            input = Console.ReadLine();
                        }
                        Console.WriteLine("Old Balance = $" + currentUser.funds);
                        currentUser.DepositFunds(amount);
                        Console.WriteLine("Depositing $" + (amount));
                        Console.WriteLine("Transfer Fee = $" + Account.transferFee);
                        Console.WriteLine("New Balance = $" + currentUser.funds);

                    }
                    //If the username selects 2, ask how much they would like to withdraw
                    if (number == 2)
                    {
                        Console.WriteLine("How much would you like to Withdraw?");
                        input = Console.ReadLine();
                        double amount;
                        while (double.TryParse(input, out amount) == false)
                        {
                            Console.WriteLine("Please Enter a valid amount");
                            input = Console.ReadLine();
                        }
                        //If they don't have enough funds, ask if they would like to sell some stocks or select a different amount
                        while ((amount - Account.transferFee) > currentUser.funds)
                        {
                            Console.WriteLine("You Don't Have enough Funds to withdraw. Would you like to sell some stocks(1), or enter a different amount?(2)");
                            input = Console.ReadLine();
                            while (Int32.TryParse(input, out number) == false || ((number != 1) && (number != 2)))
                            {
                                Console.WriteLine("Please choose an option");
                            }
                            if (number == 1)
                            {
                                AskToSell();
                            }
                            if (number == 2)
                            {
                                Console.WriteLine("How much would you like to Withdraw?");
                                input = Console.ReadLine();
                                while (double.TryParse(input, out amount) == false)
                                {
                                    Console.WriteLine("Please Enter a valid amount");
                                    input = Console.ReadLine();
                                }
                            }
                        }
                        Console.WriteLine("Old Balance = $" + currentUser.funds);
                        currentUser.WithdrawFunds(amount);
                        Console.WriteLine("Withdrawing $" + amount);
                        Console.WriteLine("Transfer Fee = $" + Account.transferFee);
                        Console.WriteLine("New Balance = $" + currentUser.funds);
                    }

                    //If the username selects 0, Break and exit
                    if (number == 0)
                    {
                        menu = 7;
                        break;
                    }

                }

                //This while loop will be called if the username wants to view their accounts or portfolios;
                while (menu == 3)
                {
                    Console.WriteLine("Would You like to view Cash And Positions Balance" +
                        " for Your Account (1) OR an individual portfolio (2), or exit(0)?");
                    input = Console.ReadLine();
                    while (Int32.TryParse(input, out number) == false || (number > 2) || (number < 0))
                    {
                        Console.WriteLine("Please Choose an Option");
                        input = Console.ReadLine();
                    }
                    //User chose 1 - view entire account
                    if (number == 1)
                    {
                        currentUser.PositionsBalance();
                    }
                    //User chose 2 - View single portfolio
                    else if (number == 2)
                    {
                        //Decide Portfolio to view
                        Console.WriteLine("Which portfolio do you want to view?");
                        Console.WriteLine("Your current Portfolios are: ");

                        foreach (string s in currentUser.portfolios.Keys)
                        {
                            Console.WriteLine(currentUser.portfolios[s]);
                        }

                        while (currentUser.portfolios.ContainsKey(input) == false)
                        {
                            Console.WriteLine("Please type the name of the Portfolio you wish to view");
                            input = Console.ReadLine();
                        }
                        string portfolioName = input;
                        currentUser.portfolios[portfolioName].ViewPortfolio();

                    }
                    //User chose 0 - exit to main menu;
                    else if (number == 0)
                    {
                        menu = 7;
                    }
                }

                //This while loop will be called if the username wants to create or delete a portfolio
                while (menu == 4)
                {
                    Console.WriteLine("Would you like to create a portfolio (1), Delete a portfolio (2) or exit (0)? ");
                    input = Console.ReadLine();
                    while ((Int32.TryParse(input, out number) == false) || (number > 2) || (number < 0))
                    {
                        Console.WriteLine("Please choose an option");

                    }
                    //User chose 1 - Create a portfolio
                    if (number == 1)
                    {
                        Console.WriteLine("What would you like to name your new portfolio?");
                        input = Console.ReadLine();
                        currentUser.CreatePortfolio(input);
                    }
                    //User chose 2 - Delete a portfolio
                    if (number == 2)
                    {//Decide Portfolio to delete
                        Console.WriteLine("Which portfolio do you want to delete?");
                        Console.WriteLine("Your current Portfolios are: ");

                        foreach (string s in currentUser.portfolios.Keys)
                        {
                            Console.WriteLine(currentUser.portfolios[s]);
                        }

                        while (currentUser.portfolios.ContainsKey(input) == false)
                        {
                            Console.WriteLine("Please type the name of the Portfolio you wish to delete");
                            input = Console.ReadLine();
                        }
                        string portfolioName = input;
                        currentUser.DeletePortfolio(portfolioName);

                    }
                    //User chose 0 - exit
                    if (number == 0)
                    {
                        menu = 7;
                    }

                }

                //This while loop will be called if the username wants to run the stock simulator 
                while (menu == 5)
                {
                    Console.WriteLine("This Simulator can be run at varying different levels of market volatility. Choose a volatility level");
                    Console.WriteLine("High Volatility (1), Medium Volatility (2), Low Volatility (3) or exit (0) ");
                    input = Console.ReadLine();
                    while ((Int32.TryParse(input, out number) == false) || (number > 3) || (number < 0)) {
                        Console.WriteLine("Please pick an option");
                    }
                    //User chose 1 - high volatility
                    if(number == 1)
                    {
                        Program.RunSimulator(MarketVolatility.HIGH);
                    }

                    //User chose 2 - medium volatility
                    if(number == 2)
                    {
                        Program.RunSimulator(MarketVolatility.MID);
                    } 
                    //User chose 3 - low volatility 
                    if(number == 3)
                    {
                        Program.RunSimulator(MarketVolatility.LOW);
                    }
                    //User chose 0 - exit
                    if(number == 0)
                    {
                        menu = 7;
                    }
                }

                //This while loop will be called if the username wants to log out of their account
                while (menu == 6)
                {
                    currentUser = null;
                    loggedIn = false;
                    menu = 7;

                }
                while (menu == 0)
                {
                    //Save all data to files, then close program
                    exit = true;
                    Console.WriteLine("Closing Program.....");
                    menu = -1;
                }

            }
            return;

        }
    }
}