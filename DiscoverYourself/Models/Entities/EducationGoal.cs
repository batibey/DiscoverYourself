using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiscoverYourself.Models.Enums;

namespace DiscoverYourself.Models.Entities;

public class EducationGoal
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } // Hedefin başlığı
    public string Description { get; set; } // Hedefin açıklaması
    public DateTime StartDate { get; set; } // Hedefin başlangıç tarihi
    public DateTime EndDate { get; set; } // Hedefin bitiş tarihi
    public enmTopics Topics { get; set; } // Hedefte ele alınacak konular (Örn: Liderlik, İletişim)
    public enmDifficultyLevel Difficulty { get; set; } // Hedefin zorluk seviyesi (Enum olarak tanımlanabilir)
    public string TargetAudience { get; set; } // Hedeflenen kitle (Örn: Yeni mezunlar, yöneticiler)
    public string Milestones { get; set; } // Hedefe ulaşmak için belirlenen ara adımlar
    public string SuccessCriteria { get; set; } // Hedefin başarı ölçütü (Örn: Bir sertifika almak)
    public bool IsCompleted { get; set; } // Hedef tamamlandı mı?
    
    // Foreign key
    [Required]
    public int UserId { get; set; }

    // Navigation property
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}