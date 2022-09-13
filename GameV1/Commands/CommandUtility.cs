
using MooseEngine.Interfaces;
using MooseEngine.Utilities;
using System.Numerics;

namespace GameV1.Commands
{
    public static class CommandUtility
    {
        // TODO: Move these to Scene?

        public static Vector2 GetRandomValidPosition(IDictionary<Vector2, IEntity> entities)
        {
            return entities.ElementAt(Randomizer.RandomInt(0, entities.Count - 1)).Value.Position;
        }

        public static Vector2 GetClosestValidPosition(IDictionary<Vector2, IEntity> entities, Vector2 targetPosition)
        {
            Vector2 closestValidPosition = Vector2.Zero;
            float tempDist = Vector2.DistanceSquared(closestValidPosition, targetPosition);

            foreach (var pos in entities.Keys)
            {
                var distanceToTarget = Vector2.DistanceSquared(pos, targetPosition);

                if (distanceToTarget < tempDist)
                {
                    tempDist = distanceToTarget;
                    closestValidPosition = pos;
                }

                //Math.Abs(targetPosition.X - pos.X) < Math.Abs(closestValidPosition.X - pos.X) &&
                //Math.Abs(targetPosition.Y - pos.Y) < Math.Abs(closestValidPosition.Y - pos.Y))

            }

            return closestValidPosition;
        }
    }
}
