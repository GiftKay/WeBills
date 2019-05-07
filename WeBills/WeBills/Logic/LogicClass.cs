using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeBills.Models;

namespace WeBills.Logic
{
    public class LogicClass
    {
        Random random = new Random();
        public double getRandom(double low, double high)
        {
            if (low >= high)
            {
                throw new ArgumentOutOfRangeException();
            }

            return random.NextDouble() * (high - low) + low;
        }

        ApplicationDbContext db = new ApplicationDbContext();
        public int calcAge()
        {
            int result = 0;

            //ApplicationUser user;

            //string idd;
            //idd = user.idno;

            return result;
        }
    }
}