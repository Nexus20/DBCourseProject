namespace CourseProject.BLL.DTO; 

public class SupplierDto : BaseDto {
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int BrandId { get; set; }

    public virtual BrandDto Brand { get; set; }

    public virtual ICollection<SupplyOrderDto> SupplyOrders { get; set; }
}