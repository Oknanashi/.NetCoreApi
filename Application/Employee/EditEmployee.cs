using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Errors;
using System.Net;
using Application.Interfaces;
using System.Linq;

namespace Application.User
{
    public class EditEmployee
    {
        public class Query : IRequest<Application.Employee.Employee>
        {
            public string Role { get; set; }
            public string Email { get; set; }
            
        }

        

        public class Handler : IRequestHandler<Query, Application.Employee.Employee>
        {
            private readonly UserManager<AppEmployee> _userManager;
            
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<AppEmployee> userManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                
                _userManager = userManager;
            }

            public async Task<Application.Employee.Employee> Handle(Query request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, new {Email="No such user exists"});
                }

                user.Role = request.Role;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                  
                    return new Application.Employee.Employee
                    {
                        
                        Token =  _jwtGenerator.CreateToken(user),
                        Image = null,
                        Username = user.UserName,
                        Role = user.Role
                        // Image = user.Photos.FirstOrDefault(x=>x.IsMain)?.Url
                    };
                } 
                throw new RestException(HttpStatusCode.Unauthorized);
            }
        }
    }
}