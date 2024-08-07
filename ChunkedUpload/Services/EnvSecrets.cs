using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChunkedUpload.Services
{
    public class EnvSecrets
    {
        public string JwtKey { get; set; }   
        public string JwtIssuer { get; set; }

        public EnvSecrets()
        {
            var jwtKey = Environment.GetEnvironmentVariable("X1_JwtKey", EnvironmentVariableTarget.User);
            var jwtIssuer = Environment.GetEnvironmentVariable("X1_JwtIssuer", EnvironmentVariableTarget.User);

            if (string.IsNullOrEmpty(jwtKey)) throw new ArgumentException($"JwtKey is missing from the Environment");
            else JwtKey = jwtKey;
            if (string.IsNullOrEmpty(jwtIssuer)) throw new ArgumentException($"JwtIssuer is missing from the Environment");
            else JwtIssuer = jwtIssuer;
        }
    }
}
