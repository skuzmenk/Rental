using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalLibrary
{
    public class VehicleDTO
    {
        public Category Category { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public string VehicleNumber { get; set; }
    }
    public enum Category
    {
        Sedan,
        SUV,
        Cabriolet,
        Sports
    }
}
