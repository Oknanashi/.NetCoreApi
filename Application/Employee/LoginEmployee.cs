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
    public class Login
    {
        public class Query : IRequest<Application.Employee.Employee>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, Application.Employee.Employee>
        {
            private readonly UserManager<AppEmployee> _userManager;
            private readonly SignInManager<AppEmployee> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<AppEmployee> userManager, SignInManager<AppEmployee> signInManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _signInManager = signInManager;
                _userManager = userManager;
            }

            public async Task<Application.Employee.Employee> Handle(Query request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, new {Email="No such user exists"});
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                  
                    return new Application.Employee.Employee
                    {
                        
                        Token =  _jwtGenerator.CreateToken(user),
                        Image = null,
                        Username = user.UserName,
                        // Role = user.Role
                        // Image = user.Photos.FirstOrDefault(x=>x.IsMain)?.Url
                    };
                } 
                throw new RestException(HttpStatusCode.Unauthorized);
            }
        }
    }
}