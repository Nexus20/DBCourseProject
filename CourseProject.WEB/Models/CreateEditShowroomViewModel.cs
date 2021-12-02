using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class CreateEditShowroomViewModel : BaseViewModel {

    [Required]
    public string City { get; set; }

    [Required]
    public string Street { get; set; }

    [Required]
    public string House { get; set; }

    [Phone]
    [Required]
    public string Phone { get; set; }
}