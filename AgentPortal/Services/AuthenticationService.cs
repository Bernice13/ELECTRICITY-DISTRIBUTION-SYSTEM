using System;
using AgentPortal.AppData;
using PortalLibrary.AgentServices;
using PortalLibrary.Models;

namespace AgentPortal.Services
{
    public class AuthenticationService
    {
        private static AgentService service = new AgentService();
         public static string RegisterUser(Agent model)
        {
            if(model.FirstName == "")
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                string id = service.RegisterAgent(model);
                AgentApplicationData.CurrentAgentId = id;
                return id == null ? "Failed" : "Success";
            }
        }

        public static Agent LoginUser(string email)
        {
            var agentFound = service.GetAgentByEmail(email);
            AgentApplicationData.CurrentAgentId = agentFound.Id;
            return agentFound;
        }

        public static Agent GetAgentInformation(string email)
        {
            var agentInformation = service.GetAgentByEmail(email);
            return agentInformation;
        }
    }
}