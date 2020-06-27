using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;

namespace BeConnectedApiGateway.Authentication
{
    public class CustomAuthOptions : AuthenticationSchemeOptions
    {
        public const string CustomAuth = "CustomgAuth";
        public string Scheme => CustomAuth;
        public StringValues AuthKey { get; set; }
    }
}
