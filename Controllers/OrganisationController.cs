using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrExtApiCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrExtApiCore.Repositories;
using Entities;
using Microsoft.AspNetCore.Cors;

namespace CrExtApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Organisation")]
    [EnableCors("AllowOrigin")]
    public class OrganisationController : Controller
    {
        private readonly IOrganisationAsync _organisation;
        private readonly IUserAsync _userAsync;
        public OrganisationController(IOrganisationAsync organisation, IUserAsync userAsync)
        {
            _organisation = organisation;
            _userAsync = userAsync;
        }

        [HttpGet("{id}", Name ="GetOrg")]
        public async Task<IActionResult> Get(int id)
        {
            if (!await _organisation.Find(id)) return NotFound();
            var org = await _organisation.Get(id);
            return Ok(org);
        }

        [HttpGet("GetUserOrganisation/{id}", Name = "GetUserOrg")]
        public async Task<IActionResult> GetUserOrg(string id)
        {
            if (!await _userAsync.Find(id)) return NotFound();
            var org = await _organisation.GetUserOranisaton(id);
            return Ok(org);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var org = await _organisation.List();
            return Ok(org);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] OrganisationDto org)
        {
          if(org == null) return NotFound();
          if (ModelState.IsValid)
          {
            
              await _organisation.Create(org);
                if (await _organisation.Save())
                {
                    var reqestedOrg = Mapper.Map<Organisations>(org);
                    var savedOrg = Mapper.Map<OrganisationDto>(reqestedOrg);
                    return CreatedAtRoute("GetOrg", new { id= savedOrg.Id }, savedOrg);
                }
                else {
                    return StatusCode(500, "Server Error,");
                } 

             
          }
          return BadRequest(ModelState);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> CreateAddUser([FromBody] CreateUserOrganisationDto m)
        {
            if (m == null) return NotFound();
            var check = await _organisation.UserBelongsToOrganisation(m.UserId, m.OrganisationId);
            if(check) return BadRequest("User Already Created");

            if (ModelState.IsValid)
            {
                await _organisation.AddUser(m);
                if (await _organisation.Save())
                {
                    var reqestedOrg = Mapper.Map<UserOrganisation>(m);
                    var savedOrg = Mapper.Map<CreateUserOrganisationDto>(reqestedOrg);
                    return CreatedAtRoute("GetUserOrg", new { id = savedOrg.Id }, savedOrg);
                }
                else
                {
                    return StatusCode(500, "Server Error,");
                }

            }
            return BadRequest(ModelState);
        }


    }
}