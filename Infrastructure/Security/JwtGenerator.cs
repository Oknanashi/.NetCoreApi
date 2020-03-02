using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Infrastructure.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly UserManager<AppEmployee> _userManager;

        public JwtGenerator(UserManager<AppEmployee> userManager)
        {
            _userManager = userManager;

        }


        public string CreateToken(AppEmployee employee)
        {
                var role = _userManager.GetRolesAsync(employee);
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name ,employee.Id),
                new Claim(ClaimTypes.Role, employee.Role)
            };

            //generate signing credentioaals
            

            var key = Encoding.ASCII.GetBytes("superpupersecret");
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);


        }
    }
}