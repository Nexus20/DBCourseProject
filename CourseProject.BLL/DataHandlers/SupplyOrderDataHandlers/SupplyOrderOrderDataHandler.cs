using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.SupplyOrderDataHandlers {

    public class SupplyOrderOrderDataHandler : DataHandler<SupplyOrder, SupplyOrderFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<SupplyOrder> expressions, SupplyOrderFilterModel filterModel) {

            switch (filterModel.OrderType) {
                case SupplyOrderOrderType.OrderIdAsc:
                    expressions.AscendingOrderExpressions.Add(p => p.Id);
                    break;
                case SupplyOrderOrderType.OrderIdDesc:
                    expressions.DescendingOrderExpressions.Add(p => p.Id);
                    break;
                case SupplyOrderOrderType.CreationDateAsc:
                    expressions.AscendingOrderExpressions.Add(p => p.CreationDate);
                    break;
                case SupplyOrderOrderType.CreationDateDesc:
                    expressions.DescendingOrderExpressions.Add(p => p.CreationDate);
                    break;
                case SupplyOrderOrderType.LastUpdateDateAsc:
                    expressions.AscendingOrderExpressions.Add(p => p.LastUpdateDate);
                    break;
                case SupplyOrderOrderType.LastUpdateDateDesc:
                    expressions.DescendingOrderExpressions.Add(p => p.LastUpdateDate);
                    break;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}