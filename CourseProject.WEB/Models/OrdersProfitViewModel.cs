using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models;

public class OrdersProfitViewModel {

    [Display(Name = "Orders count")]
    public int OrdersCount { get; set; }

    public decimal Profit { get; set; }
}