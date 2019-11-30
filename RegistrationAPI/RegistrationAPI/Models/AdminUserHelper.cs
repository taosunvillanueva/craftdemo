namespace RegistrationAPI.Models
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    public class AdminUserHelper
    {
        public static AdminUser ConertToAdminUserDatabaseEntry(JObject adminTempJson)
        {
            var adminTemp = adminTempJson.ToObject<AdminUserSimple>();

            var adminUser = new AdminUser()
            {
                Username = adminTemp.Username,
                Password = EncryptRawPasswordToHash(adminTemp.Password)
            };

            adminUser.CreateId();

            return adminUser;
        }

        private static byte[] EncryptRawPasswordToHash(string rawPassword)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(rawPassword);
            var hashAlgorithm = new System.Security.Cryptography.SHA256Managed();
            var hash = hashAlgorithm.ComputeHash(data);

            return hash;
        }

        public static JObject CreateToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;

            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(
                        issuer: "https://registrationapi20191122063201.azurewebsites.net", audience: "https://registrationapi20191122063201.azurewebsites.net",
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);

            var jObject = new JObject();
            var jProperty = new JProperty("token", tokenString);
            jObject.Add(jProperty);

            return jObject;
        }
    }
}