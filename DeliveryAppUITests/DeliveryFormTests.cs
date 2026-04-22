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
            app = FlaUI.Core.Application.Launch(@"C:\Other\Documents\College\Testing\Лабораторные\Лр2\DeliveryApp\DeliveryApp\bin\Debug\DeliveryApp.exe");
            
            automation = new UIA3Automation();
            window = app.GetMainWindow(automation);
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
