using CourseProject.Domain;

namespace CourseProject.BLL.DTO.StatisticsDtos; 

public class TopManagersWhoHandleMoreOrdersSettingsDto {
    public int Top { get; set; }

    public DateRangeSettings DateRangeSettings { get; set; }
}