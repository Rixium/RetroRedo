using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RetroRedo.Data;

namespace RetroRedo.Maps
{
    internal class MapLoader : IMapLoader
    {
        public IReadOnlyCollection<Map> LoadAll()
        {
            var maps = new List<Map>();
            
            var mapFiles = Directory.GetFiles(Path.Combine("Content", "Maps"));
            foreach (var file in mapFiles)
            {
                var mapData = File.ReadAllText(file);
                var map = JsonConvert.DeserializeObject<Map>(mapData);
                maps.Add(map);
            }
            
            return maps;
        }
    }
}