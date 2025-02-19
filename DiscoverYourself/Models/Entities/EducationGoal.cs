using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiscoverYourself.Models.Enums;

namespace DiscoverYourself.Models.Entities;

public class EducationGoal
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public enmTopics Topics { get; set; } 
    public enmDifficultyLevel Difficulty { get; set; } 
    public string TargetAudience { get; set; } 
    public string Milestones { get; set; } 
    public string SuccessCriteria { get; set; } 
    public bool IsCompleted { get; set; } 
    
    [Required]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}