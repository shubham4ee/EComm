using System.Net;

namespace UserServer.BLL.DTOs
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
        public Object Error { get; set; }
    }
}
