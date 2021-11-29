namespace CourseProject.BLL.DTO; 

public class CarDto : BaseDto {
    public string Submodel { get; set; }

    public int ModelId { get; set; }

    public ModelDto Model { get; set; }

    public ICollection<CarPhotoDto> Photos { get; set; }

    public virtual ICollection<EquipmentItemDto> EquipmentItems { get; set; }
}