using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models.ReportViewModels;

public class SupplyOrdersReportViewModel {
    public List<SupplyOrdersReportPartViewModel> Parts { get; set; } = new();
}

public class SupplyOrdersReportPartViewModel {

    [Display(Name = "Order")]
    public int OrderId { get; set; }

    [Display(Name = "Cars count")]
    public int CarsCount { get; set; }

    [Display(Name = "Creation date")]
    [DataType(DataType.DateTime)]
    public DateTime CreationDate { get; set; }

    [Display(Name = "Last update date")]
    [DataType(DataType.DateTime)]
    public DateTime LastUpdateDate { get; set; }

    public string Showroom { get; set; }

    [Display(Name = "Supplier")]
    public string SupplierName { get; set; }

    [Display(Name = "Supplier email")]
    public string SupplierEmail { get; set; }

    [Display(Name = "Supplier phone")]
    public string SupplierPhone { get; set; }

    public string Car { get; set; }

    public string Manager { get; set; }

    [Display(Name = "Manager email")]
    public string ManagerEmail { get; set; }

    [Display(Name = "Manager phone")]
    public string ManagerPhone { get; set; }

    public decimal Price { get; set; }
}