using Microsoft.AspNetCore.Http;
using DW.Company.Entities.Exceptions;
using DW.Company.Entities.Value;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace DW.Company.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var _httpResponse = context.Response;
                _httpResponse.ContentType = "application/json";

                switch (ex)
                {
                    case BadRequestException:
                        _httpResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case UnauthorizedException:
                        _httpResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case NotFoundException:
                        _httpResponse.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ForbiddenException:
                        _httpResponse.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    default:
                        _httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var _response = new Response
                {
                    Message = ex.Message,
                    Success = false,
                    StatusCode = _httpResponse.StatusCode,

                };
                var _responseJson = JsonSerializer.Serialize(_response, new(JsonSerializerDefaults.Web));
                await _httpResponse.WriteAsync(_responseJson);
            }
        }
    }
}
