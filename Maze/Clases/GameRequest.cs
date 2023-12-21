namespace Maze.Clases
{
    public class GameRequest
    {
        public class GamePostRequest
        {
            public PlayerOperation Operation { get; set; }
        }

        public enum PlayerOperation
        {
            NotSet,
            Start,
            GoNorth,
            GoSouth,
            GoEast,
            GoWest
        }
    }
}
