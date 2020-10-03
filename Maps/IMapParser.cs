using RetroRedo.Data;

namespace RetroRedo.Maps
{
    internal interface IMapParser
    {
        Map Parse(TiledMap tiledMap);
    }
    
}