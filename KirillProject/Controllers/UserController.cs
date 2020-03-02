using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using Application.Employee;
using Application.User;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace KirillProject.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<AppEmployee> _userManager;

        private readonly DataContext _context;
        public UserController(DataContext context, UserManager<AppEmployee> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<Employee>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

       
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<Employee>> Register(RegisterEmployee.Command command){
            
            return await Mediator.Send(command);
        }

        
        [HttpGet("currentUser")]
        [AllowAnonymous]
        public async Task<ActionResult<Employee>> CurrentUser(){
            return await Mediator.Send(new CurrentEmployee.Query());
        }

        
        [HttpGet("getEmployees")]
        [Authorize(Roles="Admin,Manager")]
        public async Task<List<AppEmployee>> GetEmployees(){
            var users = await  _userManager.Users.ToListAsync();
            return users;
        }

        [HttpPost("editEmployeeRole")]
        [Authorize(Roles="Admin,Manager")]
        public async Task<ActionResult<Employee>> EditEmployeeRole(EditEmployee.Query query){
            return await Mediator.Send(query);
        }

        
    }
}