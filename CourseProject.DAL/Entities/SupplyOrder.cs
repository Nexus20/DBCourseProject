using CourseProject.Domain;

namespace CourseProject.DAL.Entities; 

public class SupplyOrder : BaseEntity {

    public SupplyOrder() {
        State = SupplyOrderState.New;
        CreationDate = LastUpdateDate = DateTime.Now;
    }

    public int SupplierId { get; set; }

    public virtual Supplier Supplier { get; set; }

    public Guid ManagerId { get; set; }

    public virtual Manager Manager { get; set; }

    public SupplyOrderState State { get; set; }

    public virtual ICollection<SupplyOrderPart> Parts { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime LastUpdateDate { get; set; }
}