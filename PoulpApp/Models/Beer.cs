using System;

namespace PoulpApp.Models
{
    public class Beer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Degree { get; set; }
        public int Volume { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Uri PosterPath { get; set; }
    }
}