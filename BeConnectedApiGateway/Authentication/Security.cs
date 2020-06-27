using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BeConnectedApiGateway
{
    public static class Security
    {
        public static ApiResponseModel IsVerified(HttpContext ctx)
        {
            var response = new ApiResponseModel();
            var validations = new List<Validation>();
            var AuthToken = (ctx.Request.Headers.TryGetValue("AuthToken", out var authorizationToken)) ? authorizationToken.ToString() : string.Empty;

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
