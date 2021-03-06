using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Concrete
{
    class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string pass)
        {
            IdentityResult result = await base.ValidateAsync(pass);
            if(pass.Contains("12345"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Hasło nie może zawierać liczbowych kombinacji");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}
