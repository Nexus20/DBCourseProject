namespace CourseProject.BLL.FilterModels; 

public class ShowroomFilterModel : FilterModel {

    public ShowroomOrderType OrderType { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }
}