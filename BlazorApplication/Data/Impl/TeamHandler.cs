using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApplication.Models;

namespace BlazorApplication.Data.Impl
{
    public class TeamHandler : ITeamHandler
    {
        private readonly string URL = "https://localhost:5001";


        public async Task AddTeam(Team team)
        {
            using HttpClient client = new();

            string teamAsJson = JsonSerializer.Serialize(team);

            StringContent content = new StringContent(
                teamAsJson,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage responseMessage = await client.PostAsync($"{URL}/Team", content);
            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
        }

        public async Task<IList<Team>> GetTeams()
        {
            using HttpClient client = new();

            HttpResponseMessage responseMessage = await client.GetAsync($"{URL}/Team");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"{responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");

            string result = await responseMessage.Content.ReadAsStringAsync();

            IList<Team> teams = JsonSerializer.Deserialize<IList<Team>>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return teams;
        }
    }
}