using RetroRedo.Maps;

namespace RetroRedo
{
    public class GameStateService : IGameStateService
    {
        public int CurrentLevel { get; set; } = 1;
        public Map CurrentMap { get; set; }
        public int MapRefreshes { get; set; } = 0;
    }
}