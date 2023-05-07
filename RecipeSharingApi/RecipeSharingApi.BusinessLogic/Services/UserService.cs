using Microsoft.AspNetCore.Http;

using System.Security.Claims;


namespace RecipeSharingApi.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //public string GetMyName()
        //{
        //    var result = string.Empty;
        //    if (_httpContextAccessor.HttpContext is not null)
        //    {
        //        result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        //    }
        //    return result;
        //}
        public Guid GetMyId()
        {
            var result = new Guid();
            if (_httpContextAccessor.HttpContext is not null)
            {
                result = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
            return result;
        }
    }
}
