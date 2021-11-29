namespace CourseProject.WEB.Models; 

public class ShowroomViewModel : BaseViewModel {

    public string City { get; set; }

    public string Street { get; set; }

    public string House { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<ManagerViewModel> Managers { get; set; }
}