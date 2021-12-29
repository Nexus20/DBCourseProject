using CourseProject.BLL.FilterModels;
using CourseProject.Domain;
using CourseProject.WEB.Models.FilterViewModels;

namespace CourseProject.WEB.Models.PaginatedFilteredViewModels; 

public class SupplyOrdersWithFiltersViewModel {

    public List<SupplyOrderViewModel> SupplyOrders { get; set; }

    public SupplyOrderFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public int? SelectedOrderId { get; set; }

    public int? SelectedSupplierId { get; set; }

    public string? SelectedManagerId { get; set; }

    public SupplyOrderState? SelectedState { get; set; }

    public SupplyOrderOrderType? SelectedOrderType { get; set; }

    public string? SelectedCreationDate { get; set; }

    public string? SelectedLastUpdateDate { get; set; }

    public string? SelectedSupplierName { get; set; }

    public string? SelectedSupplierEmail { get; set; }

    public string? SelectedSupplierPhone { get; set; }
}