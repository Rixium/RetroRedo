using System.Collections.Generic;
using RetroRedo.Data;

namespace RetroRedo.Maps
{
    public interface IMapLoader
    {
        IReadOnlyDictionary<int, TiledMap> LoadAll();
        TiledMap LoadMap(int number);
    }
}