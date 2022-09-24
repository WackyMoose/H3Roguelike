using GameV1.Entities.Creatures;
using GameV1.Interfaces.Creatures;
using MooseEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities.Factories
{
    public static class EntityFactory
    {
        public static TEntity? CreateEntity<TEntity>(IEntityLayer entityLayer, string name, Vector2 position) where TEntity : class, IEntity, new()
        {
            TEntity? newEntity = entityLayer.ActivateOrCreateEntity<TEntity>(position);

            newEntity.Name = name;


            return newEntity;
        }
    }
}
