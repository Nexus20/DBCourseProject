namespace CourseProject.BLL.DTO; 

public class UserDto {
    
    public string UserId { get; set; }

    public string UserName { get; set; }
    
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public ManagerDto Manager { get; set; }

    public string Password { get; set; }

    public string Role { get; set; } = "user";
}