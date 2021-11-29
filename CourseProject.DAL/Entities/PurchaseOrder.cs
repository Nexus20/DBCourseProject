using CourseProject.Domain;

namespace CourseProject.DAL.Entities {
    public class PurchaseOrder : BaseEntity {

        public PurchaseOrder() {
            State = PurchaseOrderState.New;
        }

        public string ClientId { get; set; }

        public virtual Client Client { get; set; }

        public Guid ManagerId { get; set; }

        public virtual Manager Manager { get; set; }

        public PurchaseOrderState State { get; set; }

        public virtual ICollection<PurchaseOrderEquipmentItemValue> PurchaseOrderEquipmentItemsValues { get; set; }
    }
}
