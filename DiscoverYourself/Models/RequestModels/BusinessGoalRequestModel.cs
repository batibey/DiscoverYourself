using System.ComponentModel.DataAnnotations;
using DiscoverYourself.Models.Enums;

namespace DiscoverYourself.Models.RequestModels;

public class BusinessGoalRequestModel
{
    [Required]
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TasksCompleted { get; set; }
    public enmTaskTypes TaskType { get; set; }
    public double DailyWorkHours { get; set; }
    public double DailySelfDevelopmentHours { get; set; }
    public string? AdditionalInfo { get; set; }
}
