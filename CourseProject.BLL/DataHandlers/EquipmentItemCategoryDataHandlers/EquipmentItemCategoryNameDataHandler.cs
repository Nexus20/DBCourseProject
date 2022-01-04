using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.EquipmentItemCategoryDataHandlers; 

public class EquipmentItemCategoryNameDataHandler : DataHandler<EquipmentItemCategory, EquipmentItemCategoryFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<EquipmentItemCategory> expressions, EquipmentItemCategoryFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Name)) {
            expressions.FilterExpressions.Add(c => c.Name.Contains(filterModel.Name));
        }

        base.AddExpression(expressions, filterModel);
    }
}