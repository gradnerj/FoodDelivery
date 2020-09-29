using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Utility {
    public class AuthSenderOptions {
        private string user = "Food Delivery"; // The name you want to show up on your email
         // Make sure the string passed in below matches your API Key
        private string key = Environment.GetEnvironmentVariable("SENDGRID");
        public string SendGridUser { get { return user; } }
        public string SendGridKey { get { return key; } }
    }
}

