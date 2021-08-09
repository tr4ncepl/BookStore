using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShop.WebUI.Models
{
    public class CreateModel
    {
        [Required]
        [Display(Name="Login")]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }

        
        [Required]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }

    public class CreateCustomerModel
    {
        [Required]
        public string Name { get; set; }

        
        public string LastName { get; set; }

        
        public string Address { get; set; }

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string password { get; set; }

        public string email { get; set; }

    }
}