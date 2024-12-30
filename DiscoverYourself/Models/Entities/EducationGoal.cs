using DiscoverYourself.Models.Enums;

namespace DiscoverYourself.Models.Entities;

public class EducationGoal
{
    public int Id { get; set; }
    public string Title { get; set; } // Hedefin başlığı
    public string Description { get; set; } // Hedefin açıklaması
    public DateTime StartDate { get; set; } // Hedefin başlangıç tarihi
    public DateTime EndDate { get; set; } // Hedefin bitiş tarihi
    public List<string> Topics { get; set; } // Hedefte ele alınacak konular (Örn: Liderlik, İletişim)
    public enmDifficultyLevel Difficulty { get; set; } // Hedefin zorluk seviyesi (Enum olarak tanımlanabilir)
    public string TargetAudience { get; set; } // Hedeflenen kitle (Örn: Yeni mezunlar, yöneticiler)
    public List<string> Milestones { get; set; } // Hedefe ulaşmak için belirlenen ara adımlar
    public string SuccessCriteria { get; set; } // Hedefin başarı ölçütü (Örn: Bir sertifika almak)
    public bool IsCompleted { get; set; } // Hedef tamamlandı mı?
}