﻿namespace CourseProject.WEB.Models; 

public class UserViewModel {

    public string Id { get; set; }

    public string UserName { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public ManagerViewModel Manager { get; set; }

    public string Password { get; set; }

    public string Role { get; set; } = "user";
}