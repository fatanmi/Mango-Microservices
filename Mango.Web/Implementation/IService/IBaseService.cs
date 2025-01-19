using Mango.Web.Models;

namespace Mango.Web.Implementation.IService
{
    public interface IBaseService<T> where T : class
    {
        Task<T> SendAsync(RequestDto requestDto);
        //Task<LoginResponseDto> SignUserIn(RequestDto requestDto);
    }
}
