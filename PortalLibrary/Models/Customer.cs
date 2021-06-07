using System;

namespace PortalLibrary.Models
{
    public class Customer : BaseUser
    {
        public string MeterNumber { get; set; }
        public Customer()
        {
            // this.Id = "CUS-" + Guid.NewGuid().ToString();
            // this.MeterNumber = "MN" + Guid.NewGuid().ToString();
        }
        
    }
}