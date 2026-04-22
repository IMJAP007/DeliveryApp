using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeliveryAppTests
{
    [TestClass]
    public class DeliveryManagerTests
    {
        [TestInitialize]
        public void SetUp()
        {
            if (File.Exists("deliveries.txt"))
            {
                File.Delete("deliveries.txt");
            }
        }

        [TestMethod]
        public void AddDelivery_ValidDelivery_Adds_to_List()
        {
            DeliveryManager dm = new DeliveryManager();

            Delivery delivery = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);

            dm.AddDelivery(delivery);

            Assert.IsTrue(dm.Deliveries.Contains(delivery));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddDelivery_Null_Throws_Argument_Null_Exception()
        {
            DeliveryManager dm = new DeliveryManager();

            dm.AddDelivery(null);
        }

        [TestMethod]
        public void AddDelivery_SavesToFile()
        {
            DeliveryManager dm = new DeliveryManager();

            Delivery delivery1 = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);
            Delivery delivery2 = new Delivery("Николай", "улица Рубинштейна", DateTime.Now);

            dm.AddDelivery(delivery1);
            dm.AddDelivery(delivery2);

            Assert.IsTrue(File.Exists("deliveries.txt"));

            if (File.Exists("deliveries.txt"))
            {
                var lines = File.ReadLines("deliveries.txt");
                
                Assert.IsTrue(lines.Contains($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|0"));
                Assert.IsTrue(lines.Contains($"Николай|улица Рубинштейна|{DateTime.Now.ToString("yyyy-MM-dd")}|0"));
            }
        }
    }
}
