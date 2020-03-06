using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Companies
{
    public class CreateCompany
    {
        public class Command : IRequest<AppCompany>
        {
            [Required]
            public string CompanyName { get; set; }
            [Required]
            public string Address { get; set; }
            [Required]
            public string CompanySector { get; set; }
            
        }
        // public class CommandValidator :AbstractValidator<Command>{
        //     public CommandValidator(){
        //         RuleFor(x=>x.DisplayName).NotEmpty();
        //         RuleFor(x => x.Username).NotEmpty();
        //         RuleFor(x => x.Email).NotEmpty().EmailAddress();
        //         RuleFor(x => x.Password).Password();
        //     }
        // }
        public class Handler : IRequestHandler<Command, AppCompany>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<AppCompany> Handle(Command request, CancellationToken cancellationToken)
            {
                var company = new AppCompany
                {
                    Id = Guid.NewGuid(),
                    Address = request.Address,
                    CompanySector = request.CompanySector,
                    CompanyName = request.CompanyName,
                    

                };
                try{
                    var result = await _context.Companies.AddAsync(company);
                
                    await _context.SaveChangesAsync();
                    return company;
                }catch{
                     throw new Exception("Problem making user");
                 }
                 
            }
        }
    }
}