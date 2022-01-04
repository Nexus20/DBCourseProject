using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseProject.WEB.Models; 

public class CreatePurchaseOrderViewModel {
    
    public string ClientId { get; set; }

    [Display(Name = "Showroom")]
    public int ShowroomId { get; set; }

    public ClientPersonalDataViewModel ClientPersonalDataViewModel { get; set; }

    public List<PurchaseOrderItem> Items { get; set;}
}

public class ClientPersonalDataViewModel {

    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string Patronymic { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }
}

public class PurchaseOrderItem {

    public string EquipmentItemCategoryName { get; set; }

    public SelectList SelectList { get; set; }

}