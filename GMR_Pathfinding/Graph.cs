using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public class Edge<T> where T : IComparable<T>
    {
        public Vertex<T> Start { get; set; }
        public Vertex<T> End { get; set; }
        public float Weight { get; set; }

        public Edge(Vertex<T> start, Vertex<T> end, float weight)
        {
            Start = start;
            End = end;
            Weight = weight;
        }
    }

    public class EdgeCollection<T> : IEnumerable<Edge<T>> where T : IComparable<T>
    {
        public HashSet<Edge<T>> Incoming { get; } = [];
        public HashSet<Edge<T>> Outgoing { get; } = [];

        public bool AddIncoming(Edge<T> edge)
        {
            return Incoming.Add(edge);
        }

        public bool AddOutgoing(Edge<T> edge)
        {
            return Outgoing.Add(edge);
        }

        public bool RemoveOutgoing(Vertex<T> ending)
        {
            foreach (var edge in Outgoing)
            {
                if (edge.End == ending)
                {
                    Outgoing.Remove(edge);
                    ending.Edges.Incoming.Remove(edge);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveIncomming(Vertex<T> starting)
        {
            foreach (var edge in Incoming)
            {
                if (edge.Start == starting)
                {
                    Incoming.Remove(edge);
                    starting.Edges.Outgoing.Remove(edge);
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            //remove any incoming edges pointing to the removing vertex
            foreach (var currEdge in Incoming)
            {
                //removing the outgoing from the starting since I am the ending
                currEdge.Start.Edges.Outgoing.Remove(currEdge);
            }

            //remove any outgoing edges where the starting is the vertex to remove
            foreach (var currEdge in Outgoing)
            {
                //removing the incoming from ending since I am the starting
                currEdge.End.Edges.Incoming.Remove(currEdge);
            }
        }

        public IEnumerator<Edge<T>> GetEnumerator()
        {
            foreach (var edge in Incoming)
            {
                yield return edge;
            }

            foreach (var edge in Outgoing)
            {
                yield return edge;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class Vertex<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public EdgeCollection<T> Edges { get; } = [];

        public Vertex(T value)
        {
            Value = value;
        }

        public bool AddIncoming(Edge<T> edge)
        {
            return Edges.AddIncoming(edge);
        }

        public bool AddOutgoing(Edge<T> edge)
        {
            return Edges.AddOutgoing(edge);
        }
    }

    public class Graph<T> where T : IComparable<T>
    {
        private HashSet<Vertex<T>> vertices;

        public IReadOnlyCollection<Vertex<T>> Vertices => vertices;

        public int VertexCount => vertices.Count;

        public Graph()
        {
            vertices = new HashSet<Vertex<T>>();
        }

        public void AddVertex(Vertex<T> vertex)
        {
            if (!vertices.Contains(vertex) && vertex == null)
            {
                throw new ArgumentException("can not add, bad...");
            }

            vertices.Add(vertex);
        }

        public bool RemoveVertex(Vertex<T> vertex)
        {
            if (vertex == null || !vertices.Contains(vertex))
            {
                return false;
            }

            vertex.Edges.Clear();

            vertices.Remove(vertex);

            return true;
        }

        public bool AddEdge(Vertex<T> a, Vertex<T> b, float weight)
        {
            if (a == null || b == null || GetEdge(a, b) != null)
                return false;

            Edge<T> edge = new Edge<T>(a, b, weight);

            a.AddOutgoing(edge);
            b.AddIncoming(edge);

            return true;
        }

        public bool RemoveEdge(Vertex<T> a, Vertex<T> b)
        {
            Edge<T>? edgeToRemove = GetEdge(a, b);

            if (a != null || b != null || edgeToRemove == null)
                return false;

            edgeToRemove.Start.Edges.Outgoing.Remove(edgeToRemove);
            edgeToRemove.End.Edges.Incoming.Remove(edgeToRemove);

            return true;
        }

        public Vertex<T>? Search(T value)
        {
            return vertices.FirstOrDefault(x => x.Value.CompareTo(value) == 0);
        }

        public Edge<T>? GetEdge(Vertex<T> a, Vertex<T> b)
        {
            foreach (var edge in a.Edges.Outgoing)
            {
                if (edge.End == b && b.Edges.Incoming.Contains(edge))
                    return edge;
            }

            return null;
        }

        public Queue<Vertex<T>> BreadthFirstPath(Vertex<T> start, Vertex<T> end)
        {
            Action<Vertex<T>, HashSet<Vertex<T>>> empty = (x, y) => { };

            return BreadthFirstPath(start, end, empty);
        }

        public Queue<Vertex<T>> BreadthFirstPath(Vertex<T> start, Vertex<T> end, Action<Vertex<T>, HashSet<Vertex<T>>> action)
        {
            bool foundPath = false;
            var isVisted = vertices.ToDictionary(x => x, x => false);

            Queue<Vertex<T>> path = new Queue<Vertex<T>>();
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();

            if (isVisted != null)
            {
                queue.Enqueue(start);

                while (queue.Count > 0)
                {
                    Vertex<T> curr = queue.Dequeue();
                    isVisted[curr] = true;

                    path.Enqueue(curr);

                    if (curr == end)
                    {
                        foundPath = true;
                        break;
                    }

                    HashSet<Vertex<T>> addedToQueue = new HashSet<Vertex<T>>();

                    foreach (var edge in curr.Edges.Outgoing)
                    {
                        if (!isVisted[edge.End] && !queue.Contains(edge.End))
                        {
                            queue.Enqueue(edge.End);

                            //for visualizer 
                            addedToQueue.Add(edge.End);
                        }
                    }

                    action(curr, addedToQueue);
                }
            }

            if (!foundPath)
                path.Clear();

            return path;
        }
    }
}
