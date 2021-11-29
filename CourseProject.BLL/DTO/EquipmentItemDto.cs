namespace CourseProject.BLL.DTO; 

public class EquipmentItemDto : BaseDto {

    public int CarId { get; set; }

    public virtual CarDto Car { get; set; }

    public bool Optional { get; set; }

    public ICollection<EquipmentItemValueDto> EquipmentItemValues { get; set; }

    public int EquipmentItemCategoryId { get; set; }

    public EquipmentItemCategoryDto Category { get; set; }
}