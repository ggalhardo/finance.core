namespace Finance.Domain.DomainObjects
{
    public class ResponseModel<T>
    {
        public T result { get; private set; }
        public bool error { get; private set; }
        public string message { get; private set; }

        public ResponseModel()
        {
            error = false;
        }

        public void AddError(bool _error, string _message)
        {
            error = _error;
            message = _message;
        }

        public void AddResult(T _result)
        {
            result = _result;
            error = false;
            message = string.Empty;
        }

        public string GetErrorMessage()
        {
            return message;
        }

        public bool HasError()
        {
            return error;
        }

        public bool HasResult()
        {
            return result != null;
        }
    }
}
