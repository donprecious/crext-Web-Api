using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrExtApiCore.Models;
using CrExtApiCore.Repositories;
using Entities;
using Microsoft.AspNetCore.Cors;

namespace CrExtApiCore.Controllers
{
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserAsync _user;
        private readonly IRoleAsync _role;
        public UserController(IUserAsync user, IRoleAsync role)
        {
            _user = user;
            _role = role;
        }

        [HttpGet("List")]
        public async Task<IActionResult> list()
        {
            var user = await _user.List();
            return Ok(user);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            if (!await _user.Find(id)) return NotFound(id + "Not Found");
            var user = await _user.User(id);
            return Ok(user);
        }

        [HttpGet("GetUserByEmail/{email}", Name = "GetEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (email == null) return BadRequest("invalid email");

            var user = await _user.GetUserByEmail(email);
            if (user == null) return NotFound("User Not Found");
            return Ok(user);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserDto user)
        {
            if (user == null) return NotFound();
            if (ModelState.IsValid)
            {
                var mappedUser = Mapper.Map<Users>(user);

                ;
                //
                if (!await _user.Create(user))
                {
                    return StatusCode(500, "Server Error, Something went wrong with our server");
                }
                var createdUser = Mapper.Map<Users>(mappedUser);

                var route = CreatedAtRoute("Get", new { id = createdUser.Id }, createdUser);
                return route;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            if (createRoleDto == null) return BadRequest("No Role Name Present");
            if (ModelState.IsValid)
            {
                var add = await _role.CreateRole(createRoleDto.Name);

                if (add)
                {
                    var role = await _role.GetRoleByName(createRoleDto.Name);
                 return CreatedAtRoute("GetRole", new { id = role.Id }, role);
                }
                //
                return StatusCode(500, "Server Error, Something went wrong with our server");

            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost("AddToRole")]
        public async Task<IActionResult> CreateAddToRole([FromBody] UserRoleDto userToRole)
        {
            if (userToRole == null) return NotFound();
            if (ModelState.IsValid)
            {
                var add = await _user.AddRole(userToRole.Id, userToRole.RoleName);
                if (add)
                {
                    return Ok("Role Removed");
                }
                //
                return StatusCode(500, "Server Error, Something went wrong with our server");

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("RemoveFromRole")]
        public async Task<IActionResult> CreateRemoveFromRole([FromBody] UserRoleDto userToRole)
        {
            if (userToRole == null) return NotFound();
            if (ModelState.IsValid)
            {
                var add = await _user.RemoveRole(userToRole.Id, userToRole.RoleName);
                if (add)
                {
                    return CreatedAtRoute("GetUserRoles", new { id = userToRole.Id }, userToRole);
                }
                //
                return StatusCode(500, "Server Error, Something went wrong with our server");

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("UserRoles/{id}", Name ="GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string id)
        {
            if (id == null) return BadRequest("user id not present");

            if (!await _user.Find(id)) return NotFound("User Not Found");

            var roles = await _role.UserRoles(id);
            return Ok(roles);
        }

        [HttpGet("GetAllRoles", Name = "GetRoles")]
        public async Task<IActionResult> GetAllRoles()
        {

            var roles = await _role.List();
            return Ok(roles);
        }

        [HttpGet("FindRole/{id}", Name = "FindRole")]
        public async Task<IActionResult> FindRole(string id)
        {
            if (id == null) return BadRequest("role id not present");

           
            var role =  await _role.Find(id);
            return Ok(role);
           
        }

        [HttpGet("FindRoleByName/{name}", Name = "FindRoleByName")]
        public async Task<IActionResult> FindRoleByName(string name)
        {
            if (name == null) return BadRequest("role name not present");

      
            var role = await _role.FindByName(name);
            return Ok(role);

        }

        [HttpGet("GetRole/{id}", Name = "GetRole")]
        public async Task<IActionResult> GetRole(string id)
        {
            if (id == null) return BadRequest("role id not present");

            if (!await _role.Find(id)) return NotFound("Role Not Found");

            var roles = await _role.GetRoleAsync(id);
            return Ok(roles);
        }


        [HttpGet("GetRoleByName/{name}", Name = "GetRolesByName")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            if (name== null) return BadRequest("role name not present");

            if (!await _role.FindByName(name)) return NotFound("Role Not Found");

            var roles = await _role.GetRoleByName(name);
            return Ok(roles);
        }


    

    }
}