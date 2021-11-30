using CourseProject.BLL.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CourseProject.WEB.Extensions; 

public static class ModelStateDictionaryExtensions {

    public static void AddErrorsFromOperationResult(this ModelStateDictionary modelState,
        OperationResult operationResult) {

        foreach (var (key, errors) in operationResult.Errors) {
            foreach (var error in errors) {
                modelState.AddModelError(key, error);
            }
        }
    }
}