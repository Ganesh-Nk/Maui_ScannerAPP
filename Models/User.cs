using System;

namespace Maui_Shopping_APP.Models
{
    public class User
    {
        public string CustomerId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public User() { }

        public User(string customerId, string password)
        {
            CustomerId = customerId;
            Password = password;
        }

        public bool IsValid() => !string.IsNullOrWhiteSpace(CustomerId) && !string.IsNullOrWhiteSpace(Password);
    }
}
