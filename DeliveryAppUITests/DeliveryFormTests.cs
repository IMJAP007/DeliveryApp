using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
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
            //if (File.Exists("deliveries.txt"))
            //{
            //    File.Delete("deliveries.txt");
            //}
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
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            date.SelectedDate = DateTime.Now;
            addButton?.Click();

            Assert.AreEqual(1, list.Items.Length);
        }

        [TestMethod]
        public void AddDelivery_EmptyCustomerName_ShowsError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            address?.Enter("Бармалеева улица");
            date.SelectedDate = DateTime.Now;
            addButton?.Click();

            var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();
            var ok_button = window.FindFirstByXPath("/Window/Button")?.AsWindow();

            Assert.IsTrue(!errow_window?.Equals(null));

            ok_button?.Click();
        }

        [TestMethod]
        public void AddDelivery_EmptyAddress_ShowsError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            addButton?.Click();

            var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();
            var ok_button = window.FindFirstByXPath("/Window/Button")?.AsWindow();

            Assert.IsTrue(!errow_window?.Equals(null));

            ok_button?.Click();
        }

        [TestMethod]
        public void AddDelivery_EmptyDate_NoError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();


            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();

            Assert.IsTrue(errow_window == null);
        }

        [TestMethod]
        public void AddDelivery_ValidData_ClearsTextBoxes()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

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

        [TestMethod]
        public void RemoveDelivery_NoSelection_ShowsError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            deleteButton?.Click();

            var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();
            var ok_button = window.FindFirstByXPath("/Window/Button")?.AsWindow();

            Assert.IsTrue(!errow_window?.Equals(null));

            ok_button?.Click();
        }

        [TestMethod]
        public void RemoveDelivery_ValidSelection_RemovesFromList()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            customerName?.Enter("Николай");
            address?.Enter("улица Рубинштейна");
            addButton?.Click();

            list?.Select(0);

            deleteButton?.Click();

            Assert.AreEqual(1, list.Items.Length);
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Николай") && item.Text.Contains("улица Рубинштейна")));
        }

        [TestMethod]
        public void UpdateStatus_NoSelection_ShowsError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            updateButton?.Click();

            var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();
            var ok_button = window.FindFirstByXPath("/Window/Button")?.AsWindow();

            Assert.IsTrue(!errow_window?.Equals(null));

            ok_button?.Click();
        }

        [TestMethod]
        public void UpdateStatus_ValidSelectionInProgress_UpdatesStatus()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            list.Select(0);
            status?.Expand();
            status?.Select(1);
            status?.Collapse();
            updateButton?.Click();

            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Андрей") && item.Text.Contains("Бармалеева улица") && item.Text.Contains("В_пути")));
        }

        private void SetupDataForSelectionNew()
        {
            using (StreamWriter sw = new StreamWriter("deliveries.txt"))
            {
                sw.WriteLine($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|1");
            }
        }

        [TestMethod]
        public void UpdateStatus_ValidSelectionNew_UpdatesStatus()
        {
            // Вызывает исключение
            //var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            //var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            //var status = window.FindFirstByXPath("/ComboBox[1]")?.AsComboBox();
            //var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            //var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            //var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            //var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            //var list = window.FindFirstByXPath("/List")?.AsListBox();

            //customerName?.Enter("Андрей");
            //address?.Enter("Бармалеева улица");
            //addButton?.Click();

            //status?.Expand();
            //status?.Select(1);
            //status?.Collapse();
            //list?.Select(0);
            //updateButton?.Click();

            //list?.Select(0);
            //status?.Expand();
            //status?.Select(0);
            //status?.Collapse();
            //updateButton?.Click();

            //Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Андрей") && item.Text.Contains("Бармалеева улица") && item.Text.Contains("Новый")));

            SetupDataForSelectionNew();
            app.Kill();
            app = Application.Launch("DeliveryApp.exe");
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);

            var status = window.FindFirstByXPath("/ComboBox[1]")?.AsComboBox();
            var list = window.FindFirstByXPath("/List")?.AsListBox();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            list?.Select(0);
            status?.Expand();
            status?.Select(0);
            status?.Collapse();
            updateButton?.Click();

            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Андрей") && item.Text.Contains("Бармалеева улица") && item.Text.Contains("Новый")));

        }

        [TestMethod]
        public void UpdateStatus_ValidSelectionCompleted_UpdatesStatus()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            list.Select(0);
            status?.Expand();
            status?.Select(2);
            status?.Collapse();
            updateButton?.Click();

            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Андрей") && item.Text.Contains("Бармалеева улица") && item.Text.Contains("Доставлен")));
        }

        [TestMethod]
        public void CheckFileLineFormat_Adding()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            var lines = File.ReadLines("deliveries.txt");
            Assert.IsTrue(lines.Contains($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|0"));
        }

        [TestMethod]
        public void CheckFileLineFormat_EditingStatus()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            list.Select(0);
            status?.Expand();
            status?.Select(1);
            status?.Collapse();
            updateButton?.Click();

            var lines = File.ReadLines("deliveries.txt");
            Assert.IsTrue(lines.Contains($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|1"));
        }

        [TestMethod]
        public void CheckFileLineFormat_Deleting()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();
            
            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            list.Select(0);
            deleteButton?.Click();

            var lines = File.ReadLines("deliveries.txt");
            Assert.IsTrue(lines.Count() == 0);
        }

        private void SetupData()
        {
            using (StreamWriter sw = new StreamWriter("deliveries.txt"))
            {
                sw.WriteLine($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|0");
                sw.WriteLine($"Николай|улица Шишкина|{DateTime.Now.ToString("yyyy-MM-dd")}|2");
                sw.WriteLine($"Артем|ВДНХ|{DateTime.Now.ToString("yyyy-MM-dd")}|1");
                sw.WriteLine($"Михаил|Ребро|{DateTime.Now.ToString("yyyy-MM-dd")}|1");
                sw.WriteLine($"Виктор|Площадь мост|{DateTime.Now.ToString("yyyy-MM-dd")}|2");
                sw.WriteLine($"Влад|Осиновая роща|{DateTime.Now.ToString("yyyy-MM-dd")}|0");
            }
        }

        [TestMethod]
        public void Filtering()
        {
            SetupData();

            app.Kill();
            app = Application.Launch("DeliveryApp.exe");
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);

            var list = window.FindFirstByXPath("/List")?.AsListBox();
            var select_status = window.FindFirstByXPath("/ComboBox[2]")?.AsComboBox();
            var filter = window.FindFirstByXPath("/Button[4]")?.AsButton();
            var cancel = window.FindFirstByXPath("/Button[5]")?.AsButton();

            select_status?.Expand();
            select_status?.Select(0);
            select_status?.Collapse();
            filter?.Click();

            Assert.IsTrue(list?.Items.Count() == 2);
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Андрей") && item.Text.Contains("Бармалеева улица") && item.Text.Contains("Новый")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Влад") && item.Text.Contains("Осиновая роща") && item.Text.Contains("Новый")));

            select_status?.Expand();
            select_status?.Select(1);
            select_status?.Collapse();
            filter?.Click();

            Assert.IsTrue(list?.Items.Count() == 2);
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Артем") && item.Text.Contains("ВДНХ") && item.Text.Contains("В_пути")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Михаил") && item.Text.Contains("Ребро") && item.Text.Contains("В_пути")));

            select_status?.Expand();
            select_status?.Select(2);
            select_status?.Collapse();
            filter?.Click();

            Assert.IsTrue(list?.Items.Count() == 2);
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Николай") && item.Text.Contains("улица Шишкина") && item.Text.Contains("Доставлен")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Виктор") && item.Text.Contains("Площадь мост") && item.Text.Contains("Доставлен")));

            cancel?.Click();
            Assert.IsTrue(list?.Items.Count() == 6);
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Андрей") && item.Text.Contains("Бармалеева улица") && item.Text.Contains("Новый")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Влад") && item.Text.Contains("Осиновая роща") && item.Text.Contains("Новый")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Артем") && item.Text.Contains("ВДНХ") && item.Text.Contains("В_пути")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Михаил") && item.Text.Contains("Ребро") && item.Text.Contains("В_пути")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Николай") && item.Text.Contains("улица Шишкина") && item.Text.Contains("Доставлен")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Виктор") && item.Text.Contains("Площадь мост") && item.Text.Contains("Доставлен")));
        }

        [TestMethod]
        public void Filtering_Delete()
        {
            SetupData();

            app.Kill();
            app = Application.Launch("DeliveryApp.exe");
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);

            var list = window.FindFirstByXPath("/List")?.AsListBox();
            var select_status = window.FindFirstByXPath("/ComboBox[2]")?.AsComboBox();
            var filter = window.FindFirstByXPath("/Button[4]")?.AsButton();
            var cancel = window.FindFirstByXPath("/Button[5]")?.AsButton();

            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            select_status?.Expand();
            select_status?.Select(1);
            select_status?.Collapse();
            filter?.Click();

            list?.Select(0);
            deleteButton?.Click();

            Assert.IsTrue(list?.Items.Count() == 5);
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Андрей") && item.Text.Contains("Бармалеева улица") && item.Text.Contains("Новый")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Влад") && item.Text.Contains("Осиновая роща") && item.Text.Contains("Новый")));
            Assert.IsTrue(!list?.Items.Any(item => item.Text.Contains("Артем") && item.Text.Contains("ВДНХ") && item.Text.Contains("В_пути")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Михаил") && item.Text.Contains("Ребро") && item.Text.Contains("В_пути")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Николай") && item.Text.Contains("улица Шишкина") && item.Text.Contains("Доставлен")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Виктор") && item.Text.Contains("Площадь мост") && item.Text.Contains("Доставлен")));
        }

        [TestMethod]
        public void Filtering_Update()
        {
            SetupData();

            app.Kill();
            app = Application.Launch("DeliveryApp.exe");
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);

            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();

            var list = window.FindFirstByXPath("/List")?.AsListBox();
            var select_status = window.FindFirstByXPath("/ComboBox[2]")?.AsComboBox();
            var filter = window.FindFirstByXPath("/Button[4]")?.AsButton();
            var cancel = window.FindFirstByXPath("/Button[5]")?.AsButton();

            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            select_status?.Expand();
            select_status?.Select(1);
            select_status?.Collapse();
            filter?.Click();

            list?.Select(0);

            status?.Expand();
            status?.Select(2);
            status?.Collapse();
            updateButton?.Click();

            select_status?.Expand();
            select_status?.Select(1);
            select_status?.Collapse();
            filter?.Click();

            Assert.IsTrue(list?.Items.Count() == 1);
            Assert.IsTrue(!list?.Items.Any(item => item.Text.Contains("Артем") && item.Text.Contains("ВДНХ") && item.Text.Contains("В_пути")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Михаил") && item.Text.Contains("Ребро") && item.Text.Contains("В_пути")));

            select_status?.Expand();
            select_status?.Select(2);
            select_status?.Collapse();
            filter?.Click();

            Assert.IsTrue(list?.Items.Count() == 3);
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Николай") && item.Text.Contains("улица Шишкина") && item.Text.Contains("Доставлен")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Артем") && item.Text.Contains("ВДНХ") && item.Text.Contains("Доставлен")));
            Assert.IsTrue(list?.Items.Any(item => item.Text.Contains("Виктор") && item.Text.Contains("Площадь мост") && item.Text.Contains("Доставлен")));

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
