using CourseProject.BLL.FilterModels;
using CourseProject.WEB.Models.FilterViewModels;

namespace CourseProject.WEB.Models.PaginatedFilteredViewModels; 

public class SuppliersWithFiltersViewModel {

    public List<SupplierViewModel> Suppliers { get; set; }

    public SupplierFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public uint? SelectedBrand { get; set; }

    public SupplierOrderType? SelectedOrderType { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }
}