using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser 
    {
        [Column("AppUserId")]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public int Age { get; set; }
        public string Bio { get; set; }
        public string CreatedAt { get; set; }

        
        
        public virtual Company EmployeeCompany { get; set; }
        
        
    }
}
