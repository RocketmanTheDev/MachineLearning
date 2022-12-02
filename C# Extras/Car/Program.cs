using System;

namespace Car
{
    class Program
    {
        static void Main(string[] args)
        {
            Car myCar = new Car();
            
            Console.Write("Въведете марка:");
            myCar.Brand = Console.ReadLine();
            Console.Write("Въведете модел:");
            myCar.Model = Console.ReadLine();
            Console.Write("Въведете обем на двигателя:");
            myCar.EngineVolume = int.Parse(Console.ReadLine());
            Console.Write("Въведете година на производство:");
            myCar.YearPeriod = int.Parse(Console.ReadLine());

            myCar.spravka(myCar.AnnualTax());
        }
    }
}
