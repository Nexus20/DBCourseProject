namespace CourseProject.BLL.DTO; 

public class CarInStockDto : BaseDto {
    public string VinCode { get; set; }

    public int CarId { get; set; }

    public CarDto Car { get; set; }

    public ICollection<EquipmentItemValueDto> EquipmentItemValues { get; set; }

    public int ShowroomId { get; set; }

    public ShowroomDto Showroom { get; set; }
}