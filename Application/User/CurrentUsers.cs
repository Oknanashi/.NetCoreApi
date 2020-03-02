// using System.Threading;
// using System.Threading.Tasks;
// using Application.Interfaces;
// using Domain;
// using MediatR;
// using Microsoft.AspNetCore.Identity;

// namespace Application.User
// {
//     public class CurrentUsers
//     {
//         public class Query : IRequest<User>{}

//         public class Handler : IRequsetHandler<Query,User>{
//             private readonly IUserAccessor _userAccessor;
//             private readonly <AppUser> _userManager;

//             public Handler(UserManager<AppUser> userManager,IUserAccessor userAccessor){
//                 _userAccessor = userAccessor;
//                 _userManager = userManager;
//             }

//             public async Task<User> Handle(Query request,CancellationToken cancellationToken){
//                 var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
//                 return new User{
//                     DisplayName=user.F
//                 }
//             }
//         }
//     }
// }