using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Company
    {
        // [Key]
        [Column("CompanyId")]
        public Guid Id { get; set; }

        public string CompanyName { get; set; }
        
        
        public  virtual ICollection<AppUser> AppUsers { get; set; }
    }
}