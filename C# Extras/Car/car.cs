using System;
using System.Collections.Generic;
using System.Text;

namespace Car
{
    public class Car
    {
        private string brand;
        private string model;
        private int engineVolume;
        private int yearPeriod;

        public string Brand
        {
            set { brand = value; }
            get { return this.brand; }
        }
        public string Model
        {
            set { model = value; }
            get { return this.model; }
        }
        public int EngineVolume
        {
            set { if (value >= 1000 && value <= 3000) this.engineVolume = value; else Console.WriteLine("Невалиден Обем на Двигателя"); }
            get { return this.engineVolume; }
        }
        public int YearPeriod
        {
            set { if (value >= 1980 && value <= 2021) this.yearPeriod = value; else Console.WriteLine("Невалидна Година"); }
            get { return this.yearPeriod; }
        }


        public void spravka(double n)
        {
            Console.WriteLine("Данъкът на {0}, {1}, ", this.brand, this.model);
            Console.WriteLine("с обем на двигателя: {0}, ", this.engineVolume);
            Console.WriteLine("Произведена през {0} година, ", this.yearPeriod);
            Console.WriteLine("e {0,6:###.00}лв.", n);

        }

        public double AnnualTax()

        {
            double annualTax = 0.2 * this.engineVolume;
            if (this.yearPeriod <= 2000)
            {

                annualTax += 70.00;

            }
            else if (this.yearPeriod <= 2010)
            {
                annualTax += 60.00;
            }
            else annualTax += 50.00;
            return annualTax;
        }
    }
}
