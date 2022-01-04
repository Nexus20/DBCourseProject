using System.ComponentModel.DataAnnotations;
using CourseProject.BLL.FilterModels;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class EquipmentItemCategoryFilterViewModel : FilterViewModel {
    public string? Name { get; set; }

    [Display(Name = "Units of measure")]
    public string? UnitsOfMeasure { get; set; }

    [Display(Name = "Order by")]
    public EquipmentItemCategoryOrderType OrderType { get; set; }
}