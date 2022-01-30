using System;
using Microsoft.AspNetCore.Identity;

namespace SpotOps.Api.Models;

public class ApplicationUser : IdentityUser
{
    public string? Password { get; set; }
    public string? PasswordSalt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public DateTime DateAdded { get; set; }
    public bool Active { get; set; }
    public bool Blocked { get; set; }
}