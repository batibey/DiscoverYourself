using System.ComponentModel.DataAnnotations;
using DiscoverYourself.Models.Enums;

namespace DiscoverYourself.Models.RequestModels;

public class DevelopmentGoalRequestModel
{
    [Required]
    public int UserId { get; set; }
    public string Title { get; set; }
        
    public string Description { get; set; }
        
    public GoalCategory Category { get; set; }
        
    public DateTime StartDate { get; set; }
        
    public DateTime? EndDate { get; set; }
        
    public int Progress { get; set; }
        
    public bool IsCompleted { get; set; }
        
    public PriorityLevel PriorityLevel { get; set; }
        
    public string ResourcesNeeded { get; set; }
        
    public string Feedback { get; set; }
}