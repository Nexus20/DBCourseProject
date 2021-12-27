using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.ModelDataHandlers {

    public class AlphabetOrderDataHandler : DataHandler<Model, ModelFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<Model> expressions, ModelFilterModel filterModel) {

            switch (filterModel.OrderType) {
                case ModelOrderType.AlphabetAsc:
                    expressions.AscendingOrderExpressions.Add(m => m.Brand.Name);
                    expressions.AscendingOrderExpressions.Add(m => m.Name);
                    break;
                case ModelOrderType.AlphabetDesc:
                    expressions.DescendingOrderExpressions.Add(m => m.Brand.Name);
                    expressions.DescendingOrderExpressions.Add(m => m.Name);
                    break;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}