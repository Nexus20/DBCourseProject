namespace CourseProject.DAL.Entities; 

public class Car : BaseEntity {
    public string Submodel { get; set; }

    public int ModelId { get; set; }

    public virtual Model Model { get; set; }

    public virtual ICollection<CarPhoto> Photos { get; set; }
}