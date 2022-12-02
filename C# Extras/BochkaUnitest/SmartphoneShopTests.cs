using NUnit.Framework;
using System;
namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
       [Test]
       public void Phone_Test() 
        {
            Smartphone smartphone = new Smartphone("s", 2000);
            Assert.AreEqual("s", smartphone.ModelName);
            Assert.AreEqual(2000, smartphone.MaximumBatteryCharge);
            Assert.AreEqual(2000, smartphone.CurrentBateryCharge);
        }
        [Test]
        public void Shop_Test()
        {
            Shop shop = new Shop(15);
            Assert.AreEqual(15, shop.Capacity);
            Assert.AreEqual(0, shop.Count);
        }
        [Test]
        public void Shop_Capacity_Test()
        {
            Shop shop = new Shop(15);
            Assert.AreEqual(15, shop.Capacity);
            Assert.Throws<ArgumentException>(() => new Shop(-1));
        }
        [Test]
        public void Shop_Add_Test()
        {
            Shop shop = new Shop(1);
            Smartphone smartphone = new Smartphone("s", 2000);
            shop.Add(smartphone);
            Assert.AreEqual(1, shop.Count);
            Assert.Throws<InvalidOperationException>(() => shop.Add(new Smartphone("s", 2000)));
            Assert.Throws<InvalidOperationException>(() => shop.Add(new Smartphone("a", 2000)));
        }
        [Test]
        public void Shop_Remove_Test()
        {
            Shop shop = new Shop(1);
            Smartphone smartphone = new Smartphone("s", 2000);
            shop.Add(smartphone);
            Assert.Throws<InvalidOperationException>(() => shop.Remove("b"));
            shop.Remove(smartphone.ModelName);
            Assert.AreEqual(0, shop.Count);  
        }
        [Test]
        public void Shop_Test_Test()
        {
            Shop shop = new Shop(1);
            Smartphone smartphone = new Smartphone("s", 2000);
            shop.Add(smartphone);
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("b",150));
            Assert.Throws<InvalidOperationException>(() => shop.TestPhone("s",2500));
            shop.TestPhone("s", 200);
            Assert.AreEqual(1800,smartphone.CurrentBateryCharge);
        }
        [Test]
        public void Shop_Charge_Test()
        {
            Shop shop = new Shop(1);
            Smartphone smartphone = new Smartphone("s", 2000);
            shop.Add(smartphone);
            Assert.Throws<InvalidOperationException>(() => shop.ChargePhone("b"));
            shop.TestPhone("s", 200);
            shop.ChargePhone("s");
            Assert.AreEqual(2000, smartphone.CurrentBateryCharge);
        }
    }
}