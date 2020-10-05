using System.Collections.Generic;
using RetroRedo.Components;
using RetroRedo.Entities;

namespace RetroRedo.Screen
{
    public class MapEntityHistoryService
    {
        private readonly IList<IEntity> _historicalEntities = new List<IEntity>();

        public void AddEntities(IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                _historicalEntities.Add(entity);
                entity.AddComponent(new AutoCommandComponent());
            }
        }

        public IEnumerable<IEntity> GetHistoricalEntities() => _historicalEntities;
        public void Reset() => _historicalEntities.Clear();
    }
}