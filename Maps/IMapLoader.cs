using System.Collections.Generic;
using RetroRedo.Data;

namespace RetroRedo.Maps
{
    public interface IMapLoader
    {
        IReadOnlyDictionary<int, Map> LoadAll();
        Map LoadMap(int number);
    }
}