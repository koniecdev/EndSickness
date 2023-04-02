using IdentityModel;
using IdentityServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services {
	public class ProfileService : IProfileService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public ProfileService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var user = await _userManager.GetUserAsync(context.Subject);
			var claims = new List<Claim>
			{
				new Claim("Id", user.Id),
				new Claim("Email", user.Email),
				new Claim("Name", user.UserName)
			};
			var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
			foreach(var userRole in userRoles)
			{
				claims.Add(new Claim(JwtClaimTypes.Role, userRole));
			}

			context.IssuedClaims.AddRange(claims);
		}

		public async Task IsActiveAsync(IsActiveContext context)
		{
			var user = await _userManager.GetUserAsync(context.Subject);
			context.IsActive = user != null;
		}
	}
}
