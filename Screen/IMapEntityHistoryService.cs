using System.Collections.Generic;
using RetroRedo.Components;
using RetroRedo.Entities;

namespace RetroRedo.Screen
{
    public interface IMapEntityHistoryService
    {
        void AddEntities(IList<IEntity> entities);
        IList<IEntity> GetHistoricalEntities();
        void Reset();
    }

    public class MapEntityHistoryService : IMapEntityHistoryService
    {
        private readonly IList<IEntity> _historicalEntities = new List<IEntity>();

        public void AddEntities(IList<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                _historicalEntities.Add(entity);
                entity.AddComponent(new AutoCommandComponent());
            }
        }

        public IList<IEntity> GetHistoricalEntities() => _historicalEntities;
        public void Reset() => _historicalEntities.Clear();
    }
}