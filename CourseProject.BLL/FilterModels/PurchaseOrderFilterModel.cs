using CourseProject.Domain;

namespace CourseProject.BLL.FilterModels; 

public class PurchaseOrderFilterModel : FilterModel {

    public int OrderId { get; set; }

    public int ShowroomId { get; set; }

    public string ManagerId { get; set; }

    public PurchaseOrderState? State { get; set; }

    public PurchaseOrderOrderType OrderType { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime LastUpdateDate { get; set; }

    public string ClientEmail { get; set; }

    public string ClientPhone { get; set; }
}