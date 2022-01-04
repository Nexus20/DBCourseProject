namespace CourseProject.WEB.Models.ReportViewModels; 

public class PurchaseOrdersReportViewModel {
    public List<PurchaseOrdersReportPartViewModel> Parts { get; set; } = new();
}

public class PurchaseOrdersReportPartViewModel {

    public int OrderId { get; set; }

    public string Client  { get; set; }

    public string ClientEmail  { get; set; }

    public string ClientPhone  { get; set; }

    public string Manager { get; set; }

    public string ManagerEmail { get; set; }

    public string ManagerPhone { get; set; }

    public string Car { get; set; }

    public string VinCode { get; set; }

    public string Showroom { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime LastUpdateDate { get; set; }

    public decimal Profit { get; set; }
}