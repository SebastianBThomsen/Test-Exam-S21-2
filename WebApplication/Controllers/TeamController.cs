using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using WebApplication.DataAccess;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private Context _context;

        public TeamController()
        {
            _context = new Context();
        }
        
        [HttpPost]
        public async Task<ActionResult<Team>> PostAddAsync([FromBody] Team team)
        {
            Console.WriteLine($"Attempting to put {team} in Database");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                EntityEntry<Team> returnTeam = await _context.Teams.AddAsync(team);
                await _context.SaveChangesAsync();
                return Ok(returnTeam.Entity);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        
        [HttpGet]
        public async Task<ActionResult<IList<Team>>> GetAsync([FromQuery] int rank, [FromQuery] string name)
        {
            IList<Team> teams;
            try
            {
                teams = await getTeam(rank, name);
                return Ok(teams);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<IList<Team>> getTeam(int rank, string name)
        {
            IList<Team> teams;
            
            if(rank == 0 && name == null)
            {
                teams = await _context.Teams.Include(t=>t.Players).ToListAsync();
            }
            else if (rank == 0)
            {
                teams = await _context.Teams.Include(t=>t.Players).Where(t=>t.TeamName.Contains(name)).ToListAsync();
            }
            else if (name == null)
            {
                teams = await _context.Teams.Include(t=>t.Players).Where(t=>t.Ranking <= rank).ToListAsync();
            }
            else
            {
                teams = await _context.Teams.Include(t=>t.Players).Where(t=>t.Ranking <= rank).Where(t=>t.TeamName.Contains(name)).ToListAsync();
            }

            return teams;
        }
        
    }
}