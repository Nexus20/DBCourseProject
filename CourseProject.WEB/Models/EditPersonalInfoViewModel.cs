using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class EditPersonalInfoViewModel {

    public string Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    [Phone]
    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}