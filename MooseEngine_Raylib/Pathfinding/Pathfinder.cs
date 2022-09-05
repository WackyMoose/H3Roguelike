using System.Numerics;
using MooseEngine.Utilities;

namespace MooseEngine.Pathfinding
{
    public class Pathfinder
    {
        private Dictionary<Vector2, PathNode> _openSet = new Dictionary<Vector2, PathNode>();
        private Dictionary<Vector2, PathNode> _closedSet = new Dictionary<Vector2, PathNode>();
        private List<PathNode> _neighbours = new List<PathNode>();
        private PathNode _lastNode;

        //TODO Generate map with nodes to be used instead of the currently used walkable tiles...
        public PathNode[] GetPath(Vector2 startPos, Vector2 goalPos, PathMap map)
        {
            _openSet.Clear();
            _closedSet.Clear();

            var start = startPos * Constants.DEFAULT_ENTITY_SIZE;
            var goal = goalPos * Constants.DEFAULT_ENTITY_SIZE;

            var startNode = new PathNode(map.Map[startPos].NodePosition, null);
            startNode.G = Vector2.DistanceSquared(startPos, startPos);
            startNode.H = Vector2.DistanceSquared(goalPos, startPos);
            var endNode = new PathNode(map.Map[goalPos].NodePosition, null);

            _openSet.Add(startNode.Position, startNode);
            _lastNode = startNode;

            while (_openSet.Count > 0)
            {
                var currentNode = new PathNode(new Vector2(0, 0), null);
                currentNode.G = int.MaxValue;

                foreach (var node in _openSet)
                {
                    if (node.Value.F < currentNode.F)
                    {
                        currentNode = node.Value;
                    }

                }

                _closedSet.Add(currentNode.Position, currentNode);
                _openSet.Remove(currentNode.Position);

                if (currentNode.Position == endNode.Position)
                {
                    var temp = new List<PathNode>();
                    var tempNode = currentNode;
                    while (tempNode.Position != startNode.Position)
                    {
                        temp.Add(tempNode);
                        tempNode = tempNode.Parent;
                    }

                    return temp.ToArray();
                }

                _neighbours.Clear();

                foreach (var dir in CardinalDirectionList)
                {
                    var node = new PathNode(dir + currentNode.Position, currentNode);

                    _neighbours.Add(node);
                }

                foreach (var node in _neighbours)
                {
                    if (map.Map.ContainsKey(node.Position) == false) continue;
                    if (currentNode.Position == endNode.Position)
                    {
                        var temp = new List<PathNode>();
                        var tempNode = currentNode;
                        while (tempNode.Position != startNode.Position)
                        {
                            temp.Add(tempNode);
                            tempNode = tempNode.Parent;
                        }

                        return temp.ToArray();
                    }

                    node.G = Vector2.DistanceSquared(startPos, node.Position) + node.G;
                    node.H = Vector2.DistanceSquared(goalPos, node.Position);
                    node.W = map.Map[node.Position].NodeWeight;

                    if (_closedSet.ContainsKey(node.Position) == false && _openSet.ContainsKey(node.Position) == false)
                    {
                        _openSet.Add(node.Position, node);
                    }
                }
            }

            return new PathNode[0];
        }

        public static List<Vector2> CardinalDirectionList = new List<Vector2>
            {
                new Vector2(0,Constants.DEFAULT_ENTITY_SIZE),  //Up
                new Vector2(Constants.DEFAULT_ENTITY_SIZE,0),  //Right
                new Vector2(0,-Constants.DEFAULT_ENTITY_SIZE), //Down
                new Vector2(-Constants.DEFAULT_ENTITY_SIZE,0), //Left
            };
    }
}
