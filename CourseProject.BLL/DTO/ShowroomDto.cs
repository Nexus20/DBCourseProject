namespace CourseProject.BLL.DTO; 

public class ShowroomDto : BaseDto {

    public string City { get; set; }

    public string Street { get; set; }

    public string House { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<ManagerDto> Managers { get; set; }
}