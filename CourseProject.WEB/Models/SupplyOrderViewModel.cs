using CourseProject.Domain;

namespace CourseProject.WEB.Models; 

public class SupplyOrderViewModel : BaseViewModel {

    public int SupplierId { get; set; }

    public SupplierViewModel Supplier { get; set; }

    public Guid ManagerId { get; set; }

    public ManagerViewModel Manager { get; set; }

    public SupplyOrderState State { get; set; }

    public virtual ICollection<SupplyOrderPartViewModel> Parts { get; set; }

}