using Business;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BeConnectedApiGateway
{
    public static class Security
    {
        public static ApiResponseModel IsVerified(HttpContext ctx, IServiceProvider services)
        {
            var data = new ActivityLog();
            IActivityLog _activityLog = services.GetRequiredService<IActivityLog>();

            var response = new ApiResponseModel();
            var validations = new List<Validation>();
            var AuthToken = (ctx.Request.Headers.TryGetValue("AuthToken", out var authorizationToken)) ? authorizationToken.ToString() : string.Empty;

            data.AuthToken = AuthToken;
            data.IPAddress = ctx.Connection.RemoteIpAddress.ToString();
            data.UrlRequest = ctx.Request.Path.Value;
            data.BodyMessage = ctx.Request.Method;

            _activityLog.saveLog(data);

            if (string.IsNullOrEmpty(AuthToken))
            {
                response.StatusCode = 401;
                validations.Add(new Validation() { Key = "Token", Value = "dari api gateway" });
            }

            response.Validations = validations;

            return response;
        }
    }
}
