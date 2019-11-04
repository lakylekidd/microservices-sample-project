namespace Basket.API.Services
{
    public interface IIdentityService
    {
        /// <summary>
        /// Returns the user identity
        /// </summary>
        /// <returns></returns>
        string GetUserIdentity();
    }
}
