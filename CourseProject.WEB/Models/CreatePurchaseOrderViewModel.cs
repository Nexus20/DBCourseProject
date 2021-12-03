using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Models; 

public class CreatePurchaseOrderViewModel {
    
    public string ClientId { get; set; }

    public List<PurchaseOrderItem> Items { get; set;}

}

public class PurchaseOrderItem {

    public string EquipmentItemCategoryName { get; set; }

    public SelectList SelectList { get; set; }

}