using MooseEngine.Interfaces;
using MooseEngine.Scenes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace MooseEngine.Utilities
{
    public class Pathfinder
    {
        private Dictionary<Vector2,PathNode> openSet = new Dictionary<Vector2, PathNode>();
        private Dictionary<Vector2, PathNode> closedSet = new Dictionary<Vector2, PathNode>();
        private List<PathNode> neighbours = new List<PathNode>();
        private PathNode lastNode;

        public PathNode[] GetPath(Vector2 startPos, Vector2 goalPos, IDictionary<Vector2,IEntity> nodes) 
        {
            openSet.Clear();
            closedSet.Clear();

            var start = startPos * Constants.DEFAULT_ENTITY_SIZE;
            var goal = goalPos * Constants.DEFAULT_ENTITY_SIZE;

            var startNode = new PathNode(nodes[startPos].Position, 0, 0, 0, null);
            startNode.G = Vector2.DistanceSquared(startPos, startPos);
            startNode.H = Vector2.DistanceSquared(goalPos, startPos);
            startNode.F = startNode.G + startNode.H;
            var endNode = new PathNode(nodes[goalPos].Position, 0, 0, 0, null);

            openSet.Add(startNode.Position,startNode);
            lastNode = startNode;

            while (openSet.Count > 0)
            {
                var currentNode = new PathNode(new Vector2(0, 0), 0, 0, int.MaxValue, null);
                foreach (var node in openSet)
                {
                    if (node.Value.F < currentNode.F)
                    {
                        currentNode = node.Value;
                    }
                }
                int hash = 0;
                if (currentNode.Parent != null) hash = currentNode.Parent.GetHashCode();
                Console.WriteLine($"Currentnode: {currentNode.Position} G: {currentNode.G} H: {currentNode.H}  F: {currentNode.F} Parent: {hash}");

                closedSet.Add(currentNode.Position,currentNode);
                openSet.Remove(currentNode.Position);

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

                neighbours.Clear();

                foreach (var dir in CardinalDirectionList)
                {
                    var node = new PathNode(dir + currentNode.Position, currentNode);

                    neighbours.Add(node);
                }

                foreach (var node in neighbours)
                {
                    if (nodes.ContainsKey(node.Position) == false) continue;
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
                    node.F = node.G + node.H;

                    if (closedSet.ContainsKey(node.Position) == false && openSet.ContainsKey(node.Position) == false)
                    {
                        openSet.Add(node.Position,node);
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
