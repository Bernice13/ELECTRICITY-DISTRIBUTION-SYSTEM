using System;

namespace PortalLibrary.Models
{
    public class CustomerSubscription
    {
        public string Id { get; set; }
        public decimal Amount { get; set; } //remove
        public string TariffId { get; set; }
        public string CustomerId { get; set; }
        public string AgentId { get; set; }
        public DateTime SubcribedAt { get; set; } = DateTime.Now;
    }
}