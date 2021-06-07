using System;
using System.Collections.Generic;
using System.Threading;
using AgentPortal.AppData;
using AgentPortal.Services;
using PortalLibrary.Models;

namespace AgentPortal.Menu
{
    public class Forms : NavigationMenu
    {
        public static void RegistrationPage()
        {
            Console.Clear();
            Console.WriteLine("Please Provide your Details");
            RegistrationForm();
        }

        public static void LoginPage()
        {
            LoginForm();
        }
        private static void RegistrationForm()
        {
            Dictionary<string, string> navItemDic = new Dictionary<string, string>();
            List<string> navigationItems = new List<string>
            {
                "FirstName", "LastName", "PhoneNumber", "Email", "Password"
            };

            for (int i = 0; i < navigationItems.Count; i++)
            {
                Console.WriteLine($"Enter your {navigationItems[i]} : ");
                var value = Console.ReadLine();
                var validatedValue = ValidateUserInput(value);
                navItemDic.Add(navigationItems[i], validatedValue);
            }

            //Check if there is an existing agent with the email provided

            var agentCheck = AuthenticationService.GetAgentInformation(navItemDic["Email"]);
            if (agentCheck == null)
            {

                Agent model = new Agent
                {
                    FirstName = navItemDic["FirstName"],
                    LastName = navItemDic["LastName"],
                    EmailAddress = navItemDic["Email"],
                    Password = navItemDic["Password"],
                    PhoneNumber = navItemDic["PhoneNumber"],
                    Id = "AGT-" + Guid.NewGuid().ToString()
                };

                string registrationResponse = AuthenticationService.RegisterUser(model);
                if (registrationResponse == "Success")
                {
                    Console.Clear();
                    Console.WriteLine("Registration Successful");
                    Console.WriteLine($"Registered Details: \nAgent ID : {model.Id} \nName : {model.FirstName} {model.LastName} \nPhone Number : {model.PhoneNumber} \nEmail : {model.EmailAddress} ");
                    Console.WriteLine("Press any key to go to dashboard....");
                    Console.ReadKey();
                    inRegisterPage = false;
                    inAgentDashboard = true;
                }
            }

            else if(agentCheck != null){
                
                Console.WriteLine("Email already exist. Please Sign-In");
                Thread.Sleep(3000);
                inRegisterPage = false;
            }
            else{
                Console.WriteLine("An Error occured While Trying to Create your Account Please try Again");
                inRegisterPage = false;
            }
            
        }

        public static void AgentDashboard()
        {
            //Add option for agent self-service and customer service
            
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Agent Self-Service \n2. Customer Service   \n3. Sign Out");
            var response = Console.ReadLine();

            switch (response)
            {
                case "1":
                    Console.Clear();
                    //not implemented
                    inAgentDashboard = true; 
                break;

                case "2":
                    Console.Clear();
                    CustomerServicePage();
                    inAgentDashboard = true;
                break;
                
                case "3":
                    inAgentDashboard = false;
                    AgentApplicationData.CurrentAgentId = "";
                    break;            
            }
        }

        private static void UpdateCustomerDetailForm()
        {
            //still need to refactor
            ManageCustomerService.UpdateCustomerDetails(AgentApplicationData.CurrentAgentId);
        }

        private static void UnsubscribeCustomer()
        {
            var result = ManageCustomerService.Unsubscribe(AgentApplicationData.CurrentAgentId);
            Console.WriteLine($"{result} \nPress any key to go back to dashboard...");
            Console.ReadKey();
        }

        private static void LoginForm()
        {
            Dictionary<string, string> navItemDic = new Dictionary<string, string>();
            List<string> navigationItem = new List<string>
            {
                "Email", "Password"
            };
            
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Please Login with your Email and Password");

                for (var i = 0; i < navigationItem.Count; i++)
                {
                    Console.WriteLine($"Please Enter your {navigationItem[i]} :");
                    var value = Console.ReadLine();
                    navItemDic.Add(navigationItem[i], value);
                }

                string Email, Password;

                Email = navItemDic["Email"];
                Password = navItemDic["Password"];

                var agent = AuthenticationService.LoginUser(Email);
                if (agent == null)
                {
                    Console.WriteLine("Invalid Login Credentials Please Try Again");
                    Thread.Sleep(3000);
                }
                else
                {
                    if (agent.Password != Password)
                    {
                        Console.WriteLine("Invalid Login Credentials Please Try Again");
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        Console.WriteLine($"Welcome {agent.FirstName} {agent.LastName}");
                        Thread.Sleep(3000);
                        break;
                    }
                }
            }
            //inLoginPage = false;
            inLoginPage = false;
            inAgentDashboard = true;
        }


