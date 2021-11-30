using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class CreateEditCarViewModel : BaseViewModel {
    
    public string Submodel { get; set; }

    [Required]
    [Display(Name = "Model")]
    public int ModelId { get; set; }

    public IFormFileCollection Images { get; set; }
}