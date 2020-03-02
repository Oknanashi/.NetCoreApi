using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Employee
{
    public class CurrentEmployee
    {
        public class Query : IRequest<Employee> { }

        public class Handler : IRequestHandler<Query, Employee>
        {

            private readonly UserManager<AppEmployee> _userManager;
            private readonly IUserAccessor _userAccessor;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<AppEmployee> userManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
            {
                _jwtGenerator = jwtGenerator;
                _userAccessor = userAccessor;
                _userManager = userManager;



            }

            public async Task<Employee> Handle(Query request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUsername());

                return new Employee{
                    
                    Username= user.UserName,
                    Token= _jwtGenerator.CreateToken(user),
                    Image=null,
                    Role = user.Role
                };
            }
        }
    }
}