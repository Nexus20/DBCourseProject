using System.ComponentModel.DataAnnotations;
using CourseProject.BLL.FilterModels;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class SupplierFilterViewModel : FilterViewModel {
    public uint? BrandId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    [Display(Name = "Order by")]
    public SupplierOrderType OrderType { get; set; }
}