﻿namespace CourseProject.WEB.Models; 

public class ShowroomViewModel : BaseViewModel {

    public string City { get; set; }

    public string Street { get; set; }

    public string House { get; set; }

    public string Phone { get; set; }

    public string FullAddress => $"Showroom #{Id} {City} {Street} {House}";

    public ICollection<ManagerViewModel> Managers { get; set; }

    public ICollection<CarInStockViewModel> CarsInStock { get; set; }
}