        private static void LoadSubscriptionForm()
        {
            List<Tarrif> tarrifs = new List<Tarrif>();
            tarrifs = ManageCustomerService.GetTarrifData();
            Dictionary<string, string> itemDic = new Dictionary<string, string>();
            List<string> tarrifName = new List<string>();
            Console.WriteLine("Select subscription");
            foreach (var tarrif in tarrifs)
            {
                Console.WriteLine($"{tarrif.Id}. {tarrif.Name} at {tarrif.PricePerUnit} kobo per unit");
                itemDic.Add(tarrif.Name,tarrif.Id);
                tarrifName.Add(tarrif.Name);
            }
            var response = Console.ReadLine();
            string tarrifId = "";

            for (int i = 0; i < itemDic.Count; i++)
            {
                if (response == itemDic[tarrifName[i]])
                {
                    Console.WriteLine($"You have selected {tarrifName[i]}");
                    tarrifId = itemDic[tarrifName[i]];
                }
            }

            Console.WriteLine("Enter Customer ID : ");
            var customerId = Console.ReadLine();
            var customerExist = ManageCustomerService.FetchCustomerById(customerId);

            if (tarrifId != "" && customerExist != null)
            {
                CustomerSubscription subscription = new CustomerSubscription
                {
                    TariffId = tarrifId,
                    CustomerId = customerExist.Id,
                    AgentId = AgentApplicationData.CurrentAgentId,
                };

                var result = ManageCustomerService.AddSubscription(subscription, customerExist.Id);
                Console.Clear();
                Console.WriteLine($"{result}. Press any key to return to dashboard");
                Console.ReadKey();
            }
            else{
                Console.WriteLine("An error occured, please try again.");
                //inAgentDashboard = true;
            }
        }

        private static string ValidateUserInput(string value)
        {
            while (value == "")
            {
                Console.WriteLine("The required field cannot be empty");
                value = Console.ReadLine();
            }

            return value;
        }

        public static void CustomerServicePage()
        {
            Console.WriteLine("Customer Service");
            Console.WriteLine("1. Subscribe Customer \n2. Update Customer Information \n3. Unsubscribe Customer \n4. Delete Customer \n5. Back to Dashboard");
            var response = Console.ReadLine();

            switch (response)
            {
                case "1":
                    Console.Clear();
                    LoadSubscriptionForm();
                    //inAgentDashboard = true;
                    break;

                case "2":
                    Console.Clear();
                    UpdateCustomerDetailForm();
                    //inAgentDashboard = true;
                    break;
                case "3":
                    Console.Clear();
                    UnsubscribeCustomer();
                    //inAgentDashboard = true;
                    break;

                case "5":
                    inAgentDashboard = true;
                    AgentApplicationData.CurrentAgentId = "";  //uncomment after you confirm it returns to login page
                    break;
            }
        }
    }
}




// Console.WriteLine("What would you like to do?");
//             Console.WriteLine("1. Subscribe \n2. Update personal information \n3. Unsubscribe \n4. Sign Out");
//             var response = Console.ReadLine();

//             switch (response)
//             {
//                 case "1":
//                     Console.Clear();
//                     LoadSubscriptiionForm();
//                     inAgentDashboard = true; 
//                 break;

//                 case "2":
//                     Console.Clear();
//                     UpdateCustomerDetailForm();
//                     inAgentDashboard = true;
//                 break;
//                 case "3":
//                     Console.Clear();
//                     UnsubscribeCustomer();
//                     inAgentDashboard = true;
//                 break;

//                 case "4":
//                     inAgentDashboard = false;
//                     AgentApplicationData.CurrentAgentId = "";  //uncomment after you confirm it returns to login page
//                     break;            
//             }