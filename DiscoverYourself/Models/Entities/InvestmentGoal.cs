using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscoverYourself.Models.Entities;

public class InvestmentGoal
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; } // Kullanıcının hedeflerini belirttiği tarih

    [Required]
    public decimal TargetCurrency { get; set; } // Hedef döviz yatırımı

    [Required]
    public decimal ActualCurrency { get; set; } // Gerçekleşen döviz yatırımı

    [Required]
    public decimal TargetGold { get; set; } // Hedef altın yatırımı

    [Required]
    public decimal ActualGold { get; set; } // Gerçekleşen altın yatırımı

    [Required]
    public decimal TargetSilver { get; set; } // Hedef gümüş yatırımı

    [Required]
    public decimal ActualSilver { get; set; } // Gerçekleşen gümüş yatırımı

    // Foreign key
    [Required]
    public int UserId { get; set; }

    // Navigation property
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}
