namespace CourseProject.DAL.Entities; 

public class Model : BaseEntity {
    public string Name { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; }

    public virtual ICollection<Car> Cars { get; set; }
}