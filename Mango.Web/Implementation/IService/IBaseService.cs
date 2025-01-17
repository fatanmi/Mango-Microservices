using Mango.Web.Models;

namespace Mango.Web.Implementation.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
        Task<LoginResponseDto> SignUserIn(RequestDto requestDto);
    }
}
