using Microsoft.AspNetCore.Authentication;
using System;

namespace BeConnectedApiGateway.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, string authenticationScheme, Action<CustomAuthOptions> configureOptions)
        {
            // Add custom authentication scheme with custom options and custom handler
            return builder.AddScheme<CustomAuthOptions, CustomAuthHandler>(authenticationScheme, configureOptions);
        }
    }
}
