using System.Collections.Generic;
using PortalLibrary.Models;

namespace PortalLibrary.Data
{
    public class DbContext
    {
        public List<Agent> Agents { get; set; } = new List<Agent>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<CustomerSubscription> Subcriptions { get; set; } = new List<CustomerSubscription>();
        public List<Meter> Meters { get; set; } = new List<Meter>();
        public List<Tarrif> Tariffs { get; set; } = new List<Tarrif>();

    }
}