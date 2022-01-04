namespace CourseProject.DAL.ReportModels; 

public class SupplyOrdersReport {
    public List<SupplyOrdersReportPart> Parts { get; set; } = new();
}

public class SupplyOrdersReportPart {

    public int OrderId { get; set; }

    public int CarsCount { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime LastUpdateDate { get; set; }
    
    public string Showroom { get; set; }

    public string SupplierName { get; set; }

    public string SupplierEmail { get; set; }

    public string SupplierPhone { get; set; }

    public string Car { get; set; }
    
    public string Manager { get; set; }

    public string ManagerEmail { get; set; }

    public string ManagerPhone { get; set; }

    public decimal Price { get; set; }
}