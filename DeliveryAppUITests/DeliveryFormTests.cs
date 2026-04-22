using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace DeliveryAppUITests
{
    [TestClass]
    public class DeliveryFormTests
    {
        private FlaUI.Core.Application app;
        private AutomationBase automation;
        private Window window;

        [TestInitialize]
        public void Init()
        {
            if (File.Exists("deliveries.txt"))
            {
                File.Delete("deliveries.txt");
            }
            //app = FlaUI.Core.Application.Launch(@"C:\Other\Documents\College\Testing\Лабораторные\Лр2\DeliveryApp\DeliveryApp\bin\Debug\DeliveryApp.exe");
            app = FlaUI.Core.Application.Launch("DeliveryApp.exe");

            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);
        }

        [TestMethod]
        public void AddDelivery_ValidData_AddsToList()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/Combobox")?.AsComboBox();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            Assert.AreEqual(1, list.Items.Length);
        }

        [TestMethod]
        public void AddDelivery_EmptyCustomerName_ShowsError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/Combobox")?.AsComboBox();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            address?.Enter("Бармалеева улица");
            addButton?.Click();

            var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();

            Assert.IsTrue(!errow_window?.Equals(null));

            errow_window.Close();
        }

        [TestMethod]
        public void AddDelivery_EmptyAddress_ShowsError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/Combobox")?.AsComboBox();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Бармалеева улица");
            addButton?.Click();

            var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();

            Assert.IsTrue(!errow_window?.Equals(null));

            errow_window.Close();
        }

        [TestMethod]
        public void AddDelivery_ValidData_ClearsTextBoxes()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/Combobox")?.AsComboBox();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            Assert.AreEqual("", customerName.Text);
            Assert.AreEqual("", address.Text);
        }


        [TestCleanup]
        public void Cleanup()
        {
            app?.Close();
            app?.Dispose();
            automation?.Dispose();

            if (File.Exists("deliveries.txt"))
            {
                File.Delete("deliveries.txt");
            }
        }
    }
}
