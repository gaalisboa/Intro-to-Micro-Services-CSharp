using System.Security.Claims;
using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySqlContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(
            RoleManager<IdentityRole> role,
            UserManager<ApplicationUser> user,
            MySqlContext context)
        {
            _role = role;
            _user = user;
            _context = context;
        }

        public void Initialize()
        {
            if (
                _role.FindByNameAsync(IdentityConfiguration.Admin).Result != null)
                return;

            _role.CreateAsync(new IdentityRole(
                IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(
                IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "gabriel-admin",
                Email = "gabriel-admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (12) 91234-5678",
                FirstName = "Gabriel",
                LastName = "Admin"
            };

            _user.CreateAsync(admin, "Gabriel123-")
                .GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin)
                .GetAwaiter().GetResult();

            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                UserName = "gabriel-client",
                Email = "gabriel-client@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (12) 91234-5678",
                FirstName = "Gabriel",
                LastName = "Client"
            };

            _user.CreateAsync(client, "Gabriel123-")
                .GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client)
                .GetAwaiter().GetResult();

            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;
        }
    }
}
