using System;
using System.Collections.Generic;
using System.Threading;
using CustomerPortal.Services;
using PortalLibrary.Models;

namespace CustomerPortal.Menu
{
    public class NavigationMenu
    {
        private static bool appIsRunning = true;
        protected static bool inRegisterPage = false;
        protected static bool inLoginPage = false;
        protected static bool inCustomerDashboard = false;
        public static void HomePageMenu()
        {
            
            while (appIsRunning)
            {
                Console.Clear();
                Console.WriteLine("Welcome To Ikeja EDS CUSTOMER PORTAL.\n");
                Console.WriteLine("Choose an Option : 1. Login         2. Register          3. Exit");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        inLoginPage = true;
                        break;
                    case "2":
                        inRegisterPage = true;
                        break;
                    case "3":
                        appIsRunning = false;
                    break;
                }
                while (inLoginPage)
                {
                    Forms.LoginPage();
                }
                while (inRegisterPage)
                {
                    Forms.RegistrationPage();
                }
                while (inCustomerDashboard)
                {
                    Forms.CustomerDashboard();
                }
            }

            Environment.Exit(0);
        }
    }
}