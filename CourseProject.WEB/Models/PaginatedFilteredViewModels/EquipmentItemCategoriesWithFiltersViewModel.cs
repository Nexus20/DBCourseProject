using CourseProject.BLL.FilterModels;
using CourseProject.WEB.Models.FilterViewModels;

namespace CourseProject.WEB.Models.PaginatedFilteredViewModels; 

public class EquipmentItemCategoriesWithFiltersViewModel {

    public List<EquipmentItemCategoryViewModel> EquipmentItemCategories { get; set; }

    public EquipmentItemCategoryFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public EquipmentItemCategoryOrderType SelectedOrderType { get; set; }

    public string? SelectedName { get; set; }

    public string? SelectedUnitsOfMeasure { get; set; }
}