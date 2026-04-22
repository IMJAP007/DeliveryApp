using System;
using System.Collections.Generic;
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
        [TestMethod]
        public void AddDelivery_ValidDelivery_Adds_to_List()
        {
            DeliveryManager dm = new DeliveryManager();

            Delivery delivery = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);

            dm.AddDelivery(delivery);

            Assert.IsTrue(dm.Deliveries.Contains(delivery));
        }
    }
}
