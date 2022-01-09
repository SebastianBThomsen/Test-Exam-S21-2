using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApplication.Models;

namespace BlazorApplication.Data
{
    public interface ITeamHandler
    {
        Task AddTeam(Team team);
        Task<IList<Team>> GetTeams();
    }
}