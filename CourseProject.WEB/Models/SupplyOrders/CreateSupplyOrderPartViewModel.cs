using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models.SupplyOrders; 

public class CreateSupplyOrderPartViewModel {

    public int SupplyOrderId { get; set; }

    public CarViewModel? Car { get; set; }

    public List<SupplyOrderPartItem>? Items { get; set; }

    [Range(1, int.MaxValue)]
    public int Count { get; set; }

    public int[]? Equipment { get; set; }
}