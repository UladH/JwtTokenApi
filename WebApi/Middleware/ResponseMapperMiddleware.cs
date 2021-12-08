using AppConfiguration;
using AppExceptions.Identity;
using Newtonsoft.Json;
using System.Net;
using WebApi.Models;

namespace WebApi.Middleware
{
    public class ResponseMapperMiddleware
    {
        private RequestDelegate next;

        #region constructor

        public ResponseMapperMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        #endregion

        #region public

        public async Task InvokeAsync(HttpContext context, IAppConfiguration appConfiguration, IWebHostEnvironment environment)
        {
            context.Response.ContentType = "application/json";

            var originBody = context.Response.Body;
            ErrorResponseModel error = null;

            using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;

                try
                {
                    await next(context);
                    ThrowExceptionByStatusCode(context.Response.StatusCode);
                }
                catch (Exception exception)
                {
                    context.Response.StatusCode = GetStatusCodeByException(exception);

                    error = GetErrorModel(exception, environment);
                }

                var newBody = new ResponseModel()
                {
                    StatusCode = context.Response.StatusCode,
                    Data = DeserializeFromStream(memStream),
                    Error = error
                };

                WriteObjectToStream(newBody, memStream);
                await memStream.CopyToAsync(originBody).ConfigureAwait(false);
                context.Response.Body = originBody;
            }
        }

        #endregion

        #region private

        private ErrorResponseModel GetErrorModel(Exception exception, IWebHostEnvironment environment)
        {
            if (exception == null)
            {
                return null;
            }

            var errorModel = new ErrorResponseModel()
            {
                Message = exception.Message
            };

            if (environment.IsDevelopment())
            {
                errorModel.StackTrace = exception.StackTrace;
            }

            return errorModel;
        }

        private int GetStatusCodeByException(Exception exception)
        {

            switch (exception)
            {
                case TimeoutException e:
                    return (int)HttpStatusCode.BadRequest;
                case IdentityException e:
                    return (int)HttpStatusCode.BadRequest;
                case KeyNotFoundException e:
                    return (int)HttpStatusCode.NotFound;
                case UnauthorizedAccessException e:
                    return (int)HttpStatusCode.Unauthorized;
                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }

        private void ThrowExceptionByStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case (int)HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException();
            }
        }

        private object DeserializeFromStream(MemoryStream memStream)
        {
            memStream.Position = 0;
            string objectString = new StreamReader(memStream).ReadToEnd();
            return JsonConvert.DeserializeObject<object>(objectString);
        }

        private void WriteObjectToStream(object model, MemoryStream stream)
        {
            var json = JsonConvert.SerializeObject(model);
            var streamWriter = new StreamWriter(stream);
            stream.SetLength(0);
            streamWriter.Write(json);
            streamWriter.Flush();
            stream.Position = 0;
        }

        #endregion
    }
}
