namespace CourseProject.BLL.DTO;

public class CloseSupplyOrderDto {

    public int SupplyOrderId { get; set; }

    public string ManagerId { get; set; }

    public ClosingSupplyOrderDtoPart[] Parts { get; set; }
}

public class ClosingSupplyOrderDtoPart {

    public int CarId { get; set; }

    public string[] VinCodes { get; set; }
}