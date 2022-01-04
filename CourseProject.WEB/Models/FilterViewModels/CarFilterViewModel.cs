using System.ComponentModel.DataAnnotations;
using CourseProject.BLL.FilterModels;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class CarFilterViewModel : FilterViewModel {

    public uint? BrandId { get; set; }

    public uint? ModelId { get; set; }

    [Display(Name = "Order by")]
    public CarOrderType? OrderType { get; set; }

    [Display(Name = "Search by model")]
    public string? Model { get; set; }
}