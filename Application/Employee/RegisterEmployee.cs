using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Employee
{
    public class RegisterEmployee
    {
        public class Command : IRequest<Employee>
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, Employee>
        {
            private readonly DataContext _context;
            private readonly UserManager<AppEmployee> _userManager;

            private readonly IJwtGenerator _jwtGenerator;
            public Handler(DataContext context, UserManager<AppEmployee> userManager, IJwtGenerator jwtGenerator)
            {
                _jwtGenerator = jwtGenerator;
                _userManager = userManager;
                _context = context;

            }

            public async Task<Employee> Handle(Command request, CancellationToken cancellationToken)
            {




                if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { EmailTokenProvider = "Email already exists" });
                }

                if (await _context.Users.Where(x => x.UserName == request.Username).AnyAsync())
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "UserName already exists" });
                }

                var employee = new AppEmployee
                {

                    Email = request.Email,
                    UserName = request.Username,
                    Role = "Member"

                };




                var powerUser = await _userManager.CreateAsync(employee, request.Password);


                             // Console.WriteLine(userResult);

                if (powerUser.Succeeded)
                {

                    return new Employee
                    {

                        Token = _jwtGenerator.CreateToken(employee),
                        Username = employee.UserName,
                        Image = null,
                        // Role = Role.Member.ToString()
                    };
                }
                throw new Exception("Problem saving changes");
            }
        }
    }
}