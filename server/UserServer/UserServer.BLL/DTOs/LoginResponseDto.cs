namespace UserServer.BLL.DTOs
{
    public class LoginResponseDto
    {
        public GetUserDto User{ get; set; }
        public string token { get; set; }
    }
}
