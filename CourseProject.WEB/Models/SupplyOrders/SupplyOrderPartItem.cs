using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Models.SupplyOrders; 

public class SupplyOrderPartItem {

    public string EquipmentItemCategoryName { get; set; }

    public SelectList SelectList { get; set; }
}