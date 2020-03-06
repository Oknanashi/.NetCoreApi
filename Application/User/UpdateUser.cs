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
using Persistence;

namespace Application.User
{
    public class UpdateUser
    {
        public class Command : IRequest<AppUser>
        {
            public Guid CompanyId {get;set;}
            public Guid UserId {get;set;}
            
        }

        

        public class Handler : IRequestHandler<Command, AppUser>
        {
            
            
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<AppUser> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _context.AppUsers.FindAsync(request.UserId);

                if (user == null)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, new {Email="No such user exists"});
                }

                var company = await _context.Companies.FindAsync(request.CompanyId);
                
                user.EmployeeCompany = company;
                 try{
                     await _context.SaveChangesAsync();
                     return user;
                    
                 } catch{
                     throw new RestException(HttpStatusCode.BadRequest);
                 }

                
                throw new RestException(HttpStatusCode.Unauthorized);
            }
        }
    }
}