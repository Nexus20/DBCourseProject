namespace CourseProject.BLL.Validation;

public class OperationResult {

    public readonly Dictionary<string, List<string>> Errors;

    public OperationResult() {
        Errors = new Dictionary<string, List<string>>();
    }

    public void AddError(string key, string message) {

        if (Errors.ContainsKey(key)) {
            Errors[key].Add(message);
        }
        else {
            Errors[key] = new List<string>() { message };
        }
    }

    public bool HasErrors => Errors.Count != 0;
}

public class OperationResult<T> : OperationResult {
    public T Result { get; set; }
}
