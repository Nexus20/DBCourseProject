using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class UserViewModel {

    public string Id { get; set; }

    [Display(Name = "Login")]
    public string UserName { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public ManagerViewModel Manager { get; set; }

    public string Password { get; set; }

    public string Role { get; set; } = "user";

    public string FullName => $"{Surname} {Name} {Patronymic}";
}