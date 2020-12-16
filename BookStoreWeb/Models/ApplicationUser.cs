using System;
using Microsoft.AspNetCore.Identity;

namespace BookStoreWeb.Models
{
    /// <summary>
    /// Add custom column to IdentityUser Table
    /// </summary>
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}