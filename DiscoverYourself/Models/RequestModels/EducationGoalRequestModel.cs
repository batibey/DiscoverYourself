using DiscoverYourself.Models.Enums;

namespace DiscoverYourself.Models.RequestModels;

public class EducationGoalRequestModel
{
    public string Title { get; set; } // Hedefin başlığı
    public string Description { get; set; } // Hedefin açıklaması
    public DateTime StartDate { get; set; } // Başlangıç tarihi
    public DateTime EndDate { get; set; } // Bitiş tarihi
    public List<string> Topics { get; set; } // Eğitimde işlenecek konular
    public enmDifficultyLevel Difficulty { get; set; } // Zorluk seviyesi
    public string TargetAudience { get; set; } // Hedeflenen kitle
    public List<string> Milestones { get; set; } // Ara adımlar
    public string SuccessCriteria { get; set; } // Başarı ölçütü
}