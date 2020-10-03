using System.Collections.Generic;

namespace RetroRedo.Maps
{
    public interface IMapLoader
    {
        IReadOnlyCollection<Map> LoadAll();
        Map LoadMap(int mapId);
    }
}