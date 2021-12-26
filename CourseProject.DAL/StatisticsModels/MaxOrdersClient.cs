namespace CourseProject.DAL.StatisticsModels; 

public class MaxOrdersClient {
    
    public string Id { get; set; }

    public string Name { get; set; }

    public string Surname  { get; set; }

    public string Patronymic { get; set; }

    public string Email { get; set; }

    public int OrdersCount { get; set; }
}