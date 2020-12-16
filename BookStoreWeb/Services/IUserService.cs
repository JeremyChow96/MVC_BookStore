namespace BookStoreWeb.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}