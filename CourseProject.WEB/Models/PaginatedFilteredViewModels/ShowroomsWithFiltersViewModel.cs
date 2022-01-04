using CourseProject.BLL.FilterModels;
using CourseProject.WEB.Models.FilterViewModels;

namespace CourseProject.WEB.Models.PaginatedFilteredViewModels; 

public class ShowroomsWithFiltersViewModel {

    public List<ShowroomViewModel> Showrooms { get; set; }

    public ShowroomFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public ShowroomOrderType SelectedOrderType { get; set; }

    public string? SelectedAddress { get; set; }

    public string? SelectedPhone { get; set; }
}