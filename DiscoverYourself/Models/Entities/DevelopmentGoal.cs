using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiscoverYourself.Models.Enums;

namespace DiscoverYourself.Models.Entities;

public class DevelopmentGoal
{
    [Key]
    public int Id { get; set; }
        
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
        
    public DateTime CreatedDate { get; set; } = DateTime.Now;
        
    public DateTime? UpdatedDate { get; set; }
    // Foreign key
    [Required]
    public int UserId { get; set; }

    // Navigation property
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}