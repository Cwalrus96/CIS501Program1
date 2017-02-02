using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticker501
{
    class User
    {
        List<Portfolio> portfolios;
        string name; 

        //This constructor will produce a new user with a blank list of portfolios.
        public User(string name)
        {
            this.name = name;
            this.portfolios = new List<Portfolio>();
        }


        public void AddPortfolio(Portfolio p)
        {
            if(this.portfolios.Count == 3)
            {
                Console.WriteLine("User already has 3 portfolios - action failed");
                return;
            }
            else
            {
                this.portfolios.Add(p);
            }

        } 
        
    }
}
