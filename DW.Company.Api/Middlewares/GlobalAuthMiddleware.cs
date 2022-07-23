using Microsoft.AspNetCore.Http;
using DW.Company.Common;
using DW.Company.Contracts.Settings;
using DW.Company.Entities.Exceptions;
using DW.Company.Entities.Value;
using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DW.Company.Api.Middlewares
{
    public class GlobalAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ISessionSettings sessionSettings)
        {
            sessionSettings?.Clear();

            if (!(context.User.Identity?.IsAuthenticated ?? false))
                await _next(context);
            else
            {
                var _master = context.User.IsInRole(Constants.MASTERROLE);
                var _masterCustomer = context.User.IsInRole(Constants.MANAGERROLE);

                var _identity = context.User.Identity as ClaimsIdentity;
                sessionSettings.Role = _identity?.FindFirst(ClaimTypes.Role)?.Value;
                sessionSettings.FirstName = _identity?.FindFirst(ClaimTypes.Name)?.Value;
                sessionSettings.LastName = _identity?.FindFirst(ClaimTypes.Surname)?.Value;
                sessionSettings.Login = _identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                sessionSettings.Email = _identity?.FindFirst(ClaimTypes.Email)?.Value;

                if (!_master)
                {
                    int.TryParse(_identity?.FindFirst(CustomClaimTypes.USERID)?.Value, out int _userId);
                    if (_userId > 0)
                    {
                        sessionSettings.UserId = _userId;                        
                        var _validUntil = _identity?.FindFirst(CustomClaimTypes.VALIDUNTIL)?.Value;
                        if (!string.IsNullOrEmpty(_validUntil))
                            sessionSettings.ValidUntil = DateTime.ParseExact(_validUntil, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                        throw new UnauthorizedException(ExceptionMessages.ERR0015);
                }
                await _next(context);
            }
        }
    }
}
