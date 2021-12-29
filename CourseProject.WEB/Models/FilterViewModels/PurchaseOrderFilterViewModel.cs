using System.ComponentModel.DataAnnotations;
using CourseProject.BLL.FilterModels;
using CourseProject.Domain;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class PurchaseOrderFilterViewModel : FilterViewModel {

    public int? OrderId { get; set; }

    public string? ManagerId { get; set; }

    public PurchaseOrderState? State { get; set; }

    public PurchaseOrderOrderType? OrderType { get; set; }

    [DataType(DataType.Date)]
    public DateTime? CreationDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? LastUpdateDate { get; set; }

    public string? ClientEmail { get; set; }

    public string? ClientPhone { get; set; }
}