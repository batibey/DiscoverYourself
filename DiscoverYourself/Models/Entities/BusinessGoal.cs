using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiscoverYourself.Models.Enums;

namespace DiscoverYourself.Models.Entities;

public class BusinessGoal
{
    [Key]
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TasksCompleted { get; set; }
    public enmTaskTypes TaskType { get; set; }
    public double DailyWorkHours { get; set; }
    public double DailySelfDevelopmentHours { get; set; }
    public string? AdditionalInfo { get; set; }
    
    // Foreign key
    [Required]
    public int UserId { get; set; }

    // Navigation property
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}
