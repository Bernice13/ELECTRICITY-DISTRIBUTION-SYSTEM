using System;

namespace AgentPortal.AppData
{
    public class AgentApplicationData
    {
        public DateTime LastLoginDate { get; set; }
        public static string CurrentAgentId { get; set; }
    }
}