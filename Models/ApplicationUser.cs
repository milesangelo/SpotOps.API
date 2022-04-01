using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace SpotOps.Api.Models;

public class ApplicationUser : IdentityUser
{
    [JsonIgnore] public string? Password { get; set; }

    public string? PasswordSalt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public DateTime DateAdded { get; set; }
    public int Active { get; set; }
    public bool Blocked { get; set; }
}