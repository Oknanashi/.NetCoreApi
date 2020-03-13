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
using System;



namespace Application.Employee
{
    public class DeleteEmployee
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly UserManager<AppEmployee> _userManager;
            public Handler(UserManager<AppEmployee> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Id.ToString());

                if (user == null)
                    throw new Exception("Something went wrong");

                var result = await _userManager.DeleteAsync(user);

                
                if (result.Succeeded) return Unit.Value;
                throw new Exception("Problem deleting employee");
            }
        }
    }
}