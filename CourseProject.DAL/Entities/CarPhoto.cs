namespace CourseProject.DAL.Entities; 

public class CarPhoto : BaseEntity {

    public string Path { get; set; }

    public int CarId { get; set; }

    public virtual Car Car { get; set; }
}