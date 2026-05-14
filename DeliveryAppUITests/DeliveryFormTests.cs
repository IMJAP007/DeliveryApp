using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading;

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
        public void TC_001_AddDelivery_ValidData_AddsToList()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            date.SelectedDate = DateTime.Now;
            addButton?.Click();

            Assert.AreEqual(1, list?.Items.Count());
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Андрей - Бармалеева улица - Новый")));
        }

        [TestMethod]
        public void TC_002_AddDelivery_EmptyCustomerName_ShowsError()
        {
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            address?.Enter("Бармалеева улица");
            date.SelectedDate = DateTime.Now;
            addButton?.Click();

            //var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();
            var error_text = window.FindFirstByXPath("/Window/Text")?.AsLabel();
            var ok_button = window.FindFirstByXPath("/Window/Button")?.AsButton();

            Assert.IsTrue(string.Equals(error_text?.Text, "Введите имя клиента!"));

            ok_button?.Click();

            Assert.AreEqual(0, list?.Items.Count());
        }

        [TestMethod]
        public void TC_003_AddDelivery_EmptyAddress_ShowsError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var date = window.FindFirstByXPath("/Pane")?.AsDateTimePicker();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            date.SelectedDate = DateTime.Now;
            addButton?.Click();

            //var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();
            var error_text = window.FindFirstByXPath("/Window/Text")?.AsLabel();
            var ok_button = window.FindFirstByXPath("/Window/Button")?.AsButton();

            Assert.IsTrue(string.Equals(error_text?.Text, "Введите адрес доставки!"));

            ok_button?.Click();

            Assert.AreEqual(0, list?.Items.Count());
        }

        [TestMethod]
        public void TC_004_AddDelivery_EmptyDate_NoError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            var error_window = window.FindFirstByXPath("/Window")?.AsWindow();

            Assert.IsTrue(error_window == null);
            Assert.AreEqual(1, list?.Items.Count());
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Андрей - Бармалеева улица - Новый")));
        }

        [TestMethod]
        public void TC_005_UpdateStatus_ValidSelectionInProgress_UpdatesStatus()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
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

            Assert.AreEqual(1, list?.Items.Count());
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Андрей - Бармалеева улица - В_пути")));
        }

        [TestMethod]
        public void TC_006_UpdateStatus_ValidSelectionNew_UpdatesStatus()
        {
            // Вызывает исключение
            //var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            //var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            //var status = window.FindFirstByXPath("/ComboBox[1]")?.AsComboBox();

            //var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
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

            using (StreamWriter sw = new StreamWriter("deliveries.txt"))
            {
                sw.WriteLine($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|1");
            }

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

            Assert.AreEqual(1, list?.Items.Count());
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Андрей - Бармалеева улица - Новый")));
        }

        [TestMethod]
        public void TC_007_UpdateStatus_ValidSelectionCompleted_UpdatesStatus()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
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

            Assert.AreEqual(1, list?.Items.Count());
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Андрей - Бармалеева улица - Доставлен")));
        }

        [TestMethod]
        public void TC_008_UpdateStatus_NoSelection_ShowsError()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();
            var status = window.FindFirstByXPath("/ComboBox")?.AsComboBox();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var updateButton = window.FindFirstByXPath("/Button[3]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            updateButton?.Click();

            //var errow_window = window.FindFirstByXPath("/Window")?.AsWindow();
            var error_text = window.FindFirstByXPath("/Window/Text")?.AsLabel();
            var ok_button = window.FindFirstByXPath("/Window/Button")?.AsButton();

            Assert.IsTrue(string.Equals(error_text?.Text, "Выберите доставку для обновления статуса!"));

            ok_button?.Click();
        }

        [TestMethod]
        public void TC_009_RemoveDelivery_ValidSelection_RemovesFromList()
        {
            var customerName = window.FindFirstByXPath("/Edit[1]")?.AsTextBox();
            var address = window.FindFirstByXPath("/Edit[2]")?.AsTextBox();

            var addButton = window.FindFirstByXPath("/Button[1]")?.AsButton();
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            customerName?.Enter("Андрей");
            address?.Enter("Бармалеева улица");
            addButton?.Click();

            customerName?.Enter("Николай");
            address?.Enter("улица Рубинштейна");
            addButton?.Click();

            list?.Select(0);

            deleteButton?.Click();

            Assert.AreEqual(1, list?.Items.Count());
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Николай - улица Рубинштейна - Новый")));
        }

        [TestMethod]
        public void TC_010_RemoveDelivery_NoSelection_ShowsError()
        {
            var deleteButton = window.FindFirstByXPath("/Button[2]")?.AsButton();

            deleteButton?.Click();

            var error_text = window.FindFirstByXPath("/Window/Text")?.AsLabel();
            var ok_button = window.FindFirstByXPath("/Window/Button")?.AsButton();
            
            Assert.IsTrue(string.Equals(error_text?.Text, "Выберите доставку для удаления!"));

            ok_button?.Click();
        }

        [TestMethod]
        public void TC_011_CheckFileLineFormat_Adding()
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
            Assert.AreEqual(1, lines.Count());
            Assert.IsTrue(lines.Contains($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|0"));
        }

        [TestMethod]
        public void TC_012_CheckFileLineFormat_EditingStatus()
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
            Assert.AreEqual(1, lines.Count());
            Assert.IsTrue(lines.Contains($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|1"));
        }

        [TestMethod]
        public void TC_013_CheckFileLineFormat_Deleting()
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
            Assert.AreEqual(0, lines.Count());
        }

        [TestMethod]
        public void TC_014_LoadDeliveriesTest()
        {
            using (StreamWriter sw = new StreamWriter("deliveries.txt"))
            {
                sw.WriteLine($"Андрей|Бармалеева улица|{DateTime.Now.ToString("yyyy-MM-dd")}|0");
                sw.WriteLine($"Николай|улица Шишкина|{DateTime.Now.ToString("yyyy-MM-dd")}|1");
            }

            app.Kill();
            app = Application.Launch("DeliveryApp.exe");
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);

            var list = window.FindFirstByXPath("/List")?.AsListBox();

            Assert.AreEqual(2, list?.Items.Count());
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Андрей - Бармалеева улица - Новый")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Николай - улица Шишкина - В_пути")));
        }

        private void SetupDataForFiltering()
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
        public void TC_015_Filtering_New()
        {
            SetupDataForFiltering();

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
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Андрей - Бармалеева улица - Новый")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Влад - Осиновая роща - Новый")));
        }

        [TestMethod]
        public void TC_016_Filtering_InProgress()
        {
            SetupDataForFiltering();

            app.Kill();
            app = Application.Launch("DeliveryApp.exe");
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);

            var list = window.FindFirstByXPath("/List")?.AsListBox();
            var select_status = window.FindFirstByXPath("/ComboBox[2]")?.AsComboBox();
            var filter = window.FindFirstByXPath("/Button[4]")?.AsButton();
            var cancel = window.FindFirstByXPath("/Button[5]")?.AsButton();

            select_status?.Expand();
            select_status?.Select(1);
            select_status?.Collapse();
            filter?.Click();

            Assert.IsTrue(list?.Items.Count() == 2);
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Артем - ВДНХ - В_пути")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Михаил - Ребро - В_пути")));
        }

        [TestMethod]
        public void TC_017_Filtering_Completed()
        {
            SetupDataForFiltering();

            app.Kill();
            app = Application.Launch("DeliveryApp.exe");
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);

            var list = window.FindFirstByXPath("/List")?.AsListBox();
            var select_status = window.FindFirstByXPath("/ComboBox[2]")?.AsComboBox();
            var filter = window.FindFirstByXPath("/Button[4]")?.AsButton();
            var cancel = window.FindFirstByXPath("/Button[5]")?.AsButton();

            select_status?.Expand();
            select_status?.Select(2);
            select_status?.Collapse();
            filter?.Click();

            Assert.IsTrue(list?.Items.Count() == 2);
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Николай - улица Шишкина - Доставлен")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Виктор - Площадь мост - Доставлен")));
        }

        [TestMethod]
        public void TC_018_Filtering_CancelFiltering()
        {
            SetupDataForFiltering();

            app.Kill();
            app = Application.Launch("DeliveryApp.exe");
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);

            var list = window.FindFirstByXPath("/List")?.AsListBox();
            var select_status = window.FindFirstByXPath("/ComboBox[2]")?.AsComboBox();
            var filter = window.FindFirstByXPath("/Button[4]")?.AsButton();
            var cancel = window.FindFirstByXPath("/Button[5]")?.AsButton();

            select_status?.Expand();
            select_status?.Select(2);
            select_status?.Collapse();
            filter?.Click();

            Assert.IsTrue(list?.Items.Count() == 2);
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Николай - улица Шишкина - Доставлен")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Виктор - Площадь мост - Доставлен")));

            cancel?.Click();
            Assert.IsTrue(list?.Items.Count() == 6);
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Андрей - Бармалеева улица - Новый")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Влад - Осиновая роща - Новый")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Артем - ВДНХ - В_пути")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Михаил - Ребро - В_пути")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Николай - улица Шишкина - Доставлен")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Виктор - Площадь мост - Доставлен")));
        }

        [TestMethod]
        public void TC_019_Filtering_Delete()
        {
            SetupDataForFiltering();

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
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Андрей - Бармалеева улица - Новый")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Влад - Осиновая роща - Новый")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Михаил - Ребро - В_пути")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Николай - улица Шишкина - Доставлен")));
            Assert.IsTrue(list?.Items.Any(item => string.Equals(item.Text, "Виктор - Площадь мост - Доставлен")));
        }

        [TestMethod]
        public void TC_020_Filtering_Update()
        {
            SetupDataForFiltering();

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
