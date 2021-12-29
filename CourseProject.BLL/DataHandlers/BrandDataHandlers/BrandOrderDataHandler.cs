using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.BrandDataHandlers {

    public class BrandOrderDataHandler : DataHandler<Brand, BrandFilterModel> {

        public override void AddExpression(SelectionPipelineExpressions<Brand> expressions, BrandFilterModel filterModel) {

            switch (filterModel.OrderType) {
                case BrandOrderType.AlphabetAsc:
                    expressions.AscendingOrderExpressions.Add(b => b.Name);
                    break;
                case BrandOrderType.AlphabetDesc:
                    expressions.DescendingOrderExpressions.Add(b => b.Name);
                    break;
                case BrandOrderType.ModelsCountAsc:
                    expressions.AscendingOrderExpressions.Add(b => b.Models.Count);
                    break;
                case BrandOrderType.ModelsCountDesc:
                    expressions.DescendingOrderExpressions.Add(b => b.Models.Count);
                    break;
                case BrandOrderType.CarsCountAsc:
                    expressions.AscendingOrderExpressions.Add(b => b.Models.SelectMany(m => m.Cars, (model, car) => car).Count());
                    break;
                case BrandOrderType.CarsCountDesc:
                    expressions.DescendingOrderExpressions.Add(b => b.Models.SelectMany(m => m.Cars, (model, car) => car).Count());
                    break;
                case BrandOrderType.SuppliersCountAsc:
                    expressions.AscendingOrderExpressions.Add(b => b.Suppliers.Count);
                    break;
                case BrandOrderType.SuppliersCountDesc:
                    expressions.DescendingOrderExpressions.Add(b => b.Suppliers.Count);
                    break;
            }

            base.AddExpression(expressions, filterModel);
        }
    }
}