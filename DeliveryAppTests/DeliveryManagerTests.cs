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
        DeliveryManager dm;

        [TestInitialize]
        public void SetUp()
        {
            dm = new DeliveryManager();

            if (File.Exists("deliveries.txt"))
            {
                File.Delete("deliveries.txt");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            dm.Deliveries.Clear();

            if (File.Exists("deliveries.txt"))
            {
                File.Delete("deliveries.txt");
            }
        }

        [TestMethod]
        public void AddDelivery_ValidDelivery_Adds_to_List()
        {
            Delivery delivery = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);

            dm.AddDelivery(delivery);

            Assert.IsTrue(dm.Deliveries.Contains(delivery));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddDelivery_Null_Throws_Argument_Null_Exception()
        {
            dm.AddDelivery(null);
        }

        [TestMethod]
        public void AddDelivery_SavesToFile()
        {
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

        [TestMethod]
        public void RemoveDelivery_ExistingDelivery_RemovesFromList()
        {
            Delivery delivery1 = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);
            Delivery delivery2 = new Delivery("Николай", "улица Рубинштейна", DateTime.Now);

            dm.AddDelivery(delivery1);
            dm.AddDelivery(delivery2);
            dm.RemoveDelivery(delivery1);

            Assert.IsTrue(!dm.Deliveries.Contains(delivery1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveDelivery_NullDelivery_ThrowsArgumentNullException()
        {
            dm.RemoveDelivery(null);
        }

        [TestMethod]
        public void RemoveDelivery_NonExistingDelivery_NoChanges()
        {
            Delivery delivery1 = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);
            Delivery delivery2 = new Delivery("Николай", "улица Рубинштейна", DateTime.Now);
            Delivery delivery3 = new Delivery("Борис", "улица Шишкина", DateTime.Now);

            dm.AddDelivery(delivery1);
            dm.AddDelivery(delivery2);
            dm.RemoveDelivery(delivery3);

            Assert.AreEqual(2, dm.Deliveries.Count);
            Assert.IsTrue(dm.Deliveries.Contains(delivery1));
            Assert.IsTrue(dm.Deliveries.Contains(delivery2));
        }

        [TestMethod]
        public void RemoveDelivery_SavesToFile()
        {
            Delivery delivery1 = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);
            Delivery delivery2 = new Delivery("Николай", "улица Рубинштейна", DateTime.Now);
            Delivery delivery3 = new Delivery("Борис", "улица Шишкина", DateTime.Now);

            dm.AddDelivery(delivery1);
            dm.AddDelivery(delivery2);
            dm.AddDelivery(delivery3);
            dm.RemoveDelivery(delivery3);

            if (File.Exists("deliveries.txt"))
            {
                var lines = File.ReadLines("deliveries.txt");
                Assert.IsTrue(lines.Contains($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|0"));
                Assert.IsTrue(lines.Contains($"Николай|улица Рубинштейна|{DateTime.Now.ToString("yyyy-MM-dd")}|0"));
                Assert.IsTrue(!lines.Contains($"Борис|улица Шишкина|{DateTime.Now.ToString("yyyy-MM-dd")}|0"));
            }
        }

        [TestMethod]
        public void UpdateDeliveryStatus_ValidDelivery_UpdatesStatus()
        {
            Delivery delivery1 = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);

            dm.AddDelivery(delivery1);

            Delivery target = dm.Deliveries.Find(d => d.CustomerName == "Андрей" && d.Address == "Бармалеева улица");

            dm.UpdateDeliveryStatus(target, DeliveryStatus.В_пути);

            DeliveryStatus expected = DeliveryStatus.В_пути;
            DeliveryStatus actual = target.Status;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateDeliveryStatus_NullDelivery_ThrowsArgumentNullException()
        {
            dm.UpdateDeliveryStatus(null, DeliveryStatus.В_пути);
        }

        [TestMethod]
        public void UpdateDeliveryStatus_SavesToFile()
        {
            Delivery delivery1 = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);

            dm.AddDelivery(delivery1);

            Delivery target = dm.Deliveries.Find(d => d.CustomerName == "Андрей" && d.Address == "Бармалеева улица");

            dm.UpdateDeliveryStatus(target, DeliveryStatus.В_пути);

            if (File.Exists("deliveries.txt"))
            {
                var lines = File.ReadLines("deliveries.txt");
                Assert.IsTrue(lines.Contains($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|1"));
            }
        }
    }
}
