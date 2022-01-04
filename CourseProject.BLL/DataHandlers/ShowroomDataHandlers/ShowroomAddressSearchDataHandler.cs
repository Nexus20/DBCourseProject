using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.ShowroomDataHandlers; 

public class ShowroomAddressSearchDataHandler : DataHandler<Showroom, ShowroomFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Showroom> expressions, ShowroomFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Address)) {
            expressions.FilterExpressions.Add(c => c.City.Contains(filterModel.Address) || c.Street.Contains(filterModel.Address) || c.House.Contains(filterModel.Address));
        }

        base.AddExpression(expressions, filterModel);
    }
}