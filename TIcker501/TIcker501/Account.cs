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
        public string user; 
        public static double tradeFee = 9.99;
        public static double transferFee = 4.99; 
        public double funds;
        public Dictionary<string, Portfolio> portfolios;


        //Constructor that will be used to create an Account Object by specifying all parameters
        public Account(string user, double funds, Dictionary<string, Portfolio> portfolio )
        {
            this.user = user;
            this.funds = funds;
            this.portfolios = portfolio;
        }

        //This constructor will create an Account object with an empty dictionary of portfolios
        public Account(string user, double funds)
        {
            this.user = user;
            this.funds = funds;
            this.portfolios = new Dictionary<string, Portfolio>();
        }

        //This constructor will create an Account object with an empty dictionary and no money 
        public Account(string user)
        {
            this.user = user;
            this.funds = 0.00;
            this.portfolios = new Dictionary<string, Portfolio>();
        }

        /*This function will allow users to enter an amount that they wish to deposit, and will
         * increase the balance of their account by that amount - the flat transfer fee. The user
         * must enter a valid positive number to deposit, or the function will return without making
         * any changes to the user's account. 
         */
        public void DepositFunds(double amount)
        {

        }
        
        /*This function will allow users to remove funds from their account. The user cannot withdraw 
         * an ammount from the account greater than their balance + the value of selling all of their 
         * stocks - the flat transfer fee. If the amount that the user is requesting to withdraw is 
         * greater than the balance of their account, the user will be told that they can either cancel
         * the transaction, or they must sell off some of their stocks. If the user wishes to sell off 
         * some of their stocks, they will then have to choose which stocks, and how much they want 
         * to sell 
         */
        public void WithdrawFunds(double amount)
        {


        } 

        /* This function will allow the user to view the cash balance of their account, as well as the cash
         * values of the stocks contained within and the positions balance (the percentage values of all 
         * of the stocks in the account). It will also give them the option to only view the positions 
         * balance. Finally, the user will have the option to view a Gain/Loss report that shows how 
         * much value their total portfolio has gained/lost within a certain time period, as well as 
         * the reports for individual stocks within the account. 
         */
        public void AccountBalance()
        {

        }

        /* This function will give the user the option of viewing the cash balance, positions balance, 
         * and the Gain/Loss report of their portfolio over time. 
         */
        public void PortfolioBalance()
        {

        }

        /*This private function will be called only by the AccountBalance function, and will 
         * provide the cash balance of the Account. 
         */
        private void CashBalance()
        {

        }

        /*This private function will be called by the AccountBalance function, and will
         * provide the positions balance of the account
         */
        private void PositionsBalance()
        {

        }

        /*This private function will be called by the AccountBalance function, and will
         * provide the Gain/Loss report for the account
         */
        private void GainLossReport()
        {

        }
        /*This function will be called to create a new portfolio and add it to the account's 
         * list of portfolios
         */
        public void CreatePortfolio()
        {

        }

        /*This function will be called to delete an existing portfolio, and sell all of it's 
         * positions in a single transaction. 
         */
         public void DeletePortfolio()
        {

        }

        /* This function will be used to purchase a certain amount of stock, and add it to the 
         * portfolio
         */
        public void BuyStock()
        {

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
                this.portfolios.Add(name, p);
                return;
            }
        }
    }
}
