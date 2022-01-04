using CourseProject.BLL.FilterModels;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class ShowroomFilterViewModel : FilterViewModel {

    public ShowroomOrderType OrderType { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }
}