using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rovic_rating_app.Data;
using System.Xml.Linq;

namespace rovic_rating_app.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly UserManager<IdentityUser<int>> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public SetupController(DataContext dataContext, UserManager<IdentityUser<int>> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            this.dataContext = dataContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = roleManager.Roles.ToList();

            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            var roleExist = await roleManager.RoleExistsAsync(name);

            if (!roleExist)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole<int>(name));

                if(roleResult.Succeeded)
                {
                    return Ok("The role was added");
                }

                BadRequest("Role was not added");
            }

            return BadRequest("Role already exists");
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userManager.Users.ToListAsync();

            return Ok(users);   
        }

        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string role)
        {
            var userExist = await userManager.FindByEmailAsync(email);
            var roleExist = await roleManager.RoleExistsAsync(role);
            
            if(userExist == null)
            {
                return BadRequest("User does not exist");
            }

            if(!roleExist)
            {
                return BadRequest("Role does not exist");
            }

            var result = await userManager.AddToRoleAsync(userExist, role);

            if(result.Succeeded)
            {
                return Ok("Role was added to user");
            }
            else
            {
                return BadRequest("Role was not added to user");
            }
        }

        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var userExist = await userManager.FindByEmailAsync(email);

            if (userExist == null)
            {
                return BadRequest("User does not exist");
            }

            var roles = await userManager.GetRolesAsync(userExist);
            return Ok(roles);
        }

        [HttpPost("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string role)
        {
            var userExist = await userManager.FindByEmailAsync(email);

            if (userExist == null)
            {
                return BadRequest("User does not exist");
            }

            var roleExist = await roleManager.RoleExistsAsync(role);

            if (!roleExist)
            {
                return BadRequest("Role does not exist");
            }

            var result = await userManager.RemoveFromRoleAsync(userExist, role);

            if (result.Succeeded)
            {
                return Ok("Role was removed from user");
            }
            else
            {
                return BadRequest("Role was not removed from user");
            }
        }

    }
}
