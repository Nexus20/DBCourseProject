namespace CourseProject.WEB.Models; 

public class UserViewModel {
    
    public string UserId { get; set; }
    
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public ManagerViewModel Manager { get; set; }
}