using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.EquipmentItemCategoryDataHandlers; 

public class EquipmentItemCategoryUnitsOfMeasureSearchDataHandler : DataHandler<EquipmentItemCategory, EquipmentItemCategoryFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<EquipmentItemCategory> expressions, EquipmentItemCategoryFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.UnitsOfMeasure)) {
            expressions.FilterExpressions.Add(c => c.UnitsOfMeasure.Contains(filterModel.UnitsOfMeasure));
        }

        base.AddExpression(expressions, filterModel);
    }
}