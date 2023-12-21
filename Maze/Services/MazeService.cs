using Maze.Clases;
using System.Net.Http;
using System;
using System.Net.Http.Json;

namespace Maze.Services
{
    public class MazeService : IMazeService
    {
        private readonly HttpClient _httpClient;
        private readonly string code = "?code=CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==";

        public MazeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GameDefinition> NewGame(int width, int height)
        {
            // nuevo maze
            var response = await _httpClient.PostAsJsonAsync($"Maze{code}", new Maze.Clases.Maze() { Height = height, Width = width });
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.RequestMessage?.ToString());

            var maze = await response.Content.ReadFromJsonAsync<Maze.Clases.Maze>();

            // nuevo game
            response = await _httpClient.PostAsJsonAsync($"Game/{maze.MazeUid}{code}", new GameRequest.GamePostRequest { Operation = GameRequest.PlayerOperation.Start });
            var gameDefinition = await response.Content.ReadFromJsonAsync<GameDefinition>();
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.RequestMessage?.ToString());

            return gameDefinition;
        }

        public async Task<GameResponse> Play(Guid mazeUid, Guid gameUid, GameRequest.PlayerOperation operation)
        {
            var response = await _httpClient.PostAsJsonAsync($"Game/{mazeUid}/{gameUid}{code}", new GameRequest.GamePostRequest { Operation = operation });
            var gameResponse = await response.Content.ReadFromJsonAsync<GameResponse>();
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.RequestMessage?.ToString());

            return gameResponse;
        }

        public async Task<GameResponse> TakeLook(Guid mazeUid, Guid gameUid)
        {
            var response = await _httpClient.GetAsync($"Game/{mazeUid}/{gameUid}{code}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.RequestMessage?.ToString());
            var look = await response.Content.ReadFromJsonAsync<GameResponse>();
            return look;
        }

        public async Task<GameDefinition> RestartGame (Guid mazeUid, Guid gameUid)
        {
            var response = await _httpClient.PostAsJsonAsync($"Game/{mazeUid}/{gameUid}{code}", new GameRequest.GamePostRequest { Operation = GameRequest.PlayerOperation.Start });
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.RequestMessage?.ToString());
            var gameDefinition = await response.Content.ReadFromJsonAsync<GameDefinition>();
            return gameDefinition;
        }
    }
}
