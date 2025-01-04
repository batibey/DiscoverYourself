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
    public enmTopics Topics { get; set; } // Hedefte ele alınacak konular
    public enmDifficultyLevel Difficulty { get; set; } // Hedefin zorluk seviyesi
    public string TargetAudience { get; set; } // Hedeflenen kitle
    public string Milestones { get; set; } // Hedefe ulaşmak için belirlenen ara adımlar
    public string SuccessCriteria { get; set; } // Hedefin başarı ölçütü (Örn: Bir sertifika almak)
    public bool IsCompleted { get; set; } 
    
    // Foreign key
    [Required]
    public int UserId { get; set; }

    // Navigation property
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}