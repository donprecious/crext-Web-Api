using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrExtApiCore.Models;
using CrExtApiCore.Repositories;
using Microsoft.AspNetCore.Cors;
using AutoMapper;
using Entities;

namespace CrExtApiCore.Controllers
{
    [Produces("application/json")]
    [Route("api/TeamMember")]
    [EnableCors("AllowOrigin")]
    public class TeamMemberController : Controller
    {
        private ITeamMember _teamMember;

        private CrExtContext _context;

        public TeamMemberController(ITeamMember teamMember, CrExtContext context)
        {
            _teamMember = teamMember;
            _context = context;
        }
        [HttpGet("{id}", Name = "GetTeamMember")]
        public async Task<IActionResult> Get(int id)
        {
            if (!await _teamMember.Find(id)) return NotFound("Team not Found");
            var teamMember = await _teamMember.Get(id);
            return Ok(teamMember);
        }


        [HttpGet("GetOrganisationTeamMembers/{orgId}")]
        public async Task<IActionResult> GetOrganisationTeamMembers(int orgId)
        {
            var members = await _teamMember.GetOrganisationTeamMembers(orgId);
            return Ok(members);
        }


        [HttpGet("GetTeamMemberId/{userid}")]
        public async Task<IActionResult> Get(string userid)
        {
            
            var id = await _teamMember.GetTeamMemberId(userid);
            return Ok(id);

        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateTeamMemberDto teamMemberDto)
        {
            if (teamMemberDto == null) return NotFound();
            if (ModelState.IsValid)
            {
                var findUser = await _teamMember.FindTeamMember(teamMemberDto.UserId);

                if (findUser) return BadRequest("User Belongs to team already");
                var mappedTeam = Mapper.Map<TeamMembers>(teamMemberDto);
                await _teamMember.Create(mappedTeam);
                if (!await _teamMember.Save())
                {
                    return StatusCode(500, "Server Error, Something went wrong with our server");
                }
                var created = Mapper.Map<TeamMemberDto>(mappedTeam);
                return CreatedAtRoute("GetTeamMember", new { id = created.Id }, created);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet("List")]
        public async Task<IActionResult> List(string userid)
        {

            var list = await _teamMember.List();
            return Ok(list);

        }
    }
}