using System.Threading.Tasks;
using BlazorApplication.Models;

namespace BlazorApplication.Data
{
    public interface IPlayerHandler
    {
        Task AddPlayer(Player player, string teamName);
        Task DeletePlayer(int playerId);
    }
}