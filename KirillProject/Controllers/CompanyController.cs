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
using Application.Companies;

namespace KirillProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // [Authorize(Roles = "Admin,Manager")]
    [AllowAnonymous]
    
    public class CompanyController : BaseController
    {
        private readonly UserManager<AppEmployee> _userManager;

        private readonly DataContext _context;
        public CompanyController(DataContext context, UserManager<AppEmployee> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        //Implement Mediatr

        


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppCompany>>> Get()
        {

            var companies = await _context.Companies.ToListAsync();
            var list = JsonConvert.SerializeObject(companies,
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


        [HttpPost("createCompany")]
        [AllowAnonymous]
        public async Task<ActionResult<AppCompany>> CreateCompany(CreateCompany.Command command)
        {
            return await Mediator.Send(command);
        }

        // [HttpPost("updateCompany")]
        // [AllowAnonymous]
        // public async Task<ActionResult<AppCompany>> UpdateCompany(UpdateCompany.Command command)
        // {
        //     return await Mediator.Send(command);
        // }


        [HttpGet("testcompany")]
        public string TestCompany()
        {
            return "PostCheck";
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteCompany.Command { Id = id });
        }

        
    }
}