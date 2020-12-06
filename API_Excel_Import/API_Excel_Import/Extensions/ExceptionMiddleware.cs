using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace API_Excel_Import.Extensions
{
    [Serializable]
    public class ExceptionMiddleware : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public List<string> Errors { get; }

        public List<ValidationFailure> Failures { get; }

        public ExceptionMiddleware(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(message) => StatusCode = httpStatusCode;

        public ExceptionMiddleware(string message, List<ValidationFailure> failures, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = httpStatusCode;
            Errors = failures?.Select(f => f.ErrorMessage).ToList();
        }

        public ExceptionMiddleware(string message, List<string> errors, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = httpStatusCode;
            Errors = errors;
        }

        protected ExceptionMiddleware(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
