using CourseProject.DAL.Entities;
using CourseProject.Domain;

namespace CourseProject.WEB.Models {
    public class PurchaseOrderViewModel : BaseViewModel {

        public string ClientId { get; set; }

        public ClientViewModel Client { get; set; }

        public Guid ManagerId { get; set; }

        public ManagerViewModel Manager { get; set; }

        public PurchaseOrderState State { get; set; }

        public ICollection<EquipmentItemValueViewModel> EquipmentItemsValues { get; set; }
    }
}
