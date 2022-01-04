using CourseProject.BLL.FilterModels;
using CourseProject.DAL.Entities;
using CourseProject.DAL.SelectionPipelineExpressions;

namespace CourseProject.BLL.DataHandlers.ShowroomDataHandlers; 

public class ShowroomPhoneSearchDataHandler : DataHandler<Showroom, ShowroomFilterModel> {
    public override void AddExpression(SelectionPipelineExpressions<Showroom> expressions, ShowroomFilterModel filterModel) {

        if (!string.IsNullOrWhiteSpace(filterModel.Phone)) {
            expressions.FilterExpressions.Add(c => c.Phone.Contains(filterModel.Phone));
        }

        base.AddExpression(expressions, filterModel);
    }
}