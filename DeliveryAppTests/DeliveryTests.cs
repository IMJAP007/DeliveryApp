using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DeliveryApp;

namespace DeliveryAppTests
{
    [TestClass]
    public class DeliveryTests
    {
        [TestMethod]
        public void Create_Delivery_Works_Correct_Status_Equals_New()
        {
            Delivery delivery = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);

            string expected_name = "Андрей";
            string actual_name = delivery.CustomerName;

            string expected_address = "Бармалеева улица";
            string actual_address = delivery.Address;

            DateTime expected_date = DateTime.Now;
            DateTime actual_date = delivery.DeliveryDate;

            DeliveryStatus expected_status = DeliveryStatus.Новый;
            DeliveryStatus actual_status = delivery.Status;

            Assert.AreEqual(expected_name, actual_name);
            Assert.AreEqual (expected_address, actual_address);
            Assert.AreEqual(expected_date, actual_date);
            Assert.AreEqual(expected_status, actual_status);
        }

        [TestMethod]
        public void Change_Delivery_Attributes_Works_Correct()
        {
            Delivery delivery = new Delivery("Андрей", "Бармалеева улица", DateTime.Now);

            delivery.CustomerName = "Николай";
            delivery.Address = "улица Рубинштейна";
            delivery.DeliveryDate = new DateTime(2026, 5, 12);
            delivery.UpdateStatus(DeliveryStatus.В_пути);

            string expected_name = "Николай";
            string actual_name = delivery.CustomerName;

            string expected_address = "улица Рубинштейна";
            string actual_address = delivery.Address;

            DateTime expected_date = new DateTime(2026, 5, 12);
            DateTime actual_date = delivery.DeliveryDate;

            DeliveryStatus expected_status = DeliveryStatus.В_пути;
            DeliveryStatus actual_status = delivery.Status;

            Assert.AreEqual(expected_name, actual_name);
            Assert.AreEqual(expected_address, actual_address);
            Assert.AreEqual(expected_date, actual_date);
            Assert.AreEqual(expected_status, actual_status);
        }
    }
}
