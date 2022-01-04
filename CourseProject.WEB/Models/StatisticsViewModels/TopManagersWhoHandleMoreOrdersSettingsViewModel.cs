using System.ComponentModel.DataAnnotations;
using CourseProject.Domain;

namespace CourseProject.WEB.Models.StatisticsViewModels; 

public class TopManagersWhoHandleMoreOrdersSettingsViewModel {
    
    [Range(minimum: 3, maximum: int.MaxValue)]
    public int Top { get; set; }

    [Display(Name = "Date range settings")]
    public DateRangeSettings DateRangeSettings { get; set; }
}