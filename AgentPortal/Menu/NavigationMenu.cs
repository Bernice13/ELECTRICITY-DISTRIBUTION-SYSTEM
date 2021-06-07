using System;

namespace AgentPortal.Menu
{
    public class NavigationMenu
    {
        private static bool appIsRunning = true;
        protected static bool inRegisterPage = false;
        protected static bool inLoginPage = false;
        protected static bool inAgentDashboard = false;
        public static void HomePageMenu()
        {
            
            while (appIsRunning)
            {
                Console.Clear();
                Console.WriteLine("Welcome To EDS AGENT PORTAL.\n");
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
                while (inAgentDashboard)
                {
                    Forms.AgentDashboard();
                }
            }

            Environment.Exit(0);
        }
    }
}