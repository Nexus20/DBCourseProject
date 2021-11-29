namespace CourseProject.BLL.DTO; 

public class EquipmentItemCategoryDto : BaseDto {

    public string Name { get; set; }

    public string UnitsOfMeasure { get; set; }

    public virtual ICollection<EquipmentItemDto> EquipmentItems { get; set; }
}