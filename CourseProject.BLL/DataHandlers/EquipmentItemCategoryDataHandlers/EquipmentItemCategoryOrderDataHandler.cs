using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.EquipmentItemCategoryDataHandlers {

    public class EquipmentItemCategoryOrderDataHandler : DataHandler<EquipmentItemCategory, EquipmentItemCategoryFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<EquipmentItemCategory> expressions, EquipmentItemCategoryFilterModel filterModel) {

            switch (filterModel.OrderType) {
                case EquipmentItemCategoryOrderType.AlphabetAsc:
                    expressions.AscendingOrderExpressions.Add(c => c.Name);
                    break;
                case EquipmentItemCategoryOrderType.AlphabetDesc:
                    expressions.DescendingOrderExpressions.Add(c => c.Name);
                    break;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}