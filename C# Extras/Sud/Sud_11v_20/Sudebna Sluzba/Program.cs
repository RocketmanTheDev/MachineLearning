using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sud_11v_20
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Въведете име на подсъдим:");
            string name = Console.ReadLine();
            Console.WriteLine("Въведете адрес на подсъдим:");
            string adress = Console.ReadLine();
            Console.WriteLine("Въведете възраст на подсъдим:");
            byte age = byte.Parse(Console.ReadLine());
            Console.WriteLine("Въведете Провиненост(Осъждан, Не е осъждан):");
            string convicted = Console.ReadLine();


            Person Podsadim2 = new Person(name, adress, age, convicted);
            Person Podsadim3 = new Person(Podsadim2);
            Podsadim2.Print();
        }
    }
}
