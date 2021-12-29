using CourseProject.BLL.FilterModels;
using CourseProject.Domain;
using CourseProject.WEB.Models.FilterViewModels;

namespace CourseProject.WEB.Models.PaginatedFilteredViewModels; 

public class PurchaseOrdersWithFiltersViewModel {

    public List<PurchaseOrderViewModel> PurchaseOrders { get; set; }

    public PurchaseOrderFilterViewModel Filters { get; set; }

    public PageViewModel PageViewModel { get; set; }

    public int? SelectedOrderId { get; set; }

    public string? SelectedManagerId { get; set; }

    public PurchaseOrderState? SelectedState { get; set; }

    public PurchaseOrderOrderType? SelectedOrderType { get; set; }

    public string? SelectedCreationDate { get; set; }

    public string? SelectedLastUpdateDate { get; set; }

    public string? SelectedClientEmail { get; set; }

    public string? SelectedClientPhone { get; set; }
}