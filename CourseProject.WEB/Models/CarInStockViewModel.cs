using System.ComponentModel.DataAnnotations;

namespace CourseProject.WEB.Models; 

public class CarInStockViewModel : BaseViewModel {

    public string VinCode { get; set; }

    public int CarId { get; set; }

    public CarViewModel Car { get; set; }

    [Display(Name = "Configuration")]
    public ICollection<EquipmentItemValueViewModel> EquipmentItemValues { get; set; }

    public int ShowroomId { get; set; }

    public ShowroomViewModel Showroom { get; set; }

    public decimal FullPrice => EquipmentItemValues.Sum(x => x.Price);
}