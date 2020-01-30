using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PoulpApp.Models
{
    public class Product
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get; set; }

        public Product(string id, int amount, double unitPrice)
        {
            Id = id;
            Amount = amount;
            UnitPrice = unitPrice;
        }
    }

    public class Cart
    {
        public List<Product> ItemList { get; set; }

        public Cart()
        {
            ItemList = new List<Product>();
        }

        public void AddProduct(string id, int amount, double unitPrice)
        {
            if (amount  < 1) { return; }

            bool itemAlreadyInList = ItemList.FindAll(x => x.Id == id).Count > 0;

            if (itemAlreadyInList)
            {
                ItemList.Last(x => x.Id == id).Amount += amount;
            }
            else
            {
                ItemList.Add(new Product(id, amount, unitPrice));
            }
        }
    }
}
