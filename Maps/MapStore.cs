using System.Collections.Generic;
using RetroRedo.Data;

namespace RetroRedo.Maps
{
    internal class MapStore : IMapStore
    {
        private readonly IMapLoader _mapLoader;

        private IDictionary<int, Map> _maps;

        public MapStore(IMapLoader mapLoader)
        {
            _mapLoader = mapLoader;
        }

        public Map GetMap(int map) => _maps[map];
    }
}