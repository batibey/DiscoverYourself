using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscoverYourself.Models.Entities;

public class InvestmentGoal
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; } 

    [Required]
    public decimal TargetCurrency { get; set; } 

    [Required]
    public decimal ActualCurrency { get; set; } 

    [Required]
    public decimal TargetGold { get; set; } 

    [Required]
    public decimal ActualGold { get; set; } 

    [Required]
    public decimal TargetSilver { get; set; } 

    [Required]
    public decimal ActualSilver { get; set; }

    // Foreign key
    [Required]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}
