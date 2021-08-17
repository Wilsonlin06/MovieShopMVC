using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int UserId =>
            Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public bool IsAuthenticated { get; }
        public string Email => _httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public string FullName => _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value + " " +
                                  _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
        public bool IsAdmin { get; }
        public bool IsSuperAdmin { get; }
        public IEnumerable<string> Roles { get; }
    }
}
