using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.ShowroomDataHandlers {

    public class ShowroomOrderDataHandler : DataHandler<Showroom, ShowroomFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<Showroom> expressions, ShowroomFilterModel filterModel) {

            switch (filterModel.OrderType) {
                case ShowroomOrderType.AlphabetAsc:
                    expressions.AscendingOrderExpressions.Add(c => c.City);
                    expressions.AscendingOrderExpressions.Add(c => c.Street);
                    expressions.AscendingOrderExpressions.Add(c => c.House);
                    break;
                case ShowroomOrderType.AlphabetDesc:
                    expressions.DescendingOrderExpressions.Add(c => c.City);
                    expressions.DescendingOrderExpressions.Add(c => c.Street);
                    expressions.DescendingOrderExpressions.Add(c => c.House);
                    break;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}