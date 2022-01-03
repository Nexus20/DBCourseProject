using System.ComponentModel.DataAnnotations;
using CourseProject.BLL.FilterModels;
using CourseProject.Domain;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class SupplyOrderFilterViewModel : FilterViewModel {

    public int? OrderId { get; set; }

    public int? SupplierId { get; set; }

    public string? ManagerId { get; set; }

    public SupplyOrderState? State { get; set; }

    public SupplyOrderOrderType? OrderType { get; set; }

    [DataType(DataType.Date)]
    public DateTime? CreationDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? LastUpdateDate { get; set; }

    public string? SupplierName { get; set; }

    public string? SupplierEmail { get; set; }

    public string? SupplierPhone { get; set; }
}