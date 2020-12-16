using System.Security.Claims;
using System.Threading.Tasks;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace BookStoreWeb.Helper
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,IOptions<IdentityOptions> options):base(userManager,roleManager,options)
        {
            
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserFirstName",user.FirstName??"fr"));
            identity.AddClaim(new Claim("UserLastName",user.LastName??"ls"));
            return identity;
        }
    }
}