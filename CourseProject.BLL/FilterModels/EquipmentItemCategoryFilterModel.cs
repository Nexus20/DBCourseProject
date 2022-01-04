namespace CourseProject.BLL.FilterModels; 

public class EquipmentItemCategoryFilterModel : FilterModel {
    
    public string Name { get; set; }

    public string UnitsOfMeasure { get; set; }

    public EquipmentItemCategoryOrderType OrderType { get; set; }
}