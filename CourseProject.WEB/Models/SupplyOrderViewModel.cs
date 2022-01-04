using System.ComponentModel.DataAnnotations;
using CourseProject.Domain;

namespace CourseProject.WEB.Models; 

public class SupplyOrderViewModel : BaseViewModel {

    [Display(Name = "Supplier")]
    public int SupplierId { get; set; }

    public SupplierViewModel? Supplier { get; set; }

    public Guid ManagerId { get; set; }

    public ManagerViewModel? Manager { get; set; }

    public SupplyOrderState State { get; set; }

    public virtual ICollection<SupplyOrderPartViewModel>? Parts { get; set; }

    [Display(Name = "Creation date")]
    public DateTime CreationDate { get; set; }

    [Display(Name = "Last update date")]
    public DateTime LastUpdateDate { get; set; }
}