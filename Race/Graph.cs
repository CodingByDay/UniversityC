using Race.HelperMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race
{
   public class Graph
    {

        public class Neighbour
        {
            public int destination;
            public int weight;

            public Neighbour(int destination, int weight)
            {
                this.destination = destination;
                this.weight = weight;
            }
        }

        private int vertices;
        private  List<List<Neighbour>> adjacencyList = new List<List<Neighbour>>();

        // On using the below constructor,
        // edges must be added manually
        // to the graph using addEdge()
        public Graph(int vertices)
        {
            this.vertices = vertices;

            adjacencyList = new List<List<Neighbour>>(vertices);
            for (int i = 0; i < vertices; i++)
                adjacencyList.Add(new List<Neighbour>());
        }

        // On using the below constructor,
        // edges will be added automatically
        // to the graph using the adjacency matrix
        public Graph(int vertices, int[,] adjacencyMatrix)
        {
            this.vertices = vertices;
            adjacencyList = new List<List<Neighbour>>();
            for (int i = 0; i < vertices; i++)
                adjacencyList.Add(new List<Neighbour>());
            var check = adjacencyList.Count;
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    if (adjacencyMatrix[i,j] != 0)
                        addEdge(i, j, adjacencyMatrix[i,j]);
                }
            }
        }

        public void addEdge(int source, int destination,
                            int weight)
        {
            adjacencyList.ElementAt(source).Add(
            new Neighbour(destination, weight));
        }

        // Time complexity of this
        // implementation of dijkstra is O(V^2).
        public int[] dijkstra(int source)
        {
            bool[] isVisited = new bool[vertices];

            int[] distance = new int[vertices];

            distance.Fill(99);

            

            for (int vertex = 0; vertex < vertices; vertex++)
            {
                int minDistanceVertex = findMinDistanceVertex(
                    distance, isVisited);
                isVisited[minDistanceVertex] = true;

                foreach (Neighbour neighbour in
                 adjacencyList.ElementAt(minDistanceVertex))
                {
                    int destination = neighbour.destination;
                    int weight = neighbour.weight;

                    if (!isVisited[destination]
                        && distance[minDistanceVertex] + weight
                               < distance[destination])
                        distance[destination]
                            = distance[minDistanceVertex]
                              + weight;
                }
            }

            return distance;
        }

        // Method used by `int[] dijkstra(int)`
        private int findMinDistanceVertex(int[] distance,
                                          bool[] isVisited)
        {
            int minIndex = -1,
                minDistance = 99;

            for (int vertex = 0; vertex < vertices; vertex++)
            {
                if (!isVisited[vertex]
                    && distance[vertex] <= minDistance)
                {
                    minDistance = distance[vertex];
                    minIndex = vertex;
                }
            }

            return minIndex;
        }

        // Returns null if
        // negative weight cycle is detected
        public int[] bellmanford(int source)
        {
            int[] distance = new int[vertices];
            distance.Fill(99);
         
   

            for (int i = 0; i < vertices - 1; i++)
            {
                for (int currentVertex = 0;
                     currentVertex < vertices;
                     currentVertex++)
                {
                    foreach (Neighbour neighbour in
                     adjacencyList.ElementAt(currentVertex))
                    {
                        if (distance[currentVertex]
                                != 99
                            && distance[currentVertex]
                                       + neighbour.weight
                                   < distance
                                         [neighbour
                                              .destination])
                        {
                            distance[neighbour.destination]
                                = distance[currentVertex]
                                  + neighbour.weight;
                        }
                    }
                }
            }

            for (int currentVertex = 0;
                 currentVertex < vertices; currentVertex++)
            {
                foreach (Neighbour neighbour in
                 adjacencyList.ElementAt(currentVertex))
                {
                    if (distance[currentVertex]
                            != 99
                        && distance[currentVertex]
                                   + neighbour.weight
                               < distance[neighbour
                                              .destination])
                        return null;
                }
            }

            return distance;
        }

        // Returns null if negative
        // weight cycle is detected
        public int[][] johnsons()
        {
            // Add a new vertex q to the original graph,
            // connected by zero-weight edges to
            // all the other vertices of the graph

            this.vertices++;
            adjacencyList.Add(new List<Neighbour>());
            for (int i = 0; i < vertices - 1; i++)
                adjacencyList.ElementAt(vertices - 1)
                    .Add(new Neighbour(i, 0));

            // Use bellman ford with the new vertex q
            // as source, to find for each vertex v
            // the minimum weight h(v) of a path
            // from q to v.
            // If this step detects a negative cycle,
            // the algorithm is terminated.

            int[] h = bellmanford(vertices - 1);
            if (h == null)
                return null;

            // Re-weight the edges of the original graph using the
            // values computed by the Bellman-Ford algorithm.
            // w'(u, v) = w(u, v) + h(u) - h(v).

            for (int u = 0; u < vertices; u++)
            {
                List<Neighbour> neighbours
                    = adjacencyList.ElementAt(u);

                foreach (Neighbour neighbour in neighbours)
                {
                    int v = neighbour.destination;
                    int w = neighbour.weight;

                    // new weight
                    neighbour.weight = w + h[u] - h[v];
                }
            }

            // Step 4: Remove edge q and apply Dijkstra
            // from each node s to every other vertex
            // in the re-weighted graph

            adjacencyList.RemoveAt(vertices - 1);
            vertices--;

            int[][] distances = new int[vertices][];

            for (int s = 0; s < vertices; s++)
                distances[s] = dijkstra(s);

            // Compute the distance in the original graph
            // by adding h[v] - h[u] to the
            // distance returned by dijkstra

            for (int u = 0; u < vertices; u++)
            {
                for (int v = 0; v < vertices; v++)
                {

                    // If no edge exist, continue
                    if (distances[u][v] == 99)
                        continue;
                    var check = (h[v] - h[u]);
                    distances[u][v] += (h[v] - h[u]);
                }
            }

            return distances;
        }
    }
}
