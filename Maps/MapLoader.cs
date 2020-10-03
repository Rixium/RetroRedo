using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RetroRedo.Data;
using RetroRedo.Exceptions;

namespace RetroRedo.Maps
{
    internal class MapLoader : IMapLoader
    {
        private readonly IMapParser _mapParser;
        private IList<Map> _maps = new List<Map>();

        public MapLoader(IMapParser mapParser)
        {
            _mapParser = mapParser;
        }
        
        public IReadOnlyCollection<Map> LoadAll()
        {
            _maps = new List<Map>();
            
            var mapFiles = Directory.GetFiles(Path.Combine("Content", "Maps"));

            foreach (var mapFile in mapFiles)
            {
                var mapText = File.ReadAllText(mapFile);
                var mapNumber = GetMapNumber(mapFile);
                var tiledMap = JsonConvert.DeserializeObject<TiledMap>(mapText);
                tiledMap.MapId = mapNumber;
                
                var parsedMap = _mapParser.Parse(tiledMap);
                _maps.Add(parsedMap);
            }

            return _maps.ToImmutableList();
        }

        public Map LoadMap(int mapId)
        {
            LoadAll();
            var map = _maps.FirstOrDefault(x => x.Id == mapId);
            return map ?? throw new MapNotExistException();
        }

        private static int GetMapNumber(string mapFile)
        {
            var fileName = mapFile.Split(Path.DirectorySeparatorChar);
            var mapName = fileName.Last().Split('.').First();
            var mapNumber = mapName.Split("_");
            return int.Parse(mapNumber.Last());
        }
    }
}