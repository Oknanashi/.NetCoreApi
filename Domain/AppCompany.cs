using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AppCompany
    {
        // [Key]
        [Column("CompanyId")]
        public Guid Id { get; set; }

        public string CompanyName { get; set; }
        
        public string Address {get;set;}
        public string CompanySector {get;set;}
        public  virtual ICollection<AppUser> AppUsers { get; set; }
    }
}