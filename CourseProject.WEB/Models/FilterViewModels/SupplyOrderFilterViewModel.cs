using System.ComponentModel.DataAnnotations;
using CourseProject.BLL.FilterModels;
using CourseProject.Domain;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class SupplyOrderFilterViewModel : FilterViewModel {

    [Display(Name = "Order id")]
    public int? OrderId { get; set; }

    public int? SupplierId { get; set; }

    public string? ManagerId { get; set; }

    public SupplyOrderState? State { get; set; }

    [Display(Name = "Order by")]
    public SupplyOrderOrderType? OrderType { get; set; }

    [Display(Name = "Creation date")]
    [DataType(DataType.Date)]
    public DateTime? CreationDate { get; set; }

    [Display(Name = "Last update date")]
    [DataType(DataType.Date)]
    public DateTime? LastUpdateDate { get; set; }

    public string? SupplierName { get; set; }

    public string? SupplierEmail { get; set; }

    public string? SupplierPhone { get; set; }
}