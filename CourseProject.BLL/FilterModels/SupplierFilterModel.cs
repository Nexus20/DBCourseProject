namespace CourseProject.BLL.FilterModels; 

public class SupplierFilterModel : FilterModel {
    public uint? BrandId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public SupplierOrderType OrderType { get; set; }
}