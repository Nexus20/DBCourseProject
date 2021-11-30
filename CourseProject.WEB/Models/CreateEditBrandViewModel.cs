using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class CreateEditBrandViewModel : BaseViewModel {
    
    [Required]
    [Display(Name = "Brand")]
    public string Name { get; set; }
}