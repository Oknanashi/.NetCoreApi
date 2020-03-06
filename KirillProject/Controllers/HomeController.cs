using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using Domain;
using Application.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KirillProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin,Manager")]
    // [AllowAnonymous]
    
    public class HomeController : BaseController
    {
        private readonly UserManager<AppEmployee> _userManager;

        private readonly DataContext _context;
        public HomeController(DataContext context, UserManager<AppEmployee> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        //Implement Mediatr

        


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> Get()
        {

            var users = await _context.AppUsers.ToListAsync();
            var list = JsonConvert.SerializeObject(users,
    Formatting.None,
    new JsonSerializerSettings()
    {
        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });
            return Ok(list);
        }
        // [HttpGet]
        // public async Task<ActionResult<User>> GetUsers()
        // {
        //     return await Mediator.Send(new CurrentUsers.Query());
        // }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AppUser>> RegisterUser(Register.Command command)
        {
            return await Mediator.Send(command);
        }
         [HttpPost("updateUser")]
        [AllowAnonymous]
        public async Task<ActionResult<AppUser>> UpdateCompany(UpdateUser.Command command)
        {
            
            return await Mediator.Send(command);
        }


        [HttpGet("testregister")]
        public string RegisterUser()
        {
            return "PostCheck";
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command { Id = id });
        }

        
    }
}