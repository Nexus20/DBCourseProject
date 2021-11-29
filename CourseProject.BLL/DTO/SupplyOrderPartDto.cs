namespace CourseProject.BLL.DTO; 

public class SupplyOrderPartDto : BaseDto {

    public int SupplyOrderId { get; set; }

    public SupplyOrderDto SupplyOrder { get; set; }

    public int Count { get; set; }

    public ICollection<EquipmentItemValueDto> EquipmentItemsValues { get; set; }
}