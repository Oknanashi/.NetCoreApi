using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Persistence;

namespace Application.User
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.AppUsers.FindAsync(request.Id);

                if (user == null)
                    throw new Exception("Something went wrong");

                _context.Remove(user);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                throw new Exception("Problem saving changes");
            }
        }
    }
}