using RetroRedo.Maps;

namespace RetroRedo
{
    public interface IGameStateService
    {
        int CurrentLevel { get; set; }
        Map CurrentMap { get; set; }
        int MapRefreshes { get; set; }
    }
}