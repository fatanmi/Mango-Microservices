namespace Mango.Web.Models
{
    public class ResponseDto
    {
        public Object Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = " ";
    }
    public class LoginResponseDto : ResponseDto
    {
        public string Token { get; set; }
    }
}
