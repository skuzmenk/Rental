using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalLibrary;


namespace Rental
{
    

    public class Car
    {
        public string Manufacturer { get; }
        public string Model { get; }
        public int Year { get; }
        public double Price { get; }

        public Car(string manufacturer, string model, int year, double price)
        {
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            Price = price;
        }

        public override string ToString() => $"{Manufacturer} {Model} ({Year}) - {Price} грн";
    }

    public class Vehicle
    {
        private readonly string _companyName = "ТОВ АвтоПрокат";
        public Category Category { get; }
        public Car Car { get; }
        public DateTime StartDate { get; }
        public double Cost { get; }
        public string VehicleNumber { get; }

        public Vehicle(Category category, Car car, DateTime startDate, double cost, int duration, string vehicleNumber)
        {
            Category = category;
            Car = car;
            StartDate = startDate;
            Cost = cost;
            VehicleNumber = vehicleNumber;
        }

        public override string ToString()
        {
            return $"{_companyName}: {Car} - {Category} - Номер: {VehicleNumber} - Вартість: {Cost} грн";
        }
        public string ToShortString()
        {
            return $"{_companyName} | {StartDate.ToShortDateString()} | {Cost} грн";
        }
    }
}
