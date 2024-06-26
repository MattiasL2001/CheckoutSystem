﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_System
{
    internal class Campaign
    {
        public int ID;
        public int DiscountPercent;
        public string Title;

        public Campaign(int id, int discountPercent, string title)
        {
            if (discountPercent > 100) { discountPercent = 100; }
            if (discountPercent == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You can not add a new campaign without a discount!");
                Console.ResetColor();
            }

            ID = id;
            DiscountPercent = discountPercent;
            Title = title;
        }
    }
}
