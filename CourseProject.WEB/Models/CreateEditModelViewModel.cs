using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class CreateEditModelViewModel : BaseViewModel {
    
    [Required]
    [Display(Name = "Model")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Brand")]
    public int BrandId { get; set; }
}