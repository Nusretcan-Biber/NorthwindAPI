using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Utils.TokenFactory
{
    public class JWTTOkenFactory
    {
        private static readonly Lazy<JWTTOkenFactory> _instance = new Lazy<JWTTOkenFactory>(() => new JWTTOkenFactory());
        private JWTTOkenFactory()
        {
            securityKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        }
        public static JWTTOkenFactory Instance = _instance.Value;

        private string issuer
        {
            get
            {
                var issuer = Environment.GetEnvironmentVariable("TOKEN_ISSUER");
                if (string.IsNullOrEmpty(issuer))
                {
                    //throw new ArgumentNullException("TOKEN_SECURITY_KEY", "The environment variable TOKEN_SECURITY_KEY is not set or is empty.");
                    issuer = "Nusret";
                }
                return issuer;

            }
        }

        private string _key
        {
            get
            {
                //return Environment.GetEnvironmentVariable("TOKEN_SECURITY_KEY");
                var key = Environment.GetEnvironmentVariable("TOKEN_SECURITY_KEY");
                if (string.IsNullOrEmpty(key))
                {
                    //throw new ArgumentNullException("TOKEN_SECURITY_KEY", "The environment variable TOKEN_SECURITY_KEY is not set or is empty.");
                    key = "6e6f727468766964205665726920546162616ec4b1206a7774656b6c656d652061c59f616d616c6172c4b120626972617a2073c4b16bc4b16ec4b1206765c3a3796f722e";
                }
                return key;
            }
        }

        private SymmetricSecurityKey securityKey;
        private SigningCredentials signingCredentials;

        /// <summary>
        /// token oluşturur
        /// </summary>
        /// <param name="user">kullanıcı bilgilerini alır</param>
        /// <returns>token verisi döner geriye</returns>
        public string CreateToken(User userDto)
        {
            Claim[] claim = new[]
             {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Typ, "Bearer"),
                new Claim("name",$"{userDto.Name} {userDto.UserID}"),
                new Claim("usertype", userDto.UserTypeEnum.ToString())
            };

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                expires: DateTime.Now.AddDays(365),
                claims: claim,
                signingCredentials: signingCredentials);
            //JWT Güvenlik Belirteç token'i işlemek için
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        /// <summary>
        /// Token doğrular
        /// </summary>
        /// <param name="token">token alır</param>
        /// <returns>boolena değer döner</returns>
        public UserTypeEnum? IsTokenValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = securityKey,
                    ClockSkew = TimeSpan.FromDays(365)
                }, out SecurityToken validatedToken);

                UserTypeEnum userTypeEnum = Enum.Parse<UserTypeEnum>(claims.Claims.Where(x => x.Type.Equals("usertype")).FirstOrDefault().Value);
                return userTypeEnum;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
