using CourseProject.Domain;

namespace CourseProject.BLL.DTO; 

public class SupplyOrderDto : BaseDto {

    public int SupplierId { get; set; }

    public SupplierDto Supplier { get; set; }

    public Guid ManagerId { get; set; }

    public ManagerDto Manager { get; set; }

    public SupplyOrderState State { get; set; }

    public virtual ICollection<SupplyOrderPartDto> Parts { get; set; }

}