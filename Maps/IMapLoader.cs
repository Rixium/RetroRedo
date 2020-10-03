using System.Collections.Generic;
using RetroRedo.Data;

namespace RetroRedo.Maps
{
    public interface IMapLoader
    {
        IReadOnlyCollection<Map> LoadAll();
    }
}