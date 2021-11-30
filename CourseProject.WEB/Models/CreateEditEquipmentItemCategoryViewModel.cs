using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class CreateEditEquipmentItemCategoryViewModel : BaseViewModel {

    [Required]
    public string Name { get; set; }

    [Display(Name = "Units of measure")]
    public string UnitsOfMeasure { get; set; }
}