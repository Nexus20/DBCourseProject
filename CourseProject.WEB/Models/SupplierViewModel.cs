namespace CourseProject.WEB.Models; 

public class SupplierViewModel : BaseViewModel {
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int BrandId { get; set; }

    public virtual BrandViewModel Brand { get; set; }

    public virtual ICollection<SupplyOrderViewModel> SupplyOrders { get; set; }
}