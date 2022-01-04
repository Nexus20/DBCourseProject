using CourseProject.Domain;

namespace CourseProject.DAL.StatisticsModels;

public class TopManagersWhoHandleMoreOrdersSettings {
    public int Top { get; set; }

    public DateRangeSettings DateRangeSettings { get; set; }
}