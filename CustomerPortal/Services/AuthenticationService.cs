using System;
using CustomerPortal.AppData;
using PortalLibrary.CustomerServices;
using PortalLibrary.Models;

namespace CustomerPortal.Services
{
    public class AuthenticationService
    {
        private static CustomerService service = new CustomerService();
         public static string RegisterUser(Customer model)
        {
            if(model.FirstName == "")
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                string id = service.RegisterCustomer(model);
                CustomerApplicationData.CurrentCustomerId = id;
                return id == null ? "Failed" : "Success";
            }
        }

        public static Customer LoginUser(string email)
        {
            var customerfound = service.GetCustomerByEmail(email);
            if (customerfound == null) 
            { Console.WriteLine("Please enter the correct password and try again");
                return null;
                
                }
            else { 
            CustomerApplicationData.CurrentCustomerId = customerfound.Id;
            return customerfound;
                }
        }

        public static Customer GetCustomerInformation(string email)
        {
            var customerInformation = service.GetCustomerByEmail(email);
            return customerInformation;
        }


    }
}