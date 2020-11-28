using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services {
    public class AuthSenderOptions {
        private readonly string user = "Food Delivery"; // The name you want to show up on your email
         // Make sure the string passed in below matches your API Key
        private readonly string key = Environment.GetEnvironmentVariable("SENDGRID");
        public string SendGridUser { get { return user; } }
        public string SendGridKey { get { return key; } }
    }
}

