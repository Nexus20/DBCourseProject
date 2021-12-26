﻿namespace CourseProject.WEB.Models;

public class MaxOrdersClientViewModel {
    public string Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string Email { get; set; }

    public int OrdersCount { get; set; }

    public string FullName => $"{Surname} {Name} {Patronymic}";
}