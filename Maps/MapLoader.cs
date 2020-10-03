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
        private readonly IDictionary<int, Map> _maps = new Dictionary<int, Map>();

        public IReadOnlyDictionary<int, Map> LoadAll()
        {
            if (_maps.Count != 0)
            {
                return _maps.ToImmutableDictionary();
            }

            var mapFiles = Directory.GetFiles(Path.Combine("Content", "Maps"));

            foreach (var mapFile in mapFiles)
            {
                var mapText = File.ReadAllText(mapFile);
                var mapNumber = GetMapNumber(mapFile);
                var map = JsonConvert.DeserializeObject<Map>(mapText);
                _maps.Add(mapNumber, map);
            }

            return _maps.ToImmutableDictionary();
        }

        public Map LoadMap(int number)
        {
            LoadAll();

            if (_maps.ContainsKey(number))
            {
                return _maps[number];
            }

            throw new MapNotExistException();
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