using System.ComponentModel.DataAnnotations;

namespace DiscoverYourself.Models.RequestModels;

public class AccumulationGoalInputModel
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Hedef döviz değeri pozitif olmalıdır.")]
    public decimal TargetCurrency { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Gerçekleşen döviz değeri pozitif olmalıdır.")]
    public decimal ActualCurrency { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Hedef altın değeri pozitif olmalıdır.")]
    public decimal TargetGold { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Gerçekleşen altın değeri pozitif olmalıdır.")]
    public decimal ActualGold { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Hedef gümüş değeri pozitif olmalıdır.")]
    public decimal TargetSilver { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Gerçekleşen gümüş değeri pozitif olmalıdır.")]
    public decimal ActualSilver { get; set; }
}
