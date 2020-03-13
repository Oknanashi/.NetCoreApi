using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using Application.Employee;
using Application.User;
using Domain;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Persistence;

namespace KirillProject.Controllers
{
    [AllowAnonymous]
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
        
        public async Task<ActionResult<Employee>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

       
        [HttpPost("register")]
        
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

        [HttpPost("sendemail")]
        [AllowAnonymous]
        public string SendEmail(dynamic body){
            string msg = body.msg;
            string name = body.name;
            string email = body.email;
            string subject = body.Subject;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("belolok1@gmail.com"));
            message.To.Add(new MailboxAddress("oknanashi@gmail.com"));
            message.Subject = subject;
            message.Body = new TextPart("html"){
                Text = "From " + name+" at " +email+ "<br> " + msg
            };
            using (var client = new SmtpClient()){
                client.Connect("smtp.gmail.com",587);
                client.Authenticate("belolok1@gmail.com","dfcz11BN");
                client.Send(message);
                client.Disconnect(false);
            }
            return "Success";

        }

        [HttpGet("test")]
        [AllowAnonymous]
        public  string Test(){
            return "test";
        }


        
    }
}