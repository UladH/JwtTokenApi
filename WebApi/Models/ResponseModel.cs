namespace WebApi.Models
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public ErrorResponseModel Error { get; set; }
    }
}
