using System;
using System.Collections.Generic;
using PortalLibrary.Models;

namespace PortalLibrary.AgentServices
{
    public class AgentService : AgentFileService
    {
        public string RegisterAgent(Agent agent)
        {
            // if(customer == null)
            // {
            //     throw new ArgumentNullException(nameof(customer));
            // }
            // //This ill Handle registration of a customer
            // else
            // {
                fileService.database.Agents.Add(agent);
                fileService.SaveChanges();
                return agent.Id;
           // }
        }

        public Agent GetAgentById(string agentId)
        {
            Agent foundAgent = fileService.database.Agents.Find(c => c.Id == agentId);
            if (foundAgent != null)
            {
                return foundAgent;
            }
            return null;
        }

        //Find Customer via Email
        public Agent GetAgentByEmail(string email)
        {
            Agent foundAgent = fileService.database.Agents.Find(c => c.EmailAddress == email);
            if (foundAgent != null)
            {
                return foundAgent;
            }
            return null;
        }

        public string UpdateAgent(Agent modifiedAgent)
        {
            Agent customer = this.GetAgentById(modifiedAgent.Id);
            if(customer != null)
            {
                //int indexOfCustomer = fileService.database.Customers.IndexOf(customer);
                //fileService.database.Customers.Insert(indexOfCustomer, modifiedCustomer);
                fileService.SaveChanges();
                return "SUCCESSFULLY UPDATED";
            }
            return "Failed, Customer not found";
        }

        public string SubscribeToTariff(CustomerSubscription customerSubscription)
        {
            if(customerSubscription == null)
            {
                throw new ArgumentNullException(nameof(customerSubscription));
            }
            
            else
            {
                customerSubscription.Id = "SUB-" + Guid.NewGuid().ToString();
                fileService.database.Subcriptions.Add(customerSubscription);
                fileService.SaveChanges();
                return customerSubscription.Id;
            }
        }

        public CustomerSubscription GetSubscriptionByCustomerID(string customerId)
        {
            CustomerSubscription foundSubscription = fileService.database.Subcriptions.Find(c => c.CustomerId == customerId);
            if (foundSubscription != null)
            {
                return foundSubscription;
            }
            return null;
        }

        public bool RemoveSubscription(CustomerSubscription subscription)
        {
            bool isRemoved = fileService.database.Subcriptions.Remove(subscription);
            fileService.SaveChanges();
            return isRemoved;
        }

        public List<Tarrif> GetAllTarrif()
        {
            List<Tarrif> tarrifList = new List<Tarrif>();
            tarrifList = fileService.database.Tariffs;
            return tarrifList;
        }
    }
}