using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RetroRedo.Data;

namespace RetroRedo.Maps
{
    internal class MapLoader
    {
        private IList<Map> _maps = new List<Map>();

        private void LoadAll()
        {
            _maps = new List<Map>();
            
            var mapFiles = Directory.GetFiles(Path.Combine("assets", "Maps"));

            foreach (var mapFile in mapFiles)
            {
                var mapText = File.ReadAllText(mapFile);
                var mapNumber = GetMapNumber(mapFile);
                var tiledMap = JsonConvert.DeserializeObject<TiledMap>(mapText);
                tiledMap.MapId = mapNumber;
                
                var parsedMap = MapParser.Parse(tiledMap);
                _maps.Add(parsedMap);
            }
        }

        public Map LoadMap(int mapId)
        {
            LoadAll();
            var map = _maps.FirstOrDefault(x => x.Id == mapId);
            return map;
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