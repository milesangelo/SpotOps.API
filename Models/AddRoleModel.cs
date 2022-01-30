using System.ComponentModel.DataAnnotations;

namespace SpotOps.Api.Models;

public class AddRoleModel
{
    /// <summary>
    /// Email.
    /// </summary>
    [Required]
    public string Email { get; set; }
    
    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    public string Password { get; set; }
    
    /// <summary>
    /// Role.
    /// </summary>
    [Required]
    public string Role { get; set; }
}