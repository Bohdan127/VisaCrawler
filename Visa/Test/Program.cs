using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using ToolsPortable;
using Visa.WinForms.Managers;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var stateManager = new StateManager();
            stateManager.GetCurrentSiteAvailability();
        }
    }
}