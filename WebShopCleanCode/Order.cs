using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public class Order
    {
        public string Name { get; set; }
        public int BoughtFor { get; set; }
        public DateTime PurchaseTime { get; set; }
        public Order(string name, int boughtFor, DateTime purchaseTime)
        {
            Name = name;
            BoughtFor = boughtFor;
            PurchaseTime = purchaseTime;
        }
        public void PrintInfo()
        {
            Console.WriteLine(Name + ", bought for " + BoughtFor + "kr, time: " + PurchaseTime + ".");
        }
    }
}
