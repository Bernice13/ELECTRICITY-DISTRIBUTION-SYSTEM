using System;

namespace CustomerPortal.AppData
{
    public class CustomerApplicationData
    {
        public DateTime LastLoginDate { get; set; }
        public static string CurrentCustomerId { get; set; }
    }
}