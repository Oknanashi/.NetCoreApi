using System;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public enum Role
    {
        Admin,
        Manager,
        Member
    }
    public class AppEmployee : IdentityUser
    {

        public Guid AppEmployeeId { get; set; }
        public string DisplayName { get; set; }
        public string Role {get;set;}




        // public virtual ICollection<Photo> Photos { get; set; }


    }
}