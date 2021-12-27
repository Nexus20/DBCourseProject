namespace CourseProject.WEB.Models; 

public class MostPurchasedViewModel {

    public string Brand { get; set; }

    public string Model { get; set; }

    public int OrdersCount { get; set; }

    public string BrandWithModel => $"{Brand} {Model}";
}
