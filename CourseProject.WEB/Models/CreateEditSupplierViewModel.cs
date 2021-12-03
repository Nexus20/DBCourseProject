using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class CreateEditSupplierViewModel : BaseViewModel {

    [Required]
    public string Name { get; set; }

    [Phone]
    [Required]
    public string Phone { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public int BrandId { get; set; }
}