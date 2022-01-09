using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApplication.Models;

namespace BlazorApplication.Data.Impl
{
    public class PlayerHandler : IPlayerHandler
    {
        private readonly string URL = "https://localhost:5001";

        public async Task AddPlayer(Player player, string teamName)
        {
            using HttpClient client = new();

            string playerAsJson = JsonSerializer.Serialize(player);

            Console.WriteLine(player);
            StringContent content = new StringContent(
                playerAsJson,
                Encoding.UTF8,
                "application/json"
            );
            Console.WriteLine($"Before responseMessage: {player}");
            HttpResponseMessage responseMessage = await client.PostAsync($"{URL}/Player/{teamName}", content);
            Console.WriteLine($"After responseMessage: {player}");
            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
        }

        public async Task DeletePlayer(int playerId)
        {
            using HttpClient client = new();
            HttpResponseMessage responseMessage = await client.DeleteAsync($"{URL}/Player/{playerId}");
            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
        }
    }
}