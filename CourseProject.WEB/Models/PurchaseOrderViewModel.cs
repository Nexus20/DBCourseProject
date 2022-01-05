using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CourseProject.Domain;

namespace CourseProject.WEB.Models {
    public class PurchaseOrderViewModel : BaseViewModel {

        public string ClientId { get; set; }

        public ClientViewModel Client { get; set; }

        public Guid ManagerId { get; set; }

        public ManagerViewModel Manager { get; set; }

        public PurchaseOrderState State { get; set; }

        public ICollection<EquipmentItemValueViewModel> EquipmentItemsValues { get; set; }

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last update date")]
        public DateTime LastUpdateDate { get; set; }

        [Display(Name = "VIN Code")]
        public string? VinCode { get; set; }

        public int ShowroomId { get; set; }

        public ShowroomViewModel Showroom { get; set; }

        [Display(Name = "Full price")]
        public string FullPrice => "$ " + EquipmentItemsValues.Sum(x => x.Price).ToString(CultureInfo.InvariantCulture);
    }
}
