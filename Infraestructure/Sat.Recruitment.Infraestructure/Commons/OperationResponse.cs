using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Infraestructure.Commons
{
    /// <summary>
    ///  OperationResponse Factory
    /// </summary>
    public class OperationResponseFactory
    {
        protected OperationResponseFactory() { }

        public static OperationResponse CreateResponse(string code, string message) =>
            new OperationResponse(code, message);

        public static OperationResponse CreateResponse(string code, string message, List<string> errors) =>
            new OperationResponse(code, message, errors);

        public static OperationResponse<T> CreateResponse<T>(T data, string code = "", string message = null) =>
            new OperationResponse<T>(code, message, data);
    }

    [Serializable]
    public class OperationResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; protected set; }
        public string Code { get; protected set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Errors { get; protected set; }

        public OperationResponse(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public OperationResponse(string code, string message, List<string> errors)
        {
            Message = message;
            Code = code;
            Errors = errors;
        }
    }

    public class OperationResponse<TData> : OperationResponse
    {
        public TData Data { get; protected set; }
        public OperationResponse(string code, string message, TData data = default) : base(code, message)
        {
            Data = data;
        }
    }
}
