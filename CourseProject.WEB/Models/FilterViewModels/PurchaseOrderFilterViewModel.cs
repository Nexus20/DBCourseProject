using System.ComponentModel.DataAnnotations;
using CourseProject.BLL.FilterModels;
using CourseProject.Domain;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class PurchaseOrderFilterViewModel : FilterViewModel {

    [Display(Name = "Order id")]
    public int? OrderId { get; set; }

    public int? ShowroomId { get; set; }

    public string? ManagerId { get; set; }

    public PurchaseOrderState? State { get; set; }

    [Display(Name = "Order by")]
    public PurchaseOrderOrderType? OrderType { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Creation date")]
    public DateTime? CreationDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Last update date")]
    public DateTime? LastUpdateDate { get; set; }

    [Display(Name = "Client's email")]
    public string? ClientEmail { get; set; }

    [Display(Name = "Client's phone")]
    public string? ClientPhone { get; set; }
}