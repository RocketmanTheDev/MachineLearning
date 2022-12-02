using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sud_11v_20
{
    class Person
    {
        private string name;
        public string Name
        {
            set { this.name = value; }
            get { return this.name; }
        }
        private string adress;
        public string Adress
        {
            set { this.adress = value; }
            get { return this.adress; }
        }
        private byte age;
        public byte Age
        {
            set { if (value > 17 && value < 99) this.age = value; else Console.WriteLine("Невалидна стойност за години на подсъдимия/та"); }
            get { return this.age; }
        }
        private string convicted;
        public string Convicted
        {
            set {
                if (value == "Осъждан")
                {
                    Console.WriteLine("Въведете срок на присъда в месеци:");
                    this.PeriodSentence = int.Parse(Console.ReadLine());
                    this.convicted = value;
                }
                else if (value == "Не е осъждан") { this.periodSentence = null; this.convicted = value; }
                else Console.WriteLine("Невалидна стойност за присъда");
                }
            get { return "0";}
        }
        private int? periodSentence;
        public int? PeriodSentence
        {
            set { if (value == 0) this.periodSentence = null; else this.periodSentence = value; }
            get
            { return this.periodSentence; }
        }
        public Person(string name, string adress, byte age, string convicted)
        {
            this.name = name;
            this.adress = adress;
            this.age = age;
            this.convicted = convicted;
            if (this.convicted == "Осъждан")
            {
                Console.WriteLine("Въведете срок на присъда в месеци:");
                this.PeriodSentence = int.Parse(Console.ReadLine());
            }
            else if (this.convicted == "Не е осъждан") { this.periodSentence = null;}
            else Console.WriteLine("Невалидна стойност за присъда");
        }
        public Person(Person z)
        {
            this.name = z.name;
            this.adress = z.adress;
            this.age = z.age;
            this.convicted = z.convicted;
            this.periodSentence = z.periodSentence;
        }
        public void Print()
        {
            Console.WriteLine("                       СПРАВКА ЗА ЛИЦЕТО      ");
            Console.WriteLine("Име: {0}                                     Адрес: {1}   ",this.name, this.adress);
            Console.WriteLine("Възраст: {0}", this.age);
            if (this.periodSentence == null) Console.WriteLine("                     ЛИЦЕТО НЕ Е ОСЪЖДАНО!");
            else if (this.periodSentence < 12) Console.WriteLine("      ЛИЦЕТО Е ОСЪЖДАНО! Срок на присъдата: {0} месец/а.", this.periodSentence);
            else if (this.periodSentence >= 12 && this.periodSentence % 12 == 0) Console.WriteLine("      ЛИЦЕТО Е ОСЪЖДАНО! Срок на присъдата: {0} година/и.", this.periodSentence / 12);
            else Console.WriteLine("      ЛИЦЕТО Е ОСЪЖДАНО! Срок на присъдата {0} година/и и {1} месец/а", this.periodSentence / 12, this.periodSentence % 12);
        }
    }
}
