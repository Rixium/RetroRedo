using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RetroRedo.Data;

namespace RetroRedo.Maps
{
    internal class MapLoader : IMapLoader
    {
        public IReadOnlyCollection<Map> LoadAll()
        {
            var mapFiles = Directory.GetFiles(Path.Combine("Content", "Maps"));
            return mapFiles.Select(File.ReadAllText)
                .Select(JsonConvert.DeserializeObject<Map>).ToList();
        }
    }
}