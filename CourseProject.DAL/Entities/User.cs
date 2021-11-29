using Microsoft.AspNetCore.Identity;

namespace CourseProject.DAL.Entities; 

public class User : IdentityUser {
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public virtual Manager Manager { get; set; }
}