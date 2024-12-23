using System.ComponentModel.DataAnnotations;

namespace DiscoverYourself.Models.Entities;

public class User
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}