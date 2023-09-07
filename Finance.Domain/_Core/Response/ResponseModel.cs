using System.Text.Json.Serialization;

namespace Finance.Domain._Core.Response
{
    public class ResponseModel<T>
    {
        [JsonPropertyName("result")]
        public T Result { get; private set; }

        [JsonPropertyName("error")]
        public bool Error { get; private set; }

        [JsonPropertyName("message")]
        public string Message { get; private set; }

        public ResponseModel()
        {
            this.Error = false;
        }

        public void AddError(string pMssage)
        {
            this.Error = true;
            this.Message = pMssage;
        }

        public void AddResult(T pResult)
        {
            this.Result = pResult;
            this.Error = false;
        }

        public void AddMessage(string pMessage)
        {
            this.Message = pMessage;
        }

        public string GetMessage()
        {
            return this.Message;
        }

        public bool HasError()
        {
            return this.Error;
        }

        public bool HasResult()
        {
            return this.Result != null;
        }

        public ResponseModel<T> GetResponse()
        {
            return this;
        }
    }
}
