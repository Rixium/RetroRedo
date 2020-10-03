using System.Collections.Generic;
using RetroRedo.Entities;

namespace RetroRedo.Screen
{
    public interface IMapEntityHistoryService
    {
        void AddEntities(IList<IEntity> entities);
        IList<IEntity> GetHistoricalEntities();
    }

    public class MapEntityHistoryService : IMapEntityHistoryService
    {
        private readonly IList<IEntity> _historicalEntities = new List<IEntity>();

        public void AddEntities(IList<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                _historicalEntities.Add(entity);
            }
        }

        public IList<IEntity> GetHistoricalEntities() => _historicalEntities;
    }
}