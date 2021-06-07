using System;
using System.Collections.Generic;
using System.Threading;
using CustomerPortal.Services;
using PortalLibrary.Models;
using CustomerPortal.AppData;
using System.Text;

namespace CustomerPortal.Menu
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

            //Check if there is an existing customer with the email provided

            var customerCheck = AuthenticationService.GetCustomerInformation(navItemDic["Email"]);
            if (customerCheck == null)
            {

                Customer model = new Customer
                {
                    FirstName = navItemDic["FirstName"],
                    LastName = navItemDic["LastName"],
                    EmailAddress = navItemDic["Email"],
                    Password = navItemDic["Password"],
                    PhoneNumber = navItemDic["PhoneNumber"],
                    Id = "CUS-" + Guid.NewGuid().ToString(),
                    MeterNumber = "MN" + Guid.NewGuid().ToString(),
                };

                string registrationResponse = AuthenticationService.RegisterUser(model);
                if (registrationResponse == "Success")
                {
                    Console.Clear();
                    Console.WriteLine("Registration Successful\n");
                    Console.WriteLine($"Registered Details: \nCustomer ID : {model.Id} \nName : {model.FirstName} {model.LastName} \nPhone Number : {model.PhoneNumber} \nEmail : {model.EmailAddress} \nMeter Number : {model.MeterNumber} ");
                    Console.WriteLine("Press any key to go to dashboard....");
                    Console.ReadKey();
                    Console.Clear();
                    inRegisterPage = false;
                    inCustomerDashboard = true;
                }
            }

            else if(customerCheck != null){
                
                Console.WriteLine("Email already exist. Please Sign-In");
                Thread.Sleep(3000);
                inRegisterPage = false;
            }
            else{
                Console.WriteLine("An Error occured While Trying to Create your Account Please try Again");
                inRegisterPage = false;
            }
            
        }

        public static void CustomerDashboard()
        {
            Console.WriteLine("Welcome! What would you like to do?");
            Console.WriteLine("1. Subscribe \n2. Update personal information \n3. Unsubscribe \n4. Sign Out\n");
            var response = Console.ReadLine();

            switch (response)
            {
                case "1":
                    Console.Clear();
                    LoadSubscriptiionForm();
                    inCustomerDashboard = true; 
                break;

                case "2":
                    Console.Clear();
                    UpdateCustomerDetailForm();
                    inCustomerDashboard = true;
                break;
                case "3":
                    Console.Clear();
                    UnsubscribeCustomer();
                    inCustomerDashboard = true;
                break;

                case "4":
                    inCustomerDashboard = false;
                    CustomerApplicationData.CurrentCustomerId = "";  //uncomment after you confirm it returns to login page
                    break;            
            }
        }

        private static void UpdateCustomerDetailForm()
        {
            //still need to refactor
            ManageUserService.UpdateCustomerDetails(CustomerApplicationData.CurrentCustomerId);
        }

        private static void UnsubscribeCustomer()
        {
            var result = ManageUserService.Unsubscribe(CustomerApplicationData.CurrentCustomerId);
            Console.WriteLine($"{result} \nPress any key to go back to dashboard...");
            Console.ReadKey();
        }

        private static void LoginForm()
        {
            //Dictionary<string, string> navItemDic = new Dictionary<string, string>();
            //List<string> navigationItem = new List<string>
            //{
             //   "Email", "Password"
           // };
            
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Please Login with your Email and Password");

               // for (var i = 0; i < navigationItem.Count; i++)
               // {
               //     Console.WriteLine($"Please Enter your {navigationItem[i]} :");
                //    var value = Console.ReadLine();
                 //   navItemDic.Add(navigationItem[i], value);
               // }

                string Email="";
                string  Password="";
                Console.WriteLine("Please enter your email: ");
                Email= Console.ReadLine();
                Console.WriteLine("Please enter your password: ");
                //Password=Console.ReadLine();
                 Password = GetHiddenConsoleInput();
                
            

                //Email = navItemDic["Email"];
                //Password = navItemDic["Password"];

                var customer = AuthenticationService.LoginUser(Email);
                if (customer == null)
                {
                    Console.WriteLine("Invalid Login email, Please Try Again");
                    Thread.Sleep(3000);
                }
                else
                {
                    if (customer.Password != Password)
                    {
                        Console.WriteLine("Invalid Login credentials, Please Try Again");
                        
                    }
                    else
                    {
                        Console.WriteLine($"Welcome {customer.FirstName} {customer.LastName}");
                        
                        break;
                    }
                }
            }
            //inLoginPage = false;
            Console.Clear();
            inLoginPage = false;
            inCustomerDashboard = true;
        }


        private static void LoadSubscriptiionForm()
        {
            List<Tarrif> tarrifs = new List<Tarrif>();
            tarrifs = ManageUserService.GetTarrifData();
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

            if (tarrifId != "")
            {
                CustomerSubscription subscription = new CustomerSubscription
                {
                    TariffId = tarrifId,
                    CustomerId = CustomerApplicationData.CurrentCustomerId,
                    AgentId = "Customer subscribed"
                };

                var result = ManageUserService.AddSubscription(subscription, CustomerApplicationData.CurrentCustomerId);
                Console.Clear();
                Console.WriteLine($"{result}. Press any key to return to dashboard");
                Console.ReadKey();
            }
            else{
                Console.WriteLine("An error occured, please try again.");
                inCustomerDashboard = true;
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
         public static string GetHiddenConsoleInput()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            return input.ToString();
        }
    }
}