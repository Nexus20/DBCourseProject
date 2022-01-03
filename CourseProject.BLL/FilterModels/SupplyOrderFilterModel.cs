using CourseProject.Domain;

namespace CourseProject.BLL.FilterModels; 

public class SupplyOrderFilterModel : FilterModel {

    public int OrderId { get; set; }

    public int SupplierId { get; set; }

    public string ManagerId { get; set; }

    public SupplyOrderState? State { get; set; }

    public SupplyOrderOrderType OrderType { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime LastUpdateDate { get; set; }

    public string SupplierName { get; set; }

    public string SupplierEmail { get; set; }

    public string SupplierPhone { get; set; }
}