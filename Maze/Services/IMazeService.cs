using Maze.Clases;

namespace Maze.Services
{
    public interface IMazeService
    {
        Task<GameDefinition> NewGame(int width, int height);
        Task<GameResponse> TakeLook(Guid mazeUid, Guid gameUid);
        Task<GameResponse> Play(Guid mazeUid, Guid gameUid, GameRequest.PlayerOperation operation);
        Task<GameDefinition> RestartGame(Guid mazeUid, Guid gameUid);
    }
}
