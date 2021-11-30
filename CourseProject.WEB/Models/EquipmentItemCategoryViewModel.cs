﻿using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class EquipmentItemCategoryViewModel : BaseViewModel {

    [Required]
    public string Name { get; set; }

    [Display(Name = "Units of measure")]
    public string UnitsOfMeasure { get; set; }

    public ICollection<EquipmentItemViewModel> EquipmentItems { get; set; }
}