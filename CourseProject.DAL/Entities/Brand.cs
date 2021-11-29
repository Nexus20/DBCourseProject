namespace CourseProject.DAL.Entities; 

public class Brand : BaseEntity {
    public string Name { get; set; }

    public virtual ICollection<Model> Models { get; set; }

    public virtual ICollection<Supplier> Suppliers { get; set; }
}