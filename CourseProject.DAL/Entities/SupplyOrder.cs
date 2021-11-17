using CourseProject.Domain;

namespace CourseProject.DAL.Entities; 

public class SupplyOrder : BaseEntity {

    public SupplyOrder() {
        State = SupplyOrderState.New;
    }

    public int SupplierId { get; set; }

    public virtual Supplier Supplier { get; set; }

    public int ManagerId { get; set; }

    public virtual Manager Manager { get; set; }

    public SupplyOrderState State { get; set; }

    public virtual ICollection<SupplyOrderPart> Parts { get; set; }

}