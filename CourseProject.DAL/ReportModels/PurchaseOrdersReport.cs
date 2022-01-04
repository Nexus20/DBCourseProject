using System.ComponentModel.DataAnnotations;

namespace CourseProject.DAL.ReportModels; 

public class PurchaseOrdersReport {
    public List<PurchaseOrdersReportPart> Parts { get; set; } = new();
}

public class PurchaseOrdersReportPart {

    [Display(Name = "Order")]
    public int OrderId { get; set; }

    public string Client  { get; set; }

    [Display(Name = "Client email")]
    public string ClientEmail  { get; set; }

    [Display(Name = "Client phone")]
    public string ClientPhone  { get; set; }

    public string Manager { get; set; }

    [Display(Name = "Manager email")]
    public string ManagerEmail { get; set; }

    [Display(Name = "Manager phone")]
    public string ManagerPhone { get; set; }

    public string Car { get; set; }

    public string VinCode { get; set; }

    public string Showroom { get; set; }

    [Display(Name = "Creation date")]
    [DataType(DataType.DateTime)]
    public DateTime CreationDate { get; set; }

    [Display(Name = "Last update date")]
    [DataType(DataType.DateTime)]
    public DateTime LastUpdateDate { get; set; }

    public decimal Profit { get; set; }
}