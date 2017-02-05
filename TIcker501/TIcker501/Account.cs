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
    /*This class will be used to create Account objects, which represent different users and contain 
     * a collection of portfolios and a certain amount of unspecified funds. Users will be able to 
     * view their account balance, look at the funds in the total account and individual portfolios, 
     * and see how different stocks make up their portfolios. Users will also be able to buy new stocks,
     * and add and withdraw funds from their account. 
     */

    class Account
    {
        public string username;
        public static double tradeFee = 9.99;
        public static double transferFee = 4.99;
        public double funds;
        public Dictionary<string, Portfolio> portfolios;


        //Constructor that will be used to create an Account Object by specifying all parameters
        public Account(string user, double funds, Dictionary<string, Portfolio> portfolio)
        {
            this.username = user;
            this.funds = funds;
            this.portfolios = portfolio;
        }

        //This constructor will create an Account object with an empty dictionary of portfolios
        public Account(string user, double funds)
        {
            this.username = user;
            this.funds = funds;
            this.portfolios = new Dictionary<string, Portfolio>();
        }

        //This constructor will create an Account object with an empty dictionary and no money 
        public Account(string user)
        {
           
            this.username = user;
            this.funds = 0.00;
            this.portfolios = new Dictionary<string, Portfolio>();
        }

        /*This function will allow users to enter an amount that they wish to deposit, and will
         * increase the balance of their account by that amount - the flat transfer fee. The username
         * must enter a valid positive number to deposit, or the function will return without making
         * any changes to the username's account. 
         */
        public void DepositFunds(double amount)
        {

            this.funds += amount - Account.transferFee;
        }

        /*This function will allow users to remove funds from their account. The username cannot withdraw 
         * an ammount from the account greater than their balance + the value of selling all of their 
         * stocks - the flat transfer fee. If the amount that the username is requesting to withdraw is 
         * greater than the balance of their account, the username will be told that they can either cancel
         * the transaction, or they must sell off some of their stocks. If the username wishes to sell off 
         * some of their stocks, they will then have to choose which stocks, and how much they want 
         * to sell 
         */
        public void WithdrawFunds(double amount)
        {
            this.funds -= (amount + Account.transferFee);

        }


        /*This private function will be called by the AccountBalance function, and will
         * provide the positions balance of the account
         */
        public void PositionsBalance()
        {
            double totalValue = 0;
            int totalNumber = 0;
            foreach (string p in this.portfolios.Keys)
            {
                foreach (string s in this.portfolios[p].stocks.Keys)
                {
                    totalNumber += this.portfolios[p].amounts[s];
                    totalValue += (this.portfolios[p].stocks[s].price * this.portfolios[p].amounts[s]);
                }
            }
            foreach (string s in this.portfolios.Keys)
            {
                this.portfolios[s].ViewPortfolio();
            }
            Console.WriteLine("Total Number of Stocks: " + totalNumber);
            Console.WriteLine("Total Stock Value in Account: $" + totalValue);
            Console.WriteLine("Account Cash Value: $" + this.funds);
            Console.WriteLine("Total Account Value (Cash + Stocks: $" + (totalValue + this.funds));
        }

        /*This function will be called to create a new portfolio and add it to the account's 
         * list of portfolios
         */
        public void CreatePortfolio(string name)
        {
            Portfolio p = new Ticker501.Portfolio(name);
            this.addPortfolio(name, p);
        }

        /*This function will be called to delete an existing portfolio, and sell all of it's 
         * positions in a single transaction. 
         */
        public void DeletePortfolio(string name)
        {
            if (this.portfolios.ContainsKey(name))
            {
                //TODO: Make sure removing a portfolio sells all stocks contained within that portfolio
                this.portfolios.Remove(name);
                double totalAmount = 0;
                foreach (string s in this.portfolios[name].stocks.Keys)
                {
                    totalAmount += (this.portfolios[name].stocks[s].price) * this.portfolios[name].amounts[s];
                }
                totalAmount -= Account.tradeFee;
                this.funds += totalAmount;
                Console.WriteLine("All Stocks have been sold, and Portfolio has successfully been removed");
                Console.WriteLine("Your new account balance = $" + funds);
            }
            else
            {
                Console.WriteLine("This username does not have a portfolio named " + name + ". Action failed");
            }

        }

        /* This function will be used to purchase a certain amount of stock, and add it to the 
         * portfolio
         */
        public bool BuyStock(Portfolio p, Stock s, int amount)
        {
            //See if username has enough funds to purchase the selected stocks
            double price = s.price * amount;

            if (this.funds < price + Account.tradeFee)
            {
                Console.WriteLine("You do not have enough funds to purchase these stocks.");
                return false;
            }
            //see if username already owns stocks of that type 
            if (p.stocks.ContainsKey(s.ticker))
            {
                p.amounts[s.ticker] += amount;
            }
            else
            {
                p.stocks.Add(s.ticker, s);
                p.amounts.Add(s.ticker, amount);
            }
            this.funds -= price + Account.tradeFee;
            p.AddStock(s, amount);
            return true;
        }

        //This function will be used to sell a certain amount of stock, and add the funds to your account balance
        public bool SellStock(Portfolio p, Stock s, int amount)
        {
            if (amount < p.amounts[s.ticker])
            {
                p.amounts[s.ticker] -= amount;
                Console.WriteLine("Sold " + amount + " shares of " + s.companyName + " stock.");
                double money = (s.price * amount) - Account.tradeFee;
                Console.WriteLine("Added $" + money + " to your account");
                this.funds += money;
                return true;
            }
            else if (amount == p.amounts[s.ticker])
            {
                p.amounts.Remove(s.ticker);
                p.stocks.Remove(s.ticker);
                Console.WriteLine("Sold all of your " + s.companyName + " stock.");
                double money = (s.price * amount) - Account.tradeFee;
                Console.WriteLine("Added $" + money + " to your account");
                this.funds += money;
                return true;
            }
            else if (amount > p.amounts[s.ticker])
            {
                Console.WriteLine("You do not have enough of that stock to sell");
                return false;
            }
            return false;
        }

        public void addPortfolio(string name, Portfolio p)
        {
            if (this.portfolios.Count == 3)
            {
                Console.WriteLine("User already has 3 portfolios - action failed");
                return;
            }
            else
            {
                //Console.WriteLine("Portfolio Successfully Added!");
                Console.WriteLine("Portfolio " + name + " successfully added");
                this.portfolios.Add(name, p);
                return;
            }
        }
    }
}
