using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<AppUser>
        {
            public Guid Id { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public int Age { get; set; }
            [Required]
            public string Bio { get; set; }


            public AppCompany Company { get; set; }
        }
        // public class CommandValidator :AbstractValidator<Command>{
        //     public CommandValidator(){
        //         RuleFor(x=>x.DisplayName).NotEmpty();
        //         RuleFor(x => x.Username).NotEmpty();
        //         RuleFor(x => x.Email).NotEmpty().EmailAddress();
        //         RuleFor(x => x.Password).Password();
        //     }
        // }
        public class Handler : IRequestHandler<Command, AppUser>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<AppUser> Handle(Command request, CancellationToken cancellationToken)
            {
                CultureInfo en = new CultureInfo("en-EN");
                var user = new AppUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Age = request.Age,
                    Bio = request.Bio,
                    CreatedAt=DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss",en)

                };
                var company = await _context.Companies.FindAsync(request.Company.Id);
                user.EmployeeCompany = company;
                try{
                    await _context.AppUsers.AddAsync(user);
                
                    await _context.SaveChangesAsync();
                    return user;
                }catch(Exception error){
                     throw new Exception("Problem saving cahnges",error);
                 }
                 
            }
        }
    }
}