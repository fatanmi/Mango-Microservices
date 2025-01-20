using Mango.Web.Models;

namespace Mango.Web.Implementation.IService
{
    public interface IBaseService<T> where T : class
    {
        Task<T> SendAsync(RequestDto requestDto, bool plusToken = true);
        //Task<LoginResponseDto> SignUserIn(RequestDto requestDto);
    }
}
