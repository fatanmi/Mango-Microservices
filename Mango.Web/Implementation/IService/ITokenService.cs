namespace Mango.Web.Implementation.IService
{
    public interface ITokenService
    {
        void SetToken(string token);
        string GetToken();
        void RemoveToken();
    }
}
