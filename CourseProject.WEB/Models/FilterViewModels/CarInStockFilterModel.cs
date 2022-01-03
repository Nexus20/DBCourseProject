﻿using CourseProject.BLL.FilterModels;

namespace CourseProject.WEB.Models.FilterViewModels; 

public class CarInStockFilterViewModel : FilterViewModel {
    public uint BrandId { get; set; }

    public uint ModelId { get; set; }

    public uint ShowroomId { get; set; }

    public CarOrderType OrderType { get; set; }

    public string Model { get; set; }
}