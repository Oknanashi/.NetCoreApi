using System;
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
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string Bio { get; set; }
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
                var user = new AppUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Age = request.Age,
                    Bio = request.Bio,
                    CreatedAt=DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")

                };
                try{
                    var result = await _context.AppUsers.AddAsync(user);
                
                    await _context.SaveChangesAsync();
                    return user;
                }catch{
                     throw new Exception("Problem making user");
                 }
                 
            }
        }
    }
